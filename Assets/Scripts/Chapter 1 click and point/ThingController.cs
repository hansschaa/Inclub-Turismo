using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingController : MonoBehaviour 
{
	[Header("Controladores")]
	public GameObject _mechanicController;
	public GameObject _chapterViewController;
	
	[Header("Scripts Controladores")]
	private MechanicController _mechanicControllerObject;
	private ChapterViewController _chapterViewControllerObject;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		this._mechanicControllerObject = this._mechanicController.GetComponent<MechanicController>();
		this._chapterViewControllerObject = this._chapterViewController.GetComponent<ChapterViewController>();
	}

	public void OnMouseDown()
	{
		print("On");
		if(_mechanicControllerObject._currentGO == null || this.gameObject != _mechanicControllerObject._currentGO)
		{
			if(this._mechanicControllerObject._currentGO!=null)
			{
				print("Ya había un objeto con anterioridad");
				this._chapterViewControllerObject.offOutline(this._mechanicControllerObject._currentGO);
			}

			this._chapterViewControllerObject.onOutline(this.gameObject);
			this._mechanicControllerObject.setCurrentGO(this.gameObject);

			// if(this.GetComponent<SpriteOutline>().enabled)
			// 	this.GetComponent<SpriteOutline>().enabled = false;

			// else
			// 	this.GetComponent<SpriteOutline>().enabled = true;
			
			this._chapterViewControllerObject.switchAnswerPanel(true);

		}
	}	
}
