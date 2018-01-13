using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Prime31.TransitionKit;

public class ViewController : MonoBehaviour 
{

	//Global Variables
	public static bool _fromChapter;
	[Header("Controllers")]
	public GameObject _soundController;
	
	//Views
	[Header("Views")]
	public GameObject _titleScreenView;
	public GameObject _contactView;
	public GameObject _chapterListView;
	public GameObject _introductionView;
	public GameObject _happyTraveler;
	public GameObject _loadingView;
	public GameObject _howTitleScreenView;
	public GameObject _howHappyTraveler;
	public GameObject _howChapterListView;

	public GameObject _playText3D;

	public Color32 colorText;
	public Color32 colorTextAlpha;



	//Buttons
	[Header("Buttons")]
	public GameObject _backButton;
	public GameObject _socialNetworkButton;

	//Variables
	[Header("Variables")]
	public float _delayChapter;
	public Camera mainCamera;
	public GameObject _globalPointsUI;
	

	public GameObject _scrollViewMesagges;
	public GameObject _uiAnimationController;

	public void Start()
	{
		if(ViewController._fromChapter)
		{
			ViewController._fromChapter = false;
			this.updateView(_chapterListView, _titleScreenView, true,true);
		}

		if(this._titleScreenView.activeInHierarchy)
		{
			this._backButton.SetActive(false);
		}
	}

	public void pressHappyTraveler()
	{
		var fader = new FadeTransition()
		{
			fadedDelay = 0.2f,
			fadeToColor = Color.black
		};
		TransitionKit.instance.transitionWithDelegate( fader );
		StartCoroutine(waitThenCallback(0.5f, () => 
        { 
			this.updateView(this._happyTraveler,this._titleScreenView,true,false);
			// this._scrollViewMesagges.SetActive(false);

			StartCoroutine(waitThenCallback(0.5f, () => 
			{ 
				this._uiAnimationController.GetComponent<UIAnimationController>().inHappyTravelerView();
			}));
		}));
	}

	public void pressBackButton()
	{
		if(this._introductionView.activeInHierarchy)
		{
			this.updateView(this._titleScreenView,this._introductionView,false,true);
		}

		else if(this._happyTraveler.activeInHierarchy)
		{
			var fader = new FadeTransition()
			{
				fadedDelay = 0.2f,
				fadeToColor = Color.black
			};
			TransitionKit.instance.transitionWithDelegate( fader );
			StartCoroutine(waitThenCallback(0.5f, () => 
			{ 
				this.updateView(this._titleScreenView,this._happyTraveler,false,true);
				this._uiAnimationController.GetComponent<UIAnimationController>().outHappyTravelerView();

			}));
		}

		else if(this._chapterListView.activeInHierarchy)
		{
			this.updateView(this._titleScreenView,this._chapterListView,false,true);
		}
	}

	public void pressSocialNetworkButton()
	{
		this._playText3D.GetComponent<TextMesh>().color = this.colorTextAlpha;
		this._contactView.SetActive(true);
	}	

	public void pressPlayButton()
	{
		this.updateView(this._introductionView,this._titleScreenView, false,false);
	}

	public void pressChapterButton(int chapterNumber)
	{
		var fader = new FadeTransition()
		{
			fadedDelay = 0.5f,
			fadeToColor = Color.black
		};
		TransitionKit.instance.transitionWithDelegate( fader );
		StartCoroutine(waitThenCallback(0.5f, () => 
		{ 
			this.updateView(this._loadingView,this._chapterListView,false,false);
			StartCoroutine(waitThenCallback(3f, () => 
			{ 
				fader = new FadeTransition()
				{
					fadedDelay = 0.5f,
					fadeToColor = Color.black
				};

				TransitionKit.instance.transitionWithDelegate( fader );
				StartCoroutine(waitThenCallback(1f, () => 
				{
					SceneManager.LoadScene(chapterNumber);
				}));
			}));

		}));
	}

	public void pressExitButton()
	{
		Application.Quit();
	}

	public void pressNextButton()
	{
		this.updateView(this._chapterListView, this._introductionView,true,true);
	}

	public void updateView(GameObject activeView, GameObject inactiveView, bool backButtonBool, bool globalPointsUIBool)
	{
		activeView.SetActive(true);
		inactiveView.SetActive(false);

		this._backButton.SetActive(backButtonBool);

		this._globalPointsUI.SetActive(globalPointsUIBool);
	}

	private IEnumerator waitThenCallback(float time, Action callback)
	{
		yield return new WaitForSeconds(time);
		callback();
	}

	public void pressClose(GameObject gameObjectClose)
	{
		if(this._titleScreenView.activeInHierarchy)
		{
			this._playText3D.GetComponent<TextMesh>().color = this.colorText;
		}

		gameObjectClose.SetActive(false);
	}

	public void pressHowButton()
	{
		if(this._titleScreenView.activeInHierarchy)
		{
			this._howTitleScreenView.SetActive(true);
			this._playText3D.GetComponent<TextMesh>().color = this.colorTextAlpha;
			
		}

		else if(this._happyTraveler.activeInHierarchy)
		{
			this._howHappyTraveler.SetActive(true);
		}

		else if(this._chapterListView.activeInHierarchy)
		{
			this._howChapterListView.SetActive(true);
		}
	}

	public void playSound(int soundEffect)
	{
		this._soundController.GetComponent<SoundController>().playEffectSound(soundEffect);
	}
	
}
