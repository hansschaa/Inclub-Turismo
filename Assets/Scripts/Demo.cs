using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

public class Demo : MonoBehaviour {

	public Transform gestureOnScreenPrefab;
	private int strokeId = -1;
	private Vector3 virtualKeyPosition = Vector2.zero;
	private RuntimePlatform platform;
	private int vertexCount = 0;
	public List<LineRenderer> gestureLinesRenderer = new List<LineRenderer>();
	public LineRenderer currentGestureLineRenderer;

	private bool recognized;

	public Text textDebug;

	void Start () 
	{
		this.platform = Application.platform;
	}

	void Update () {

		if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer) 
		{
			if (Input.touchCount > 0) 
			{
				virtualKeyPosition = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
			}
		} 

		//For Desktop
		else 
		{
			if (Input.GetMouseButton(0)) 
			{
				virtualKeyPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
			}
		}

		

		if (Input.GetMouseButtonDown(0)) 
		{

			if (recognized) 
			{

				recognized = false;
				strokeId = -1;

				foreach (LineRenderer lineRenderer in gestureLinesRenderer)
				{
					lineRenderer.SetVertexCount(0);
					Destroy(lineRenderer.gameObject);
				}

				gestureLinesRenderer.Clear();
			}

			strokeId++;
			
			Transform tmpGesture = Instantiate(gestureOnScreenPrefab, transform.position, transform.rotation) as Transform;
			currentGestureLineRenderer = tmpGesture.GetComponent<LineRenderer>();
			
			gestureLinesRenderer.Add(currentGestureLineRenderer);
			
			vertexCount = 0;
		}
		
