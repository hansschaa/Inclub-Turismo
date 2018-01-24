using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MechanicControllerChapter1 : MonoBehaviour
{
	[Header("Controllers")]
	public GameObject _chapter1ViewController;
	private Chapter1ViewController _chapter1View;

	public GameObject _soundController;
	public GameObject _animationController;

	[Header("Animation")]
	public Color _green;
	public Color _red;
	public Color _brown;

	[Header("SubChapters")]
	public int _currentChapter;
	public int _subChaptersCount;
	public int _currentSubChapter; // numero del sub capitulo dentro del capitulo
	public int _subChapterKey; // numero unico del sub capitulo
	public int _currentPoints;

	[Header ("Reproductores")]
	public AudioSource reproductorMusica;
	public AudioSource reproductorSonido;

	[HideInInspector]
	public float _maxPoints;

	[HideInInspector]
	public string _initialBossDescription;

	public int _pointsGlobalChapter;
	public int _wrongAnswers;

	public bool _nextSubChapter;

	// [HideInInspector]
	// public int _totallyQuestions;
	
	void Start()
	{

		if(!ConfigController.Instance._audioEncendido)
		{	
			this.reproductorMusica.volume = 0;
			this.reproductorSonido.volume = 0;
		}

		this._nextSubChapter = true;
		this._chapter1View = this._chapter1ViewController.GetComponent<Chapter1ViewController>();
		this._wrongAnswers = 0;
		this._pointsGlobalChapter=0;
		this.inicializarDatos();
	}
	private void inicializarDatos()
    {
		this._currentPoints=0;

		this._maxPoints = BaseDeDatos._stagesInformation[_subChapterKey,1];
		this._currentSubChapter = BaseDeDatos._stagesInformation[_subChapterKey,2];

		if(_currentSubChapter != 0)
			this._chapter1View.loadNewView(this._currentSubChapter,this._currentPoints,this._maxPoints);

		
    }

    // public void openTutorial(float _subChapterKey)
    // {
	// 	if(PlayerPrefs.GetInt("playerChapter" + _subChapterKey)==0)
	// 	{
	// 		this._chapter1View.showBossPanel(this._initialBossDescription, this._brown);
	// 		PlayerPrefs.SetInt("playerChapter" + _subChapterKey,1);
	// 	}
    // }

   

    public void compareResults(LineRendererController lineRendererController, GameObject gameObjectEscene)
	{
		ShapeController shapeController = gameObjectEscene.GetComponent<ShapeController>();


		if(lineRendererController._typeShape == shapeController._espectedShape && gameObjectEscene.activeInHierarchy)
		{
			lineRendererController._colisionoCorresponde = true;
			this._soundController.GetComponent<SoundController>().playResultAnswerSound(true);
			this.changeTextView(gameObjectEscene);
			this._currentPoints++;
			this._pointsGlobalChapter++;

			if(shapeController._haveGoodResponseFeedback)
				this._chapter1View.showBossPanel(shapeController._feedbackGoodResponse,this._green);

			if(this._currentPoints == this._maxPoints)
			{	
				if(shapeController._haveGoodResponseFeedback)
					this._nextSubChapter = false;

				else
				{
					if(this._currentSubChapter+1 < this._subChaptersCount)
					{
						this._subChapterKey++;
						this._currentSubChapter++;
						this._chapter1View.updateScoreText(this._currentPoints,this._maxPoints);
						this.inicializarDatos();
					}				

					else 
					{
						
						this.backEndFinalData();
					}
				}
				
			}

			else
			{
				this._chapter1View.updateScoreText(_currentPoints,_maxPoints);
			}
		}

		else
		{
			if(!gameObjectEscene.GetComponent<ShapeController>()._wasWrongAnswered)
			{
				this._wrongAnswers++;
				gameObjectEscene.GetComponent<ShapeController>()._wasWrongAnswered = true;
			}
				
			this._soundController.GetComponent<SoundController>().playResultAnswerSound(false);

			if(shapeController._haveBadResponseFeedback)
				this._chapter1View.showBossPanel(shapeController._feedbackBadResponse,this._red);

			gameObjectEscene.GetComponent<ShapeController>()._touchLine = false;
		}
		
	}

	public void xButtonResetLevel()
	{
		if(this._currentSubChapter+1 < this._subChaptersCount)
		{
			this._subChapterKey++;
			this._currentSubChapter++;
			this._chapter1View.updateScoreText(this._currentPoints,this._maxPoints);
			this.inicializarDatos();
		}				

		else 
		{
			
			this.backEndFinalData();
		}

	}
  

    private void backEndFinalData()
    {
		float wrongAverage = (this._wrongAnswers*100)/this._pointsGlobalChapter;
		float performance = 100 - wrongAverage;
		int stars;

		if(performance<5)
		{
			stars= 0;
			this._chapter1View.updateFinalView(performance, stars, this._wrongAnswers, this._pointsGlobalChapter);
		}

		else
		{
			for(int i = 0 ; i < 5; i++)
			{
				if(performance >= BaseDeDatos._rangosRendimiento[i,0] && performance <= BaseDeDatos._rangosRendimiento[i,1])
				{
					
					stars = BaseDeDatos._rangosRendimiento[i,2];
					this._chapter1View.updateFinalView(performance, stars, this._wrongAnswers, this._pointsGlobalChapter);
				
					if(PlayerPrefs.GetInt("LocalStarsChapter" + this._currentChapter) < stars)
					{
						PlayerPrefs.SetInt("LocalStarsChapter" + this._currentChapter, stars);
					}

					break;
				}
			}
		}
    }

	private void changeTextView(GameObject gameObjectEscene)
    {
		ShapeController shapeController = gameObjectEscene.GetComponent<ShapeController>();
		Transform currentTransform = gameObjectEscene.transform;
		currentTransform.Find("Icon").gameObject.SetActive(true);	
		shapeController._outlineScript.effectColor = _green;
		this._animationController.GetComponent<Animations>().shakeObject(gameObjectEscene.transform);

		if(shapeController.questionType ==  QuestionType.Type.StatementQuestion)
		{
		
			currentTransform.Find("WrongDialogueText").gameObject.SetActive(false);
			currentTransform.Find("RightDialogueText").gameObject.SetActive(true);
		}
    }
}
