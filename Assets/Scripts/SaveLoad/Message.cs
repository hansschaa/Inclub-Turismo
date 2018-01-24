using System;
using System.Collections;
using UnityEngine;

[System.Serializable]
public class Message 
{
	public string _message;
	public int _starsMessage;
	public int _portraitImageIndex;

	public DateTime _dateMessage;

	public Message(string message, int starsMessage, int portraitImageIndex, DateTime dateMessage)
	{
		this._message = message;
		this._starsMessage = starsMessage;
		this._portraitImageIndex = portraitImageIndex;
		this._dateMessage = dateMessage;
	}
}
