using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeController : MonoBehaviour 
{
	public GameObject _mechanicController;
	public bool _touchLine;
	public int _espectedShape;

	public int _numberQuestionChapter;

	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag.Equals("Line") && !this._touchLine) 
		{
			other.gameObject.GetComponent<LineRendererController>()._colisionWhitInteractiveForm = true;
			this._touchLine = true;
			this._mechanicController.GetComponent<MechanicControllerChapter1>().compareResults(other.GetComponent<LineRendererController>()._typeShape,this.gameObject);
		}	
	}
}
