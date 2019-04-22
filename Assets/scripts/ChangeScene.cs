using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //poner esto en todo lo que vaya a cambiar de escena
using UnityEngine.UI;


public class ChangeScene : MonoBehaviour
{
    public int scene; //se introduce la scena a la que se quiere ir
    //private bool isInsideTrigger = false;

    public Image black;
    public Animator animator;
    private GameObject player;

    public float pX;
    public float pZ;

    //public float plGoingX;
    //public float plGoingZ;

    //private SettingsMenu settings;
    //private AudioSource audioSource;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //audioSource = GetComponent<AudioSource>();
        //settings = GameObject.FindGameObjectWithTag("HUD").GetComponent<SettingsMenu>();
        pX = PlayerPrefs.GetFloat("p_x");
        pZ = PlayerPrefs.GetFloat("p_z");
        player.transform.position = new Vector3(pX, 1, pZ);
    }

    /*void Update()
    {
        if (isInsideTrigger && Input.GetButtonDown("Action"))
        {
            Debug.Log ("Change Scene");
            audioSource.Play();
            //SceneManager.LoadScene(scene); //linea que hace que funcione
            //Fade();
            //settings.SaveVolume();
            StartCoroutine(Fade());
            pX = plGoingX;
            pZ = plGoingZ;

            SavePosition();
        }
        //Debug.Log(pX);
    }*/

    /*void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isInsideTrigger = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isInsideTrigger = false;
        }
    }*/
    //Change Scene In Menu
    /*public void ChangeToScene (int num)
    {
        SceneManager.LoadScene(num);
    }*/
    public void FadeChangeScene(int num)
    {
        StartCoroutine(FadeButton(num));
    }
    public void Death()
    {
        StartCoroutine(FadeDead());
    }
    public void ExitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }
    /*public void*/
    IEnumerator Fade()
    {
        //animator.SetTrigger("FadeOut");
        //SceneManager.LoadScene(scene);
        animator.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(scene);
    }
    IEnumerator FadeButton(int num)
    {
        animator.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        //settings.SaveVolume();
        SceneManager.LoadScene(num);
    }
    IEnumerator FadeDead()
    {
        //animator.SetTrigger("FadeOut");
        //SceneManager.LoadScene(scene);
        animator.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        //settings.SaveVolume();
        SceneManager.LoadScene(7);
    }

    public void SavePosition()
    {
        PlayerPrefs.SetFloat("p_x", pX);
        PlayerPrefs.SetFloat("p_z", pZ);
    }
}


