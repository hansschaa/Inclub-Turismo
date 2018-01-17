using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Prime31.TransitionKit;
using DG.Tweening;

public class Chapter1ViewController : MonoBehaviour 
{
	
	[Header("Controllers")]
	public GameObject _mechanicController;
	public GameObject _soundController;
	public GameObject _animationsController;


	[Header("Sub Chapters")]
	public GameObject[] _subChaptersView;
	

	[Header("Final View")]
	public GameObject _finalChapterUI;
	public Text _wrongAnswersText;
	public Text _rightAnswersText;
	public Text _starsFinalChapterText;
	public Text _performanceText;
	public Transform _starsGroup;
	public Sprite _badStarImage;


	[Header("Boss Panel View")]
	public GameObject _bossPanel;
	public Text _bossTextPanel;
	public GameObject _fadeBoss;


	[Header("Other canvas elements")]
	public Text _currentPointsText;
	public GameObject _permanentElements;

	public void pressReturnToChapterButton()
	{
		ViewController._fromChapter = true;
		SceneManager.LoadScene(1);
	}

	public void closePanel(GameObject panel)
	{
		this._fadeBoss.SetActive(false);
		panel.SetActive(false);
	}

	public void updateScoreText(int currentPoints, int maxPoints)
	{
		this._currentPointsText.text = currentPoints + "/" + maxPoints;
	}

	public void showBossPanel(int numberQuestionChapter)
	{
		this._fadeBoss.SetActive(true);
		for(int i = 0 ; i < 6; i++)
		{
			if(i==numberQuestionChapter)
			{
				this._bossTextPanel.text = BaseDeDatos._bossFeedBack[i];
				break;
			}
		}
		this._bossPanel.SetActive(true);
	}

    public void updateFinalView(float promedio, int estrellas, int wrongAnswers, int rightAnswers)
    {
		this._permanentElements.SetActive(false);
		this._subChaptersView[this._subChaptersView.Length-1].gameObject.SetActive(false);
		this._finalChapterUI.SetActive(true);

		this._animationsController.GetComponent<Animations>().showStars(_starsGroup,_badStarImage,estrellas);

		this._rightAnswersText.text = "" + rightAnswers;
		this._wrongAnswersText.text = "" + wrongAnswers;
    }


    public void loadNewView(int currentSubChapter)
    {
		var fader = new FadeTransition()
		{
			fadedDelay = 0.5f,
			fadeToColor = Color.black
		};

		TransitionKit.instance.transitionWithDelegate( fader );
		StartCoroutine(GestureController.waitThenCallback(1, () => 
		{ 
			this._subChaptersView[currentSubChapter-1].gameObject.SetActive(false);
			this._subChaptersView[currentSubChapter].gameObject.SetActive(true);
			
		}));
    }

    public void loadScene(int scene)
	{
		SceneManager.LoadScene(scene);
	}

	public void playSound(int soundEffect)
	{
		this._soundController.GetComponent<SoundController>().playEffectSound(soundEffect);
	}
}
