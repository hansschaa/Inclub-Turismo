using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Prime31.TransitionKit;
using TMPro;

public class ViewController : MonoBehaviour 
{

	//Global Variables
	public static bool _fromChapter;
	[Header("Controllers")]
	public GameObject _soundController;
	public GameObject _uiAnimationController;
	
	//Views
	[Header("Views")]
	public GameObject _titleScreenView;
	public GameObject _exitPanelView;
	public GameObject _contactView;
	public GameObject _chapterListView;
	public GameObject _introductionView;
	public GameObject _happyTraveler;
	public GameObject _loadingView;
	public GameObject _howTitleScreenView;
	public GameObject _howHappyTraveler;
	public GameObject _howChapterListView;
	public GameObject _creditstView;
	public GameObject _playText3D;

	[Header("LoadingView")]
	public Text _adviceLoadingText;
	public float _tiempoLoading;

	[Header("Inclub Hotel")]
	public RectTransform _contentScrollView;
	public GameObject _messagePrefab;
	private const int ELEMENTSPACE = 100;
	public Sprite[] _portraitsMessageImages;
	public bool _loadMessages;
	public Sprite _entireStar;
	

	//Buttons
	[Header("Buttons")]
	public GameObject _backButton;
	public GameObject _socialNetworkButton;
	

	//Variables
	[Header("Variables")]
	public float _delayChapter;
	public Camera mainCamera;
	public GameObject _globalPointsUI;
	public GameObject _doorTitleScreen;
	public Color32 colorText;
	public Color32 colorTextAlpha;

	[Header("Buttons")]
	public GameObject _notificationButton;

	// public GameObject _scrollViewMesagges;


	public void Start()
	{

		

		this._notificationButton.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("notReadNotifications").ToString();

		this._loadMessages = false;

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

    public void pressCreditsButton()
    {
		this._playText3D.GetComponent<TextMesh>().color = this.colorTextAlpha;
        this._creditstView.SetActive(true);
    }

    public void pressHappyTraveler()
	{

		this._notificationButton.GetComponent<Button>().interactable = false;

		var fader = new FadeTransition()
		{
			fadedDelay = 0.2f,
			fadeToColor = Color.black
		};
		TransitionKit.instance.transitionWithDelegate( fader );
		StartCoroutine(waitThenCallback(0.5f, () => 
        { 
			if(this._titleScreenView.activeInHierarchy)
				this.updateView(this._happyTraveler,this._titleScreenView,true,true);

			else if(this._chapterListView.activeInHierarchy)
			{
				SetupMainScene.desdeChapterList = true;
				this.updateView(this._happyTraveler,this._chapterListView,true,true);
			}
				


			StartCoroutine(waitThenCallback(0.5f, () => 
			{ 
				this._uiAnimationController.GetComponent<UIAnimationController>().inHappyTravelerView();
				
			}));
		}));
	}

    public void loadMessages()
    {
		if(!this._loadMessages)
		{
			PlayerPrefs.SetInt("notReadNotifications",0);
			this._notificationButton.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("notReadNotifications").ToString();

			this._loadMessages = true;
			SaveLoad.Load();
			if(SaveLoad.savedMessages.Count!=0)
			{
				for(int i = 0 ; i < SaveLoad.savedMessages.Count; i++)
				{
					GameObject mensaje = Instantiate(this._messagePrefab, this._contentScrollView.transform.position, Quaternion.identity, this._contentScrollView) as GameObject;
					mensaje.transform.Find("MessageText").gameObject.GetComponent<TextMeshProUGUI>().text = SaveLoad.savedMessages[i]._message;
					mensaje.transform.Find("PerfilImage").gameObject.GetComponent<Image>().sprite = this._portraitsMessageImages[SaveLoad.savedMessages[i]._portraitImageIndex];
					mensaje.transform.Find("DateText").gameObject.GetComponent<TextMeshProUGUI>().text = SaveLoad.savedMessages[i]._dateMessage.ToString();
					int stars = SaveLoad.savedMessages[i]._starsMessage;
					Transform starsGroup = mensaje.transform.Find("StarsGroup").gameObject.transform;
					
					for(int j = 0 ; j < stars ; j++)
					{
						starsGroup.GetChild(j).gameObject.GetComponent<Image>().sprite = this._entireStar;
					}

					this._contentScrollView.sizeDelta+= new Vector2(0, ELEMENTSPACE);
				}
			}
		}	
    }

    public void pressBackButton()
	{
		if(this._introductionView.activeInHierarchy)
		{
			this.updateView(this._titleScreenView,this._introductionView,false,true);
		}

		else if(this._happyTraveler.activeInHierarchy)
		{
			this._notificationButton.GetComponent<Button>().interactable = true;
			var fader = new FadeTransition()
			{
				fadedDelay = 0.2f,
				fadeToColor = Color.black
			};
			TransitionKit.instance.transitionWithDelegate( fader );
			StartCoroutine(waitThenCallback(0.5f, () => 
			{ 
				if(SetupMainScene.desdeChapterList)
				{
					this.updateView(this._chapterListView,this._happyTraveler,true,true);
					SetupMainScene.desdeChapterList = false;
				}

				else
				{
					this.updateView(this._titleScreenView,this._happyTraveler,false,true);
				}
			
				this._uiAnimationController.GetComponent<UIAnimationController>().outHappyTravelerView();

			}));
		}

		else if(this._chapterListView.activeInHierarchy)
		{
			this._doorTitleScreen.GetComponent<Animator>().SetBool("Open", false);
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
		this._doorTitleScreen.GetComponent<Animator>().SetBool("Open", true);
		StartCoroutine(waitThenCallback(0.5f, () => 
		{ 
			this.updateView(this._introductionView,this._titleScreenView, false,false);
			
		}));
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
			this._adviceLoadingText.text = BaseDeDatos._consejosLoading[UnityEngine.Random.Range(0,BaseDeDatos._consejosLoading.Length)];
			this.updateView(this._loadingView,this._chapterListView,false,false);
			StartCoroutine(asynchronousLoadingChapter(chapterNumber));
		}));
	}

    private IEnumerator asynchronousLoadingChapter(int chapterNumber)
    {
		AsyncOperation async = SceneManager.LoadSceneAsync(chapterNumber);
		async.allowSceneActivation = false;

		yield return new WaitForSeconds(3f);
		var fader = new FadeTransition()
		{
			fadedDelay = 0.5f,
			fadeToColor = Color.black
		};

		TransitionKit.instance.transitionWithDelegate( fader );
		yield return new WaitForSeconds(1f);
		print("Debria acctivar la cosa");
		async.allowSceneActivation = true;
    }

    public void openPanelAction(GameObject panelToOpen)
	{
		panelToOpen.SetActive(true);
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

		else if(gameObjectClose.name.Equals("Credits"))
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

	public void openBrowserPageButton(String link)
	{	
		Application.OpenURL(link);
	}
	
}
