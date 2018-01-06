using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigController : MonoBehaviour 
{

	public bool _audioEncendido = true;
    private static ConfigController _instance;

    public static ConfigController Instance 
    { 
        get 
        { 
            return _instance; 
        } 
    } 

    private void Awake() 
    { 
        if (_instance != null && _instance != this) 
        { 
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    } 

	
	void Start()
	{
		if(!_audioEncendido)
			GameObject.Find("Main Camera").gameObject.GetComponent<AudioSource>().enabled = false;

        else
			GameObject.Find("Main Camera").gameObject.GetComponent<AudioSource>().enabled = true;
	}
}
