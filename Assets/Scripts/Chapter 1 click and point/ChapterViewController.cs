using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChapterViewController : MonoBehaviour 
{
	[Header("Controladores")]
	public GameObject _mechanicController;

	[Header("Scripts Controlares")]
	private MechanicController _mechanicControllerObject;

	[Header("Variables Animaciones")]
	public GameObject _showAnswerPanel;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		this._mechanicControllerObject = _mechanicController.GetComponent<MechanicController>();
	}


    public void offOutline(GameObject go)
    {
        go.GetComponent<SpriteOutline>().enabled = false;
    }

	public void onOutline(GameObject go)
    {
        go.GetComponent<SpriteOutline>().enabled = true;
    }

    internal void switchAnswerPanel(Boolean action)
    {
		this._showAnswerPanel.SetActive(action);
		
    }
}
