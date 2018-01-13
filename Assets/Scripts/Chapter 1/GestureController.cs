using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

public class GestureController : MonoBehaviour 
{
	[Header("Controllers")]
	public GameObject _soundController;

	[Header("Variables")]
	public bool _isTick;
	public bool _isX;
	public GameObject _mainCanvas;

	public GameObject _nullIcon;

	[Header("Generacion Collider")]
	public float _circleColliderRadius;

	public Transform gestureOnScreenPrefab;
	private int strokeId = -1;
	private Vector3 virtualKeyPosition = Vector2.zero;
	private RuntimePlatform platform;
	private int vertexCount = 0;
	public List<LineRenderer> gestureLinesRenderer = new List<LineRenderer>();
	public LineRenderer currentGestureLineRenderer;
	private bool recognized;

	public Vector2 _intersección;

	public Text _textDebug;

	public bool _beginCount;
	

	[Header("Tiempo")]
	[Tooltip("Delay for recognize the gesture, this time may vary depending on the device")]
	public float _recognizeDelayTime;
	[Tooltip("Range of time for recognize, after this, the gestures are deleted")]
	public float _delayDeleteGestureTime;

	float _time=0;

	//Parameters for help the collision in buttons or objects 
	Vector3 mousePos;
	Vector2 mousePos2D;
	RaycastHit2D hit;

	void Start () 
	{
		this._isTick = false;
		this._isX = false;
		this.platform = Application.platform;
	}

	void Update () 
	{
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
			this.mousePos = Camera.main.ScreenToWorldPoint(virtualKeyPosition);
            this.mousePos2D = new Vector2(mousePos.x, mousePos.y);
			this.hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
			if(hit.collider != null && hit.collider.gameObject.tag.Equals("NotDrawOverThis") )
			{
				
				return;
			}
			


			this._beginCount = true; 

			strokeId++;
			
			Transform tmpGesture = Instantiate(gestureOnScreenPrefab, transform.position, transform.rotation) as Transform;
			currentGestureLineRenderer = tmpGesture.GetComponent<LineRenderer>();
			
			gestureLinesRenderer.Add(currentGestureLineRenderer);
			
			vertexCount = 0;	
		}

		if (Input.GetMouseButton(0) && currentGestureLineRenderer!= null) 
		{
			currentGestureLineRenderer.SetVertexCount(++vertexCount);
			currentGestureLineRenderer.SetPosition(vertexCount - 1, Camera.main.ScreenToWorldPoint(new Vector3(virtualKeyPosition.x, virtualKeyPosition.y, 10)));
		}
		
