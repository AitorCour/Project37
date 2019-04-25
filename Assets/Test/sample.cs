using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;
public class sample : MonoBehaviour {

	public float timeCounter;
	public int scene;
	private float videoTime = 5.0f;
	private ChangeScene chScene;
	
	// Use this for initialization
	void Start () 
	{
		//timeCounter += Time.deltaTime;
		chScene = GetComponent<ChangeScene>();
	}
	
	// Update is called once per frame
	void Update () {
		timeCounter += Time.deltaTime;
		if(timeCounter >= videoTime)
		{
			//SceneManager.LoadScene(scene);
			chScene.FadeChangeScene(scene);
		}
	}
}
