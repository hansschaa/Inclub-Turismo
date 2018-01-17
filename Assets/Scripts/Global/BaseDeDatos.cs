using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDeDatos : MonoBehaviour 
{
	public static string[] _consejosLoading;
	public static int[,] _rangosRendimiento;
	public static string[] _bossFeedBack;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		BaseDeDatos._consejosLoading = new string[16];
		BaseDeDatos._rangosRendimiento = new int[5,3];
		BaseDeDatos._bossFeedBack = new string[6];
		this.loadLoadingAdvices();
		this.loadRangesPerformance();
		this.loadFeedBack();
	}

	/// <summary>
	/// LateUpdate is called every frame, if the Behaviour is enabled.
	/// It is called after all Update functions have been called.
	/// </summary>
	public void loadLoadingAdvices()
	{
		BaseDeDatos._consejosLoading[0] = "No hagas supposiciones de lo que la persona puede o no puede hacer, pregúntale: ¿cómo puedo ayudarte?";
		BaseDeDatos._consejosLoading[1] = "Dirígete a la persona directamente y no a su acompañante";
		BaseDeDatos._consejosLoading[2] = "Es bueno hacer contacto físico para que la persona pueda saber que le estás hablando a ella";
		BaseDeDatos._consejosLoading[3] = "Siempre identifiquese con su nombre";
		BaseDeDatos._consejosLoading[4] = "La señalización e información debe estar con letra grande y contraste esperado, para los clientes con baja visión";
		BaseDeDatos._consejosLoading[5] = "Las personas que ayudan o dirigen a las personas ciegas se denominan guías videntes";
		BaseDeDatos._consejosLoading[6] = "Para ser un guía vidente, ofrezca al cliente su codo";
		BaseDeDatos._consejosLoading[7] = "Describa los puntos de salida, la cafetería, los baños";
		BaseDeDatos._consejosLoading[8] = "Cuando salga de la habitación avise a a persona ciega, lo mismo cuando regrese";
		BaseDeDatos._consejosLoading[9]= "Utilice lenguaje concreto para describir lugares o cosas, las referencias así, aquí, allá no son útiles";
		BaseDeDatos._consejosLoading[10] = "Utilice términos visuales sin pudor al hablar con personas ciegas, por ejemplo nos vemos mañana, podemos ver";
		BaseDeDatos._consejosLoading[11] = "Para subir y bajar escaleras ubique la mano de la persona ciega en el pasamanos";
		BaseDeDatos._consejosLoading[12] = "Al pasar por un lugar estrecho el guía debe ubicar su mano por detrás de la espalda para que el ciego se sienta protegido";
		BaseDeDatos._consejosLoading[13] = "En el servicio de alimentación siempre deben describir el menú y el costo";
		BaseDeDatos._consejosLoading[14] = "Cada vez que se le quiera entregar un objeto se le acerca la mano de la persona a este";
		BaseDeDatos._consejosLoading[15] = "Hacer una descripción de como se distrubuyen las cosas en la mesa para comer ";
	}

	private void loadRangesPerformance()
    {
		BaseDeDatos._rangosRendimiento[0,0]= 5;
		BaseDeDatos._rangosRendimiento[0,1]= 25;
		BaseDeDatos._rangosRendimiento[0,2]= 1;
		BaseDeDatos._rangosRendimiento[1,0]= 25;
		BaseDeDatos._rangosRendimiento[1,1]= 50;
		BaseDeDatos._rangosRendimiento[1,2]= 2;
		BaseDeDatos._rangosRendimiento[2,0]= 50;
		BaseDeDatos._rangosRendimiento[2,1]= 75;
		BaseDeDatos._rangosRendimiento[2,2]= 3;
		BaseDeDatos._rangosRendimiento[3,0]= 75;
		BaseDeDatos._rangosRendimiento[3,1]= 99;
		BaseDeDatos._rangosRendimiento[3,2]= 4;
		BaseDeDatos._rangosRendimiento[4,0]= 99;
		BaseDeDatos._rangosRendimiento[4,1]= 100;
		BaseDeDatos._rangosRendimiento[4,2]= 5;
    }

	private void loadFeedBack()
    {
        BaseDeDatos._bossFeedBack[0]="Al atender una persona en situación de discapacidad visual debemos presentarnos e indicar nuestro cargo.";
		BaseDeDatos._bossFeedBack[1]="Debemos utilizar un lenguaje concreto, que permita dar puntos de orientación o de referencia.";
		BaseDeDatos._bossFeedBack[2]="FEEDBACK";
		BaseDeDatos._bossFeedBack[3]="FEEDBACK";
		BaseDeDatos._bossFeedBack[4]="FEEDBACK";
		BaseDeDatos._bossFeedBack[5]="FEEDBACK";
    }
}
