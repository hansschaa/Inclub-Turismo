using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererController : MonoBehaviour 
{
	[Header("Variables")]
	public bool colisione = false;
	public bool _colisionWhitInteractiveForm;
	public Vector2 _centerLine;

	//0 para cruz
	//1 para tick
	//-1 para no es ninguna de las dos
	public int _typeShape;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		this._colisionWhitInteractiveForm = false;
	}

	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag.Equals("Line"))
		{
			this.colisione = true;
			return;
		}
	}	
}
