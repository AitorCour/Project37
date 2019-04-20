using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KeyDoor : MonoBehaviour 
{
	private PlayerBehaviour plBehaviour;
	public GameObject TextPanel = null;
	public int scene; //se introduce la scena a la que se quiere ir
	private bool isInsideTrigger = false;
	//public int key = 1; //Con esto se podrá poner cuantas llaves se necesitará para la puerta
	private bool isDoorOpen = false;
	private bool MessageReaded = false;
	public string message = "Hello World";
    public string message_2;
	public Text eText;

	//public Animator animator;
	//public Image black;
    private SoundObj soundObj;
    private ChangeScene changeSc;
    //Donde irá
    public float xPos;
    public float zPos;
	void Start()
	{
		plBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        soundObj = GetComponentInChildren<SoundObj>();
        changeSc = GameObject.FindGameObjectWithTag("Manager").GetComponent<ChangeScene>();
    }
	void Update()
	{
		if (isInsideTrigger && Input.GetButtonDown("Action"))
		{
			if(plBehaviour.key1 == true && !MessageReaded && !isDoorOpen)
			{
				//StartCoroutine(Fade());
				
				isDoorOpen = true;
                Read();
            }
			else if(isDoorOpen && !MessageReaded) // utilizar esto para si vuelve a la habitación o lo que sea, que la puerta ya esté abierta
			{
                //SceneManager.LoadScene(scene);
                soundObj.Play(2);
				//StartCoroutine(Fade());
                changeSc.FadeChangeScene(scene);
                changeSc.pX = xPos;
                changeSc.pZ = zPos;
                changeSc.SavePosition();
			}
			else if (MessageReaded)
			{
				ReadEnd();
			}
			else Read();
		}
	}

	void OnTriggerEnter(Collider other)
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
	}
	private void Read()
	{
		TextPanel.SetActive(true);
		Debug.Log("reading");
		MessageReaded = true;
		Time.timeScale = 0;
        if (!isDoorOpen)
        {
            eText.text = message;
            soundObj.Play(0);
        }
        else
        {
            eText.text = message_2;
            soundObj.Play(1);
        }
    }
	private void ReadEnd()
	{
		TextPanel.SetActive(false);
		Debug.Log("quit");
		MessageReaded = false;
		Time.timeScale = 1;
	}
	 /*IEnumerator Fade()
	 {
		animator.SetBool("Fade", true);
		yield return new WaitUntil(()=>black.color.a==1);
		SceneManager.LoadScene(scene);
	 }*/
}