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



	//Buttons
	[Header("Buttons")]
	public GameObject _backButton;
	public GameObject _socialNetworkButton;

	//Variables
	[Header("Variables")]
	public float _delayChapter;
	public Camera mainCamera;
	

	public GameObject _scrollViewMesagges;
	public GameObject _uiAnimationController;

	public void Start()
	{
		if(ViewController._fromChapter)
		{
			ViewController._fromChapter = false;
			this.updateView(_chapterListView, _titleScreenView, true);
		}

		if(this._titleScreenView.activeInHierarchy)
		{
			this._backButton.SetActive(false);
		}
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		if (Input.touchCount > 1 || Input.GetMouseButton(0)) 
		{
			if(_introductionView.activeInHierarchy)
				this.updateView(_chapterListView, _introductionView,true);
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
			this.updateView(this._happyTraveler,this._titleScreenView,true);
			this._scrollViewMesagges.SetActive(false);

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
			this.updateView(this._titleScreenView,this._introductionView,false);
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
				this.updateView(this._titleScreenView,this._happyTraveler,false);
				this._uiAnimationController.GetComponent<UIAnimationController>().outHappyTravelerView();

			}));
		}

		else if(this._chapterListView.activeInHierarchy)
		{
			this.updateView(this._titleScreenView,this._chapterListView,false);
		}
	}

	public void pressSocialNetworkButton()
	{
		this._contactView.SetActive(true);
	}	

	public void pressPlayButton()
	{
		this.updateView(this._introductionView,this._titleScreenView, false);
	}

	public void pressChapterButton(int chapterNumber)
	{
		// this.updateView(this._loadingView, this._chapterListView, false);
		var fader = new FadeTransition()
		{
			fadedDelay = 0.5f,
			fadeToColor = Color.black
		};
		TransitionKit.instance.transitionWithDelegate( fader );
		StartCoroutine(waitThenCallback(0.5f, () => 
		{ 
			this.updateView(this._loadingView,this._chapterListView,false);
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

	public void updateView(GameObject activeView, GameObject inactiveView, bool backButtonBool)
	{
		
		activeView.SetActive(true);
		inactiveView.SetActive(false);

		if(backButtonBool)
			this._backButton.SetActive(true);

		else
			this._backButton.SetActive(false);

	}

	private IEnumerator waitThenCallback(float time, Action callback)
	{
		yield return new WaitForSeconds(time);
		callback();
	}

	public void pressClose(GameObject gameObjectClose)
	{
		gameObjectClose.SetActive(false);
	}

	public void pressHowButton()
	{
		if(this._titleScreenView.activeInHierarchy)
		{
			this._howTitleScreenView.SetActive(true);
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

	
}
