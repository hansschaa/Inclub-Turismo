using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class UIAnimationController : MonoBehaviour 
{
	//Sequences
	public Sequence _inHappyTravelerSequence;
	public Sequence _linkTextSequence;

	[Header("ImagesUpButton")]
	public Sprite _soundOnImage;
	public Sprite _soundOffImage; 

	[Header("HappyTravelerSceneTransforms")]
	public RectTransform _computer;
	public GameObject _fadeElements;

	[Header("Variables")]
	public GameObject _viewController; 
	public Text spriteRandom;
	public GameObject _mainCamera;
	public void OnPressDownButton(Transform buttonTransform)
	{
		buttonTransform.DOScale(0.85f,0.1f);
	}

	public void OnPressUpButton(Transform buttonTransform)
	{
		
		buttonTransform.DOScale(1,0.1f);

		if(buttonTransform.gameObject.name.Equals("SoundButton"))
		{
			ConfigController.Instance._audioEncendido = !ConfigController.Instance._audioEncendido;
			if(!ConfigController.Instance._audioEncendido)
			{
				buttonTransform.gameObject.GetComponent<Image>().sprite = this._soundOffImage;
				this._mainCamera.GetComponent<AudioSource>().enabled = false;
			}
				
			else
			{
				buttonTransform.gameObject.GetComponent<Image>().sprite = this._soundOnImage;
				this._mainCamera.GetComponent<AudioSource>().enabled = true;
			}		
		}

		else if(buttonTransform.gameObject.name.Equals("SocialButton"))
		{
			_viewController.GetComponent<ViewController>().pressSocialNetworkButton();
		}

		else if(buttonTransform.gameObject.name.Equals("HowButton"))
		{
			_viewController.GetComponent<ViewController>().pressHowButton();
		}

		else if(buttonTransform.gameObject.name.Equals("InclubFelizButton"))
		{
			_viewController.GetComponent<ViewController>().pressHappyTraveler();
		}
	}

	public void inHappyTravelerView()
	{
		this._inHappyTravelerSequence = DOTween.Sequence();
		this._inHappyTravelerSequence.Append(this._computer.DOScale(1.9f,1f));
		this._inHappyTravelerSequence.Play().OnComplete(()=> this._fadeElements.SetActive(true));
		this._inHappyTravelerSequence.Play();

		TweenParams tParms = new TweenParams().SetLoops(-1);
		this._linkTextSequence = DOTween.Sequence();
		this._linkTextSequence.Append(this.spriteRandom.DOFade(0f,0.2f)).AppendInterval(0.5f);
		this._linkTextSequence.Append(this.spriteRandom.DOFade(1f,.2f)).AppendInterval(0.3f);
		this._linkTextSequence.SetAs(tParms);

		_linkTextSequence.Play();
	}

	public void outHappyTravelerView()
	{
		this._computer.transform.localScale= new Vector3(1,1,1);
		this._fadeElements.SetActive(false);
	}

}
