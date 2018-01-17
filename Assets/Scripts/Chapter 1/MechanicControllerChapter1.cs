using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MechanicControllerChapter1 : MonoBehaviour
{
	[Header("Controllers")]
	public GameObject _chapter1ViewController;
	public GameObject _soundController;
	public GameObject _animationController;



	[Header("SubChapters")]
	public int _subChaptersCount;
	public int _currentPoints;
	public int _currentSubChapter;
	public int _maxPoints;
	public int _wrongAnswers;
	public int _totallyQuestions;
	
	void Start()
	{
		this._wrongAnswers = 0;
		this._maxPoints = 2;
		this._chapter1ViewController.GetComponent<Chapter1ViewController>().updateScoreText(_currentPoints,_maxPoints);
	}

    public void compareResults(int numeroSign, GameObject gameObjectEscene)
	{
		if(numeroSign == gameObjectEscene.GetComponent<ShapeController>()._espectedShape && gameObjectEscene.activeInHierarchy)
		{
			this._currentPoints++;
			gameObjectEscene.GetComponent<ShapeController>().finishAnimation();
			this._soundController.GetComponent<SoundController>().playResultAnswerSound(true);
			this.changeTextView(gameObjectEscene);


			if(this._currentPoints == this._maxPoints)
			{
				this._currentSubChapter++;
				
				if(this._currentSubChapter < this._subChaptersCount)
				{
					print("Cargando view");
					this.loadNewChapterData();
				}

			  	else 
				{
					print("Cargando view final");
					this.backEndFinalData();
				}
			}

			else
			{
				this._chapter1ViewController.GetComponent<Chapter1ViewController>().updateScoreText(_currentPoints,_maxPoints);
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
			this._chapter1ViewController.GetComponent<Chapter1ViewController>().showBossPanel(gameObjectEscene.GetComponent<ShapeController>()._numberQuestionChapter);
			gameObjectEscene.GetComponent<ShapeController>()._touchLine = false;
		}
		
	}
    private void loadNewChapterData()
    {
        this._currentPoints=0;
		this._chapter1ViewController.GetComponent<Chapter1ViewController>().loadNewView(this._currentSubChapter);
		this.updateBackEndParameters(this._currentSubChapter);
    }

    private void backEndFinalData()
    {
		float wrongAverage = (this._wrongAnswers*100)/6;
		float performance = 100 - wrongAverage;
		int stars;

		if(performance<5)
		{
			stars= 0;
			this._chapter1ViewController.GetComponent<Chapter1ViewController>().updateFinalView(performance, stars, this._wrongAnswers,this._totallyQuestions);
		}

		else
		{
			for(int i = 0 ; i < 5; i++)
			{
				print("entro al for");
				if(performance >= BaseDeDatos._rangosRendimiento[i,0] && performance <= BaseDeDatos._rangosRendimiento[i,1])
				{
					
					stars = BaseDeDatos._rangosRendimiento[i,2];
					this._chapter1ViewController.GetComponent<Chapter1ViewController>().updateFinalView(performance, stars, this._wrongAnswers,this._totallyQuestions);
					print("backenf");
					if(PlayerPrefs.GetInt("LocalStarsChapter1") < stars)
					{
						PlayerPrefs.SetInt("LocalStarsChapter1", stars);
					}

					break;
				}
			}
		}
    }

    private void updateBackEndParameters(int currentSubChapter)
    {
		this._currentPoints = 0;
		this._maxPoints = 4;
		this._chapter1ViewController.GetComponent<Chapter1ViewController>().updateScoreText(_currentPoints,_maxPoints);
    }

	

	private void changeTextView(GameObject gameObjectEscene)
    {
		ShapeController shapeController = gameObjectEscene.GetComponent<ShapeController>();
		Transform currentTransform = gameObjectEscene.transform;
		currentTransform.Find("Sign").gameObject.SetActive(true);

		if(shapeController.questionType ==  QuestionType.Type.StatementQuestion && shapeController._espectedShape==0)
		{
			this._animationController.GetComponent<Animations>().shakeObject(gameObjectEscene.transform);
			currentTransform.Find("WrongTextMessage").gameObject.SetActive(false);
			currentTransform.Find("RightTextMessage").gameObject.SetActive(true);
			currentTransform.Find("Sign").gameObject.SetActive(true);
		}

    }
}
