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
	public int _numberQuestionChapter;
	public bool _wasWrongAnswered;

	[Header("Animación")]
	public Outline _outlineScript;
	private Sequence _notResponseSequence;
	
	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		this._wasWrongAnswered = false;
		// startAnimation();

	}

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

	public void startAnimation()
	{
		// this._notResponseSequence = DOTween.Sequence();
		// TweenParams tParms = new TweenParams().SetLoops(-1);
		// this._notResponseSequence.Append(this._outlineScript.DOFade(0f,0.7f));
		// this._notResponseSequence.Append(this._outlineScript.DOFade(1,1f)).AppendInterval(0.3f);
		// this._notResponseSequence.SetAs(tParms);
		// this._notResponseSequence.Play();
	}

	public void finishAnimation()
	{
		// this._notResponseSequence.Kill();
	}
}
