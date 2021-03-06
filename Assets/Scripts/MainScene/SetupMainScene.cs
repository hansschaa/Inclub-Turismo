﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SetupMainScene : MonoBehaviour 
{

	//Global
	static public bool desdeChapterList;

	[Header ("Reproductores")]
	public AudioSource reproductorMusica;
	public AudioSource reproductorSonido;


	[Header("Data About Points")]
	public GameObject _localStarsText;
	public GameObject _imageOverBackgroundPoints;

	private int _totalLocalStars;

	[Header("Estrellas")]
	public Sprite _starSprite;

	[Header("Random Data")]
	public int _numeroCapitulos;
	public GameObject _capitulos;

	// Use this for initialization
	void Start () 
	{
		SetupMainScene.desdeChapterList = false;
		// PlayerPrefs.DeleteAll();
		if(!ConfigController.Instance._audioEncendido)
		{	

			this.reproductorMusica.volume = 0;
			this.reproductorSonido.volume = 0;
		}

		//Imprimiendo mensajes
		Debug.Log(Application.persistentDataPath + "/savedMessages.gd");
		this._totalLocalStars = 0;
		GameObject currentChapter;
		GameObject starGroup;
		for(int i = 1; i< this._numeroCapitulos ; i++)
		{
			this._totalLocalStars += PlayerPrefs.GetInt("LocalStarsChapter" + i);
			currentChapter = this._capitulos.transform.GetChild(i-1).gameObject;
			starGroup = currentChapter.transform.Find("StarsGroup").gameObject;
			for(int j = 0 ; j < PlayerPrefs.GetInt("LocalStarsChapter" + i);j++)
			{
				var estrella = starGroup.transform.GetChild(j).gameObject;
				estrella.GetComponent<Image>().sprite = _starSprite;
			}

		}
		

		this._localStarsText.GetComponent<TextMeshProUGUI>().text = this._totalLocalStars+"/20";

		//
		float widthBar = (this._totalLocalStars * 162)/20;
		this._imageOverBackgroundPoints.GetComponent<RectTransform>().sizeDelta = new Vector2(widthBar,this._imageOverBackgroundPoints.GetComponent<RectTransform>().sizeDelta.y);
	}
}