		if (Input.GetMouseButton(0)) {
			
			currentGestureLineRenderer.SetVertexCount(++vertexCount);
			currentGestureLineRenderer.SetPosition(vertexCount - 1, Camera.main.ScreenToWorldPoint(new Vector3(virtualKeyPosition.x, virtualKeyPosition.y, 10)));
		}
		
	}

	public void recognizeGesture()
	{
		bool isX = checkX();
		if(isX)
		{
			this.addColliderToLine(gestureLinesRenderer[0].gameObject, gestureLinesRenderer[0].GetPosition(0), gestureLinesRenderer[0].GetPosition(gestureLinesRenderer[0].positionCount-1));
			this.addColliderToLine(gestureLinesRenderer[1].gameObject, gestureLinesRenderer[1].GetPosition(0), gestureLinesRenderer[1].GetPosition(gestureLinesRenderer[1].positionCount-1));
			
			StartCoroutine(waitThenCallback(0.2f, () => 
			{ 
				if(gestureLinesRenderer[0].gameObject.GetComponent<LineRendererController>().colisione)
				{
					textDebug.text = "Es una X";
				}

				else
				{
					textDebug.text = "NO Es una X";
				}
			}));
		}
			

		else
		{
			bool isTick = checkTick();
			if(isTick)
				textDebug.text = "Es un tick";

			else
				textDebug.text = "Eso no es un tick ni una X";
		}

		// this.eraseGesture();

		
	}

    private bool checkX()
    {

        //Comprobar que hayan dos trazos
		if(this.gestureLinesRenderer.Count-1 != 2)
		{
			return false;
		}

		//Falta saber que las dos lineas no tenga bifurcaciones
		//Falta saber que no sean curvas

		return true;
    }

	private void addColliderToLine(GameObject line, Vector2 origen,Vector2 final)
    {
		line.AddComponent<BoxCollider2D>();
		line.GetComponent<BoxCollider2D>().isTrigger = true;
		line.AddComponent<LineRendererController>();
		line.AddComponent<Rigidbody2D>();
		line.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        
		
		float lineLength = Vector3.Distance (origen, final); 
        line.GetComponent<BoxCollider2D>().size = new Vector3 (lineLength, 0.1f, 1f); 
        Vector3 midPoint = (origen + final)/2;
        line.GetComponent<BoxCollider2D>().transform.position = midPoint; 
        float angle = (Mathf.Abs (origen.y - final.y) / Mathf.Abs (origen.x - final.x));
        if((origen.y<final.y && origen.x>final.x) || (final.y<origen.y && final.x>origen.x))
        {
            angle*=-1;
        }
        angle = Mathf.Rad2Deg * Mathf.Atan (angle);
        line.GetComponent<BoxCollider2D>().transform.Rotate (0, 0, angle);

		line.GetComponent<Collider2D>().offset = Vector2.zero;
    
    }

    public bool checkTick()
	{
		int pending=0;
		int bifurcacionesCantidad = 0;
		bool pendiente= false;
		bool primerPuntoInterseccionEncontrado= false;
		int primerPuntoInterseccion = 0;
		double positionY1;
		double positionY2;

		//Se obtiene información sobre donde comenzó a hacer el tick el usuario
		// 0 para pendiente neutra
		// 1 para crecimiento positivo en x
		// -1 para crecimiento negativo en x
		pending = this.getSlope (gestureLinesRenderer[0]);

		//<First Barrier> Analizando cambios de direccion en y
		if(!primeraBarreraTick(gestureLinesRenderer[0]))
		{
			print("Estás dibujando lineas hacia arriba");
			return false;
		}
		//</First Barrier>

		//<Second Barrier> Analizando cambios de dirección en y
		for(int i = 0; i< gestureLinesRenderer[0].positionCount-1; i++)
		{
			positionY1 = gestureLinesRenderer[0].GetPosition(i).y;
			positionY2 = gestureLinesRenderer[0].GetPosition(i+1).y;
			if(positionY2>positionY1 && !pendiente)
			{
				bifurcacionesCantidad++;
				pendiente = true;
				if(!primerPuntoInterseccionEncontrado)
				{
					primerPuntoInterseccionEncontrado = true;
					primerPuntoInterseccion = i+1;
				}
			}

			else if(positionY1>positionY2 && pendiente)
			{
				bifurcacionesCantidad++;
				pendiente = false;
				if(!primerPuntoInterseccionEncontrado)
				{
					primerPuntoInterseccionEncontrado = true;
					primerPuntoInterseccion = i+1;
					
				}
			}
		}

		double positionX1Aux;
		double positionX2Aux;
		//<Third Barrier> Analizando cambios de dirección en x
		//hacia el tercer cuadrante
		if(pending == -1)
		{
			for(int i = 0; i< gestureLinesRenderer[0].positionCount-1; i++)
			{
				positionX1Aux = gestureLinesRenderer[0].GetPosition(i).x;
				positionX2Aux = gestureLinesRenderer[0].GetPosition(i+1).x;
				if(positionX2Aux<positionX1Aux)
				{
					print("Partió de arriba hacia abajo pero en x dobló hacia delante");
					return false;
				}
			}
		}

		else if(pending == 1)
		{
			for(int i = 0; i< gestureLinesRenderer[0].positionCount-1; i++)
			{
				positionX1Aux = gestureLinesRenderer[0].GetPosition(i).x;
				positionX2Aux = gestureLinesRenderer[0].GetPosition(i+1).x;
				if(positionX1Aux < positionX2Aux)
				{
					print("Partió de abajo hacia arriba pero en x dobló hacia atrás");
					return false;
				}
			}
		}

		//</second>

		if(bifurcacionesCantidad!=1)
		{
			return false;
		}

		

		//<Fourth Barrier> Analizando tamaños de los dos trazos
		float primeraDistancia;
		float segundaDistancia;

		if(pending == 1)
		{
			primeraDistancia = Math.Abs(Vector3.Distance(gestureLinesRenderer[0].GetPosition(0), gestureLinesRenderer[0].GetPosition(primerPuntoInterseccion-1)));
			segundaDistancia = Math.Abs(Vector3.Distance(gestureLinesRenderer[0].GetPosition(primerPuntoInterseccion), gestureLinesRenderer[0].GetPosition(gestureLinesRenderer[0].positionCount-1)));
			
			if(primeraDistancia < segundaDistancia)
			{
				return false;	
			}
		}

		else if(pending == -1)
		{
			primeraDistancia = Math.Abs(Vector3.Distance(gestureLinesRenderer[0].GetPosition(0), gestureLinesRenderer[0].GetPosition(primerPuntoInterseccion-1)));
			segundaDistancia = Math.Abs(Vector3.Distance(gestureLinesRenderer[0].GetPosition(primerPuntoInterseccion), gestureLinesRenderer[0].GetPosition(gestureLinesRenderer[0].positionCount-1)));
			
			if(primeraDistancia > segundaDistancia)
			{
				return false;	
			}
		}

		//</Third Barrier>

		
		return true;
	}

    private bool primeraBarreraTick(LineRenderer trazo)
    {
		double positionY1Aux=0;
		double positionY2Aux=0;

        for(int i = 0 ; i < trazo.positionCount-1;i++)
		{	
			positionY1Aux = trazo.GetPosition(i).y;
			positionY2Aux = trazo.GetPosition(i+1).y;
			if(positionY1Aux != positionY2Aux)
			{
				print(positionY1Aux);
				print(positionY2Aux);
				if(positionY1Aux < positionY2Aux)
				{	
					return false;
				}

				break;
			}
		}

		return true;
    }

    //Erase the current Gesture
    public void eraseGesture()
	{
		this.strokeId = -1;

		foreach (LineRenderer lineRenderer in this.gestureLinesRenderer)
		{
			Destroy(lineRenderer.gameObject);
		}

		this.gestureLinesRenderer.Clear();
	}

	// 0 para pendiente neutra
	// 1 para crecimiento positivo en x
	// -1 para crecimiento negativo en x
	public int getSlope(LineRenderer trazo)
	{
		double positionX1;
		double positionX2;

		for(int i = 0 ; i < trazo.positionCount-1;i++)
		{
			positionX1 = gestureLinesRenderer[0].GetPosition(i).x;
			positionX2 = gestureLinesRenderer[0].GetPosition(i+1).x;
			if(positionX1 > positionX2)
			{

				return 1;
			}

			else if(positionX2 > positionX1)
			{
				return -1;
			}
		}

		return 0;
	}

	private IEnumerator waitThenCallback(float time, Action callback)
	{
		yield return new WaitForSeconds(time);
		callback();
	}
}
