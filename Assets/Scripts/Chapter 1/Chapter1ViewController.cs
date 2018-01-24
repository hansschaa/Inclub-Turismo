using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Prime31.TransitionKit;
using DG.Tweening;
using TMPro;

public class Chapter1ViewController : MonoBehaviour 
{
	
	[Header("Controllers")]
	public GameObject _mechanicController;
	public MechanicControllerChapter1 _mechanic;
	public GameObject _soundController;
	public GameObject _animationsController;

	public GestureController gestureController;

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
	public TextMeshProUGUI _bossTextPanel;
	public GameObject _fadeBoss;


	[Header("Other canvas elements")]
	public Text _currentPointsText;
	public GameObject _permanentElements;
	public GameObject _tutorial;
	public GameObject _panelAuxiliar;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		if(PlayerPrefs.GetInt("isPlayChapter" + this._mechanic._currentChapter)  != 0)
			this._tutorial.SetActive(false);

		this._mechanic = this._mechanicController.GetComponent<MechanicControllerChapter1>();
	}

	public void pressReturnToChapterButton()
	{
		ViewController._fromChapter = true;
		SceneManager.LoadScene(1);
	}

	public void close(GameObject panel)
	{
		panel.SetActive(false);
	}

	public void closePanel(GameObject panel)
	{
		if(_mechanic._nextSubChapter==false)
		{
			this._mechanic.xButtonResetLevel();
			this._mechanic._nextSubChapter = true;
		}
			
	
		this._fadeBoss.SetActive(false);
		panel.SetActive(false);
	}

	public void updateScoreText(int currentPoints, float maxPoints)
	{
		this._currentPointsText.text = currentPoints + "/" + maxPoints;
	}

	public void showOjoPanel(GameObject panel)
	{
		if(this._mechanic._subChapterKey==3)
		{
			this._panelAuxiliar.SetActive(true);
		}

		else
		{
			panel.SetActive(true);
		}	
	}

	public void showBossPanel(string feedBackText, Color colorPanel)
	{
		this._fadeBoss.SetActive(true);
		this._bossPanel.GetComponent<Image>().color = colorPanel;
		this._bossTextPanel.text = feedBackText;
		this._bossPanel.SetActive(true);
	}

    public void updateFinalView(float promedio, int estrellas, int wrongAnswers, int globalPointsChapter)
    {
		PlayerPrefs.SetInt(("isPlayChapter") + this._mechanic._currentChapter,1);
		this.SendMessage(promedio, estrellas);
		StartCoroutine(GestureController.waitThenCallback(0.8f, () => 
		{
			var fader = new FadeTransition()
			{
				fadedDelay = 0.5f,
				fadeToColor = Color.black
			};

			TransitionKit.instance.transitionWithDelegate( fader );

			StartCoroutine(GestureController.waitThenCallback(0.8f, () => 
			{

				this._permanentElements.SetActive(false);
				this._subChaptersView[this._subChaptersView.Length-1].gameObject.SetActive(false);
				this._finalChapterUI.SetActive(true);
				this._rightAnswersText.text = "" + globalPointsChapter;
				this._wrongAnswersText.text = "" + wrongAnswers;

				StartCoroutine(GestureController.waitThenCallback(1f, () => 
				{

					this._permanentElements.SetActive(false);
					this._subChaptersView[this._subChaptersView.Length-1].gameObject.SetActive(false);
					this._finalChapterUI.SetActive(true);

					this._animationsController.GetComponent<Animations>().showStars(_starsGroup,_badStarImage,estrellas);

					
				}));
			}));

		}));
    }

    private void SendMessage(float promedio, int estrellas)
    {

		Message message;

        if(promedio==100)
			message = new Message(BaseDeDatos._messagesClients1[UnityEngine.Random.Range(0, BaseDeDatos._messagesClients1.Length)], estrellas, UnityEngine.Random.Range(0,4), DateTime.Now);
		

		else if (promedio > 75 && promedio <= 99)
			message = new Message(BaseDeDatos._messagesClients2[UnityEngine.Random.Range(0, BaseDeDatos._messagesClients2.Length)], estrellas, UnityEngine.Random.Range(0,4), DateTime.Now);
		

		else if (promedio >50 && promedio <= 75)
			message = new Message(BaseDeDatos._messagesClients3[UnityEngine.Random.Range(0, BaseDeDatos._messagesClients3.Length)], estrellas, UnityEngine.Random.Range(0,4), DateTime.Now);
		

		else if (promedio > 25 && promedio <= 50)
			message = new Message(BaseDeDatos._messagesClients4[UnityEngine.Random.Range(0, BaseDeDatos._messagesClients4.Length)], estrellas, UnityEngine.Random.Range(0,4), DateTime.Now);
	
		else
			message = new Message(BaseDeDatos._messagesClients5[UnityEngine.Random.Range(0, BaseDeDatos._messagesClients5.Length)], estrellas, UnityEngine.Random.Range(0,4), DateTime.Now);
		

		PlayerPrefs.SetInt("notReadNotifications", PlayerPrefs.GetInt("notReadNotifications") + 1);
		SaveLoad.Save(message);
    }	

    public void loadNewView(int currentSubChapter, int currentPoints, float maxPoints)
    {

		StartCoroutine(GestureController.waitThenCallback(1.2f, () => 
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
				this._currentPointsText.text = currentPoints + "/" + maxPoints;
			}));

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
