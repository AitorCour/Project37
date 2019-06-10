using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class sample : MonoBehaviour
{

	public float timeCounter;
	public int scene;
	public float videoTime = 35.0f;
    public Image black;
    public Animator animator;
    public bool intro = false;
    public Image introImage = null;
    // Use this for initialization
    void Start () 
	{
        if(intro)
        {
            introImage.DOFade(1f, 1f).SetDelay(5f);
            introImage.DOFade(0f, 1f).SetDelay(35f);
        }
	}
	
	// Update is called once per frame
	void Update () {
		timeCounter += Time.deltaTime;
        if(Input.GetButtonDown("Action") || Input.GetKeyDown(KeyCode.Return))
        {
            ChangeScene();
        }
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
    void ChangeScene()
    {
        SceneManager.LoadScene(scene);
    }
}