		if(this._beginCount)
		{
			this._time += Time.deltaTime;
			this._textDebug.text = this._time + "";
			if(_time >= this._delayDeleteGestureTime)
			{
				this._beginCount = false;
				this._time = 0;
				this.recognizeGesture();
				
				StartCoroutine(waitThenCallback(this._recognizeDelayTime, () => 
				{ 
					
					this.eraseGesture();
				
				}));
			}	
		}
	}

	public void recognizeGesture()
	{
		
		
		this._isX = checkX();
		if(this._isX)
		{
			this.addCircle2DCollider(gestureLinesRenderer[0].gameObject,true);
			this.addCircle2DCollider(gestureLinesRenderer[1].gameObject,true);
			// this.addCircle2DCollider(gestureLinesRenderer[0].gameObject, gestureLinesRenderer[0].GetPosition(0), gestureLinesRenderer[0].GetPosition(gestureLinesRenderer[0].positionCount-1));
			// this.addCircle2DCollider(gestureLinesRenderer[1].gameObject, gestureLinesRenderer[1].GetPosition(0), gestureLinesRenderer[1].GetPosition(gestureLinesRenderer[1].positionCount-1));

			StartCoroutine(waitThenCallback(this._recognizeDelayTime, () => 
			{ 
				if(gestureLinesRenderer[0].gameObject.GetComponent<LineRendererController>().colisione)
				{
					// this._soundController.GetComponent<SoundController>().playResultAnswerSound(true);
					_textDebug.text = "Es una X";
					gestureLinesRenderer[0].gameObject.GetComponent<LineRendererController>()._typeShape = 0;
					gestureLinesRenderer[1].gameObject.GetComponent<LineRendererController>()._typeShape = 0;
				}

				else
				{
					_textDebug.text = "NO Es una X";
				}
			}));
		}
			

		else
		{
			this._isTick = checkTick();
			if(this._isTick)
			{
				// this._soundController.GetComponent<SoundController>().playResultAnswerSound(true);
				this.addCircle2DCollider(gestureLinesRenderer[0].gameObject,false);
				gestureLinesRenderer[0].gameObject.GetComponent<LineRendererController>()._typeShape = 1;
				_textDebug.text = "Es un tick";
			}
				

			else
			{
				// this._soundController.GetComponent<SoundController>().playResultAnswerSound(false);

				gestureLinesRenderer[0].gameObject.GetComponent<LineRendererController>()._typeShape =-1;
				this.addEdgeCollider(gestureLinesRenderer[0].gameObject);
				_textDebug.text = "Eso no es un tick ni una X";
			}
		}	
	}

    private bool checkX()
    {
        //Comprobar que hayan dos trazos
		if(this.gestureLinesRenderer.Count != 2)
		{
			// print(this.gestureLinesRenderer.Count);
			// print("Saliste aca");
			return false;
		}

		return true;
    }

	List<Vector2> m_Points = new List<Vector2>();
	int incremento = 1;
	private void addEdgeCollider(GameObject line)
	{
		EdgeCollider2D e = line.AddComponent<EdgeCollider2D>();	
		for(int i = 0; i< line.GetComponent<LineRenderer>().positionCount && (i+incremento) < line.GetComponent<LineRenderer>().positionCount;i+=incremento)
		{
			m_Points.Add(line.GetComponent<LineRenderer>().GetPosition(i));
		}
		e.isTrigger = true;
		e.points = m_Points.ToArray();	
	}

	
	private void addCircle2DCollider(GameObject line, bool havetwoLines)
	{
		//Agrego el LineRendererController
		line.AddComponent<LineRendererController>();

		//Agrego el collider circular
		line.AddComponent<CircleCollider2D>();
		line.GetComponent<CircleCollider2D>().isTrigger = true;
		line.AddComponent<Rigidbody2D>();
		line.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
		Vector2 midPoint;
		if(havetwoLines)
		{
			
			midPoint = (line.GetComponent<LineRenderer>().GetPosition(0) + line.GetComponent<LineRenderer>().GetPosition(line.GetComponent<LineRenderer>().positionCount-1))/2;
			line.GetComponent<LineRendererController>()._centerLine= midPoint;
			line.GetComponent<CircleCollider2D>().transform.position = midPoint; 
		}

		else
		{
			midPoint = this._intersección;
			line.GetComponent<LineRendererController>()._centerLine=  this._intersección;
			line.GetComponent<CircleCollider2D>().transform.position = this._intersección; 

		}
	
		line.GetComponent<CircleCollider2D>().offset = Vector2.zero;
		line.GetComponent<CircleCollider2D>().radius = this._circleColliderRadius;
		
	}
	
	private void addColliderToLine(GameObject line, Vector2 origen,Vector2 final)
    {
		line.AddComponent<BoxCollider2D>();
		line.GetComponent<BoxCollider2D>().isTrigger = true;
		// line.AddComponent<LineRendererController>();
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

		line.GetComponent<BoxCollider2D>().offset = Vector2.zero;
    
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
					this._intersección = gestureLinesRenderer[0].GetPosition(primerPuntoInterseccion);
					
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
					this._intersección = gestureLinesRenderer[0].GetPosition(primerPuntoInterseccion);

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
			print("Es una linea cualquiera");
			Vector2 midPoint = (gestureLinesRenderer[0].GetPosition(0) + gestureLinesRenderer[0].GetPosition(gestureLinesRenderer[0].positionCount-1))/2;
			print(midPoint);
			gestureLinesRenderer[0].GetComponent<LineRendererController>()._centerLine = midPoint;
			return false;
		}

		else
		{
			gestureLinesRenderer[0].GetComponent<LineRendererController>()._centerLine = this._intersección;
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
		if(this.gestureLinesRenderer.Count==2)
		{
			if(!this.gestureLinesRenderer[0].GetComponent<LineRendererController>()._colisionWhitInteractiveForm && 
			!this.gestureLinesRenderer[1].GetComponent<LineRendererController>()._colisionWhitInteractiveForm)
			{
				Vector3 nullIconPosition = Vector3.zero;
				nullIconPosition = gestureLinesRenderer[0].GetComponent<LineRendererController>()._centerLine;
				if(!this.gestureLinesRenderer[0].GetComponent<LineRendererController>()._colisionWhitInteractiveForm)
				{
					this._soundController.GetComponent<SoundController>().playResultAnswerSound(false);
					Destroy(Instantiate(this._nullIcon, nullIconPosition,Quaternion.identity, this._mainCanvas.transform),0.5f);
					print("No colisione con un objeto interactivo pero quien sabe si con una linea :)");
				}

			}
		}

		else
		{
			if(!this.gestureLinesRenderer[0].GetComponent<LineRendererController>()._colisionWhitInteractiveForm)
			{
				Vector3 nullIconPosition = Vector3.zero;
				if(gestureLinesRenderer[0].GetComponent<LineRendererController>()._typeShape==-1)
				{
					this._soundController.GetComponent<SoundController>().playResultAnswerSound(false);
					nullIconPosition = gestureLinesRenderer[0].GetComponent<EdgeCollider2D>().bounds.center;
					nullIconPosition.z = 0;
				}

				else
				{
					nullIconPosition = this._intersección;
				}

				if(!this.gestureLinesRenderer[0].GetComponent<LineRendererController>()._colisionWhitInteractiveForm)
				{
					this._soundController.GetComponent<SoundController>().playResultAnswerSound(false);
					Destroy(Instantiate(this._nullIcon, nullIconPosition,Quaternion.identity, this._mainCanvas.transform),0.5f);
					print("No colisione con un objeto interactivo pero quien sabe si con una linea :)");
				}

			}

		}
		

		this.strokeId = -1;

		foreach (LineRenderer lineRenderer in this.gestureLinesRenderer)
		{
			Destroy(lineRenderer.gameObject);
		}

		this.gestureLinesRenderer.Clear();
		this.m_Points.Clear();
		
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

	public static IEnumerator waitThenCallback(float time, Action callback)
	{
		yield return new WaitForSeconds(time);
		callback();
	}
}
