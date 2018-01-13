using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Prime31.TransitionKit;

public class Chapter1ViewController : MonoBehaviour 
{

	[Header("Sub Chapters")]
	public GameObject[] _subChaptersView;
	

	[Header("Other Canvas")]
	public GameObject _canvasOptions;
	public GameObject _finalChapterUI;
	public Text _currentPointsText;
	public Text _starsFinalChapterText;
	public Text _performanceText;
	public GameObject _bossPanel;
	public Text _bossTextPanel;
	public GameObject _fadeBoss;

	[Header("Controllers")]
	public GameObject _mechanicController;
	public GameObject _soundController;


	public String[] _bossFeedBack;
	public GameObject _permanentElements;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		this._bossFeedBack = new String[6];
		this.loadFeedBack();
	}

    public void pressOptionsButton()
	{
		this._canvasOptions.SetActive(true);
	}

	public void pressResumeButton()
	{
		this._canvasOptions.SetActive(false);
	}

	public void pressReturnToChapterButton()
	{
		// this._soundController.GetComponent<SoundController>().playEffectSound(0);
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
				this._bossTextPanel.text = _bossFeedBack[i];
				break;
			}
		}
		this._bossPanel.SetActive(true);
	}

   


    public void updateFinalView(float promedio, int estrellas)
    {
		this._permanentElements.SetActive(false);
		this._subChaptersView[this._subChaptersView.Length-1].gameObject.SetActive(false);
		this._finalChapterUI.SetActive(true);
		this._starsFinalChapterText.text = "Lograste " + estrellas + " estrellas";
		this._performanceText.text = "Rendimiento: " + promedio + "%"; 
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
		// this._soundController.GetComponent<SoundController>().playEffectSound(0);
		SceneManager.LoadScene(scene);
	}

	public void playSound(int soundEffect)
	{
		this._soundController.GetComponent<SoundController>().playEffectSound(soundEffect);
	}


    private void loadFeedBack()
    {
        this._bossFeedBack[0]="Al atender una persona en situación de discapacidad visual debemos presentarnos e indicar nuestro cargo.";
		this._bossFeedBack[1]="Debemos utilizar un lenguaje concreto, que permita dar puntos de orientación o de referencia";
		this._bossFeedBack[2]="FEEDBACK";
		this._bossFeedBack[3]="FEEDBACK";
		this._bossFeedBack[4]="FEEDBACK";
		this._bossFeedBack[5]="FEEDBACK";
    }
}
