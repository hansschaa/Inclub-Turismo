using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour 
{
	public AudioClip _correctAnswerSound;
	public AudioClip _incorrectAnswerSound;
	public AudioClip[] _soundEffects;

	public void playResultAnswerSound(bool correct)
	{
		if(correct)
			this.gameObject.GetComponent<AudioSource>().PlayOneShot(this._correctAnswerSound);

		else
			this.gameObject.GetComponent<AudioSource>().PlayOneShot(this._incorrectAnswerSound);
	}

	public void playEffectSound(int soundEffect)
	{
		
		for(int i = 0 ; i< this._soundEffects.Length;i++)
		{
			if(soundEffect == i)
			{
				this.gameObject.GetComponent<AudioSource>().PlayOneShot(this._soundEffects[i]);
				break;
			}
		}
	}

	public void setPitch(float pitch)
	{
		this.gameObject.GetComponent<AudioSource>().pitch = pitch;
	}

	
}
