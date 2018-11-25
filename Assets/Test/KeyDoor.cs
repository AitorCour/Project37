using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KeyDoor : MonoBehaviour 
{
	private PlayerBehaviour plBehaviour;
	public GameObject TextPanel = null;
	public int scene; //se introduce la scena a la que se quiere ir
	private bool isInsideTrigger = false;
	public int key = 1; //Con esto se podrá poner cuantas llaves se necesitará para la puerta
	private bool isDoorOpen = false;
	private bool MessageReaded = false;
	public string message = "Hello World";
	public Text eText;

	public Animator animator;
	public Image black;
	void Start()
	{
		plBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
	}
	void Update()
	{
		if (isInsideTrigger && Input.GetButtonDown("Action"))
		{
			if(plBehaviour.keys >= key)
			{
				//Debug.Log ("Change Scene");
				//SceneManager.LoadScene(scene); //cambio scene
				StartCoroutine(Fade());
				plBehaviour.LoseKeys(key); //player pierde una o lo que se necesiten de llaves
				isDoorOpen = true;
			}
			else if(isDoorOpen) // utilizar esto para si vuelve a la habitación o lo que sea, que la puerta ya esté abierta
			{
				//SceneManager.LoadScene(scene);
				StartCoroutine(Fade());
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
		eText.text = message;
		Debug.Log("reading");
		MessageReaded = true;
		Time.timeScale = 0;
	}
	private void ReadEnd()
	{
		TextPanel.SetActive(false);
		Debug.Log("quit");
		MessageReaded = false;
		Time.timeScale = 1;
	}
	 IEnumerator Fade()
	{
		animator.SetBool("Fade", true);
		yield return new WaitUntil(()=>black.color.a==1);
		SceneManager.LoadScene(scene);
	}
}
