using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class Animations : MonoBehaviour 
{
	[Header("Controllers")]
	public GameObject _soundController;

	public float _pitch;

	// Use this for initialization
	void Start () {
		this._pitch = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void showStars(Transform starsParent,Sprite badStarImage, int estrellas)
	{
		print("estrellas : " + estrellas);
		Sequence showStarsSequence = DOTween.Sequence();
		// Sequence showBadStarsSequence = DOTween.Sequence();
		TweenParams tParms = new TweenParams().SetEase(Ease.InCirc);
		print("Buenas estrellas");

		for(int i = 0 ; i< estrellas ; i++)
		{
			print(i)			;
			showStarsSequence.Append(starsParent.GetChild(i).DOScale(1,0.5f).SetAs(tParms)).AppendCallback(soundGoodStars);
		}

		// showGoodStarsSequence.Play().OnComplete(()=> showBadStarsSequence.Play().OnComplete(()=> this._soundController.GetComponent<SoundController>().setPitch(1)));

		print("Malas estrellas");
		for(int j = estrellas; j < 5; j++)
		{
			print(j);
			starsParent.GetChild(j).GetComponent<Image>().sprite = badStarImage;
			showStarsSequence.Append(starsParent.GetChild(j).DOScale(1,0.5f).SetAs(tParms)).AppendCallback(soundBadStars);
		}

		showStarsSequence.Play().OnComplete(()=>this._soundController.GetComponent<SoundController>().setPitch(1));

	}

    private void soundBadStars()
    {
		this._soundController.GetComponent<SoundController>().setPitch(1f);
        this._soundController.GetComponent<SoundController>().playEffectSound(3);
	
    }

    private void soundGoodStars()
    {
        this._soundController.GetComponent<SoundController>().playEffectSound(2);
		this._pitch+= 0.3f;
		this._soundController.GetComponent<SoundController>().setPitch(this._pitch);
    }

    

    public void shakeObject(Transform go)
	{
		go.transform.DOShakeScale(1,0.5f,7,3,true);
	}

}
