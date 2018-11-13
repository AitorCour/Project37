using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyMusic : MonoBehaviour 
{
	// Use this for initialization
	void Start ()
    {
        if(GameObject.FindGameObjectsWithTag("Music").Length > 1)
        {
            Destroy(this.gameObject);
        }
        else DontDestroyOnLoad(this.gameObject);
	}
}
