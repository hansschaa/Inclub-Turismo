using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicController : MonoBehaviour 
{
    [Header("Controladores")]
    public GameObject _chapterViewController;
	
	[Header("Scripts Controladores")]
	private ChapterViewController _chapterViewControllerObject;
	public GameObject _currentGO;

    //Variables
    private RaycastHit2D _hit;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        this._currentGO = null;
        this._chapterViewControllerObject = this._chapterViewController.GetComponent<ChapterViewController>();
    }

    public void resetVariables()
    {
        this._chapterViewControllerObject.offOutline(_currentGO);
        this._currentGO = null;
        this._chapterViewControllerObject.switchAnswerPanel(false);
    }

    public void setCurrentGO(GameObject go)
    {
        this._currentGO = go;
    }

    public void updateColliders(GameObject gameObjectView1, GameObject gameObjectView2)
    {
        print("gameObjectView1");
        foreach(Transform child in gameObjectView1.transform)
        {
            print(child.gameObject.name);
        }

        print("gameObjectView2");
        foreach(Transform child in gameObjectView2.transform)
        {
            print(child.gameObject.name);
        }

    }
}
