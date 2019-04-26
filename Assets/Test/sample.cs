using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sample : MonoBehaviour {

	public float timeCounter;
	public int scene;
	private float videoTime = 5.0f;
    public Image black;
    public Animator animator;
    // Use this for initialization
    void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () {
		timeCounter += Time.deltaTime;
		if(timeCounter >= videoTime)
		{
			SceneManager.LoadScene(scene);
			//FadeChangeScene();
		}
	}
    public void FadeChangeScene()
    {
        StartCoroutine(FadeButton());
    }
    IEnumerator FadeButton()
    {
        animator.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        //settings.SaveVolume();
        SceneManager.LoadScene(scene);
    }
}
