using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShapeController : MonoBehaviour 
{
	[Header("Controladores")]
	public GameObject _mechanicController;

	[Header("Variables de colisión")]
	public bool _touchLine;
	public int _espectedShape;
	public QuestionType.Type questionType;
	public bool _wasWrongAnswered;

	[Header("Animación")]
	public Outline _outlineScript;
	private Sequence _notResponseSequence;

	[Header("FeedBack")]
	public bool _haveGoodResponseFeedback;
	public bool _haveBadResponseFeedback;
	public string _feedbackGoodResponse;
	public string _feedbackBadResponse;



	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		this._wasWrongAnswered = false;
	}

	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.tag.Equals("Line") && !this._touchLine && other.GetComponent<LineRendererController>().colisione &&
		other.gameObject.GetComponent<LineRendererController>()._colisionWhitInteractiveForm== false) 
		{
			other.gameObject.GetComponent<LineRendererController>()._colisionWhitInteractiveForm = true;
			this._touchLine = true;
			this._mechanicController.GetComponent<MechanicControllerChapter1>().compareResults(other.GetComponent<LineRendererController>(),this.gameObject);
		}	
	}
}
