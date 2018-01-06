using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class UIAnimationController : MonoBehaviour 
{
	//Sequences
	public Sequence _inHappyTravelerSequence;
	

	[Header("ImagesUpButton")]
	public Sprite _soundOnImage;
	public Sprite _soundOffImage; 

	[Header("HappyTravelerSceneTransforms")]
	public Transform _book;
	public Transform _woodTexture;
	public GameObject _scrollView;
	public Transform _pencil;
	public RectTransform _contentScrollView;

	[Header("Variables")]
	public GameObject _viewController; 

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

		else if(buttonTransform.gameObject.name.Equals("ExitButton"))
		{
			_viewController.GetComponent<ViewController>().pressExitButton();

		}

		else if(buttonTransform.gameObject.name.Equals("HowButton"))
		{
			_viewController.GetComponent<ViewController>().pressHowButton();
		}
	}

	public void inHappyTravelerView()
	{
		this._inHappyTravelerSequence = DOTween.Sequence();

		this._inHappyTravelerSequence.Append(this._book.DOScale(2.2f,1));
		this._inHappyTravelerSequence.Join(this._woodTexture.DOScale(1.5f,1));
		this._inHappyTravelerSequence.Join(this._pencil.DOScale(2f,1));
		this._inHappyTravelerSequence.Join(this._pencil.DOMove(Camera.main.ViewportToWorldPoint(new Vector2(1.5f,0)),1));

		this._inHappyTravelerSequence.Play().OnComplete(()=> _scrollView.SetActive(true));
	}

	public void outHappyTravelerView()
	{
		this._book.transform.localScale= new Vector3(1,1,1);
		this._pencil.transform.localScale= new Vector3(1,1,1);
		this._pencil.transform.localPosition = new Vector3(94,-172,0);
		// this._contentScrollView.transform.position = new Vector3(this._contentScrollView.transform.position.x, -154 );
		this._scrollView.SetActive(false);
	}


}
