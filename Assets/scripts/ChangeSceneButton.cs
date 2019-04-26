using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //poner esto en todo lo que vaya a cambiar de escena
using UnityEngine.UI;

public class ChangeSceneButton : MonoBehaviour
{
    public int scene;
    public Image black;
    public Animator animator;
    public void FadeChangeScene(int num)
    {
        StartCoroutine(FadeButton(num));
    }
    IEnumerator FadeButton(int num)
    {
        animator.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        //settings.SaveVolume();
        SceneManager.LoadScene(num);
    }
    public void ExitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }
}
