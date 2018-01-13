using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicControllerChapter1 : MonoBehaviour
{
	[Header("Controllers")]
	public GameObject _chapter1ViewController;
	public GameObject _soundController;


	[Header("SubChapters")]
	public int _subChaptersCount;
	public int _currentPoints;
	public int _currentSubChapter;
	public int _maxPoints;
	public int _wrongAnswers;
	public int[,] _rangosRendimiento;
	
	void Start()
	{
		this._wrongAnswers = 0;
		this._maxPoints = 2;
		this._rangosRendimiento = new int[5,3];
		this._chapter1ViewController.GetComponent<Chapter1ViewController>().updateScoreText(_currentPoints,_maxPoints);
		this.loadRangesPerformance();
	}

    public void compareResults(int numeroSign, GameObject gameObjectEscene)
	{
		if(numeroSign == gameObjectEscene.GetComponent<ShapeController>()._espectedShape && gameObjectEscene.activeInHierarchy)
		{
			gameObjectEscene.transform.GetChild(gameObjectEscene.transform.childCount-1).gameObject.SetActive(true);
			this._currentPoints++;
			this._soundController.GetComponent<SoundController>().playResultAnswerSound(true);

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
			this._soundController.GetComponent<SoundController>().playResultAnswerSound(false);
			this._wrongAnswers++;
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
			this._chapter1ViewController.GetComponent<Chapter1ViewController>().updateFinalView(performance, stars);
		}

		else
		{
			for(int i = 0 ; i < 5; i++)
			{
				print("entro al for");
				if(performance >= this._rangosRendimiento[i,0] && performance <= this._rangosRendimiento[i,1])
				{
					
					stars = this._rangosRendimiento[i,2];
					this._chapter1ViewController.GetComponent<Chapter1ViewController>().updateFinalView(performance, stars);
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

	private void loadRangesPerformance()
    {
		this._rangosRendimiento[0,0]= 5;
		this._rangosRendimiento[0,1]= 25;
		this._rangosRendimiento[0,2]= 1;
		this._rangosRendimiento[1,0]= 25;
		this._rangosRendimiento[1,1]= 50;
		this._rangosRendimiento[1,2]= 2;
		this._rangosRendimiento[2,0]= 50;
		this._rangosRendimiento[2,1]= 75;
		this._rangosRendimiento[2,2]= 3;
		this._rangosRendimiento[3,0]= 75;
		this._rangosRendimiento[3,1]= 99;
		this._rangosRendimiento[3,2]= 4;
		this._rangosRendimiento[4,0]= 99;
		this._rangosRendimiento[4,1]= 100;
		this._rangosRendimiento[4,2]= 5;
    }
}
