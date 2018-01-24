using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseDeDatos : MonoBehaviour 
{
	public static string[] _consejosLoading;
	public static int[,] _rangosRendimiento;
	public static int[,] _stagesInformation;

	public static string[] _messagesClients5;//Mensajes para redimiento entre 0% y 25%
	public static string[] _messagesClients4;//Mensajes para redimiento entre 25% y 50%
	public static string[] _messagesClients3;//Mensajes para redimiento entre 50% y 75%
	public static string[] _messagesClients2;//Mensajes para redimiento entre 75% y 99%
	public static string[] _messagesClients1;//Mensajes para redimiento 100%

	// public static int[] _chapterInformation;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		// PlayerPrefs.DeleteAll();

		BaseDeDatos._consejosLoading = new string[16];
		BaseDeDatos._rangosRendimiento = new int[5,3];
		BaseDeDatos._stagesInformation = new int[14,3];
		BaseDeDatos._messagesClients1 = new string[5];
		BaseDeDatos._messagesClients2 = new string[5];
		BaseDeDatos._messagesClients3 = new string[5];
		BaseDeDatos._messagesClients4 = new string[5];
		BaseDeDatos._messagesClients5 = new string[5];
		// BaseDeDatos._chapterInformation = new int[5];
	
		this.loadLoadingAdvices();
		this.loadRangesPerformance();
		this.loadStagesInformation();
		this.loadMessagesClients();
		// this.resetValues();
	}

    private void loadMessagesClients()
    {
		//100% performance Messages
        BaseDeDatos._messagesClients1[0]= "Muy buen servicio en el hotel";
		BaseDeDatos._messagesClients1[1]= "Esto es lo que esperaba al venir!!";
		BaseDeDatos._messagesClients1[2]= "El trato es de lo mejor en el país";
		BaseDeDatos._messagesClients1[3]= "Me sentí como en casa...";
		BaseDeDatos._messagesClients1[4]= "No tuve ningún problema en mi estadía :)";

		//75% - 99%
		BaseDeDatos._messagesClients2[0]= "Buen servicio casi todo el día";
		BaseDeDatos._messagesClients2[1]= "Me sentí seguro en el establecimiento";
		BaseDeDatos._messagesClients2[2]= "Casi ningún problema en toda mi estadía";
		BaseDeDatos._messagesClients2[3]= "Todo salió como esperaba";
		BaseDeDatos._messagesClients2[4]= "La inclusión es buena";

		//50% - 75%
		BaseDeDatos._messagesClients3[0]= "El servicio no es tan malo, volvería otro día";
		BaseDeDatos._messagesClients3[1]= "Me esperé un mejor trato en ocaciones";
		BaseDeDatos._messagesClients3[2]= "Falta trabajo en el trato al cliente";
		BaseDeDatos._messagesClients3[3]= "Creí que la pasaría mejor";
		BaseDeDatos._messagesClients3[4]= "Tuve solo unos pocos problemas";

		//25% - 50%
		BaseDeDatos._messagesClients4[0]= "No la pasé muy bien en este recinto";
		BaseDeDatos._messagesClients4[1]= "Deberían estudiar más sobre como tratar a gente de escasa visión";
		BaseDeDatos._messagesClients4[2]= "Mmm nose si volvería otro día";
		BaseDeDatos._messagesClients4[3]= "Vine a descansar pero hice todo lo contrario debido al trato";
		BaseDeDatos._messagesClients4[4]= "Faltó mas empatía de los trabajadores con los clientes";

		//0% - 25%
		BaseDeDatos._messagesClients5[0]= "No volveré más a hospedarme en éste hotel";
		BaseDeDatos._messagesClients5[1]= "Me sentí inseguro la mayor parte del tiempo";
		BaseDeDatos._messagesClients5[2]= "No me trataron como esperaba";
		BaseDeDatos._messagesClients5[3]= "Creí que el hotel era bueno";
		BaseDeDatos._messagesClients5[4]= "No vendré nunca más";
    }

    private void resetValues()
    {
        PlayerPrefs.DeleteAll();
    }

    private void loadStagesInformation()
    {
        BaseDeDatos._stagesInformation[0,0] = 0; // id del subcapitulo
		BaseDeDatos._stagesInformation[0,1] = 2; // puntaje total del sub capitulo
		BaseDeDatos._stagesInformation[0,2] = 0; // numero de sub capitulo
		
		BaseDeDatos._stagesInformation[1,0] = 1;
		BaseDeDatos._stagesInformation[1,1] = 2;
		BaseDeDatos._stagesInformation[1,2] = 1;
		
		BaseDeDatos._stagesInformation[2,0] = 2; 
		BaseDeDatos._stagesInformation[2,1] = 2;
		BaseDeDatos._stagesInformation[2,2] = 2;
		
		BaseDeDatos._stagesInformation[3,0] = 3;
		BaseDeDatos._stagesInformation[3,1] = 4;
		BaseDeDatos._stagesInformation[3,2] = 3;
		
		//2DO CAPITULO
		BaseDeDatos._stagesInformation[4,0] = 4;
		BaseDeDatos._stagesInformation[4,1] = 4;
		BaseDeDatos._stagesInformation[4,2] = 0;

		BaseDeDatos._stagesInformation[5,0] = 5;
		BaseDeDatos._stagesInformation[5,1] = 4;
		BaseDeDatos._stagesInformation[5,2] = 1;

		//3DO CAPITULO
		BaseDeDatos._stagesInformation[6,0] = 6;
		BaseDeDatos._stagesInformation[6,1] = 1;
		BaseDeDatos._stagesInformation[6,2] = 0;

		BaseDeDatos._stagesInformation[7,0] = 7;
		BaseDeDatos._stagesInformation[7,1] = 1;
		BaseDeDatos._stagesInformation[7,2] = 1;

		BaseDeDatos._stagesInformation[8,0] = 8;
		BaseDeDatos._stagesInformation[8,1] = 1;
		BaseDeDatos._stagesInformation[8,2] = 2;

		BaseDeDatos._stagesInformation[9,0] = 9;
		BaseDeDatos._stagesInformation[9,1] = 1;
		BaseDeDatos._stagesInformation[9,2] = 3;

		//4TO CAPITULO
		BaseDeDatos._stagesInformation[10,0] = 10;
		BaseDeDatos._stagesInformation[10,1] = 1;
		BaseDeDatos._stagesInformation[10,2] = 0;

		BaseDeDatos._stagesInformation[11,0] = 11;
		BaseDeDatos._stagesInformation[11,1] = 1;
		BaseDeDatos._stagesInformation[11,2] = 1;

		BaseDeDatos._stagesInformation[12,0] = 12;
		BaseDeDatos._stagesInformation[12,1] = 1;
		BaseDeDatos._stagesInformation[12,2] = 2;

		BaseDeDatos._stagesInformation[13,0] = 13;
		BaseDeDatos._stagesInformation[13,1] = 1;
		BaseDeDatos._stagesInformation[13,2] = 3;

    }

    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    public void loadLoadingAdvices()
	{
		BaseDeDatos._consejosLoading[0] = "No hagas suposiciones de lo que la persona puede o no puede hacer, pregúntale: ¿cómo puedo ayudarte?";
		BaseDeDatos._consejosLoading[1] = "Dirígete a la persona directamente y no a su acompañante";
		BaseDeDatos._consejosLoading[2] = "Es bueno hacer contacto físico para que la persona pueda saber que le estás hablando a ella";
		BaseDeDatos._consejosLoading[3] = "Siempre identifíquese con su nombre";
		BaseDeDatos._consejosLoading[4] = "La señalización e información debe estar con letra grande y contraste esperado, para los clientes con baja visión";
		BaseDeDatos._consejosLoading[5] = "Las personas que ayudan o dirigen a las personas ciegas se denominan guías videntes";
		BaseDeDatos._consejosLoading[6] = "Para ser un guía vidente, ofrezca al cliente su codo";
		BaseDeDatos._consejosLoading[7] = "Describa los puntos de salida, la cafetería, los baños";
		BaseDeDatos._consejosLoading[8] = "Cuando salga de la habitación avise a a persona ciega, lo mismo cuando regrese";
		BaseDeDatos._consejosLoading[9]= "Utilice lenguaje concreto para describir lugares o cosas, las referencias así, aquí, allá, no son útiles";
		BaseDeDatos._consejosLoading[10] = "Utilice términos visuales sin pudor al hablar con personas ciegas, por ejemplo nos vemos mañana, podemos ver";
		BaseDeDatos._consejosLoading[11] = "Para subir y bajar escaleras ubique la mano de la persona ciega en el pasamanos";
		BaseDeDatos._consejosLoading[12] = "Al pasar por un lugar estrecho el guía debe ubicar su mano por detrás de la espalda para que el ciego se sienta protegido";
		BaseDeDatos._consejosLoading[13] = "En el servicio de alimentación siempre deben describir el menú y el costo";
		BaseDeDatos._consejosLoading[14] = "Cada vez que se le quiera entregar un objeto se le acerca la mano de la persona a este";
		BaseDeDatos._consejosLoading[15] = "Hacer una descripción de como se distrubuyen las cosas en la mesa para comer ";
	}

	private void loadRangesPerformance()
    {
		BaseDeDatos._rangosRendimiento[0,0]= 5; // Porcentaje inferior
		BaseDeDatos._rangosRendimiento[0,1]= 25; // Porcentaje superior
		BaseDeDatos._rangosRendimiento[0,2]= 1; // Cantidad de estrellas
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
}
