using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Potion : MonoBehaviour 
{
	private PlayerBehaviour plBehaviour;
	//public GameObject potionObject;
	public bool isInsideTrigger = false;
	private int potion = 1;
	public GameObject TextPanel = null;
	public string message = "Hello World";
	public Text eText;
	public bool messageReaded = false;
	private SoundObj sound;
    private InputManager iM;
    public bool getObj;
    private BoxCollider box;
    private MeshRenderer cureMat;
	private ParticleSystem brillo;
	// Use this for initialization
	void Start () 
	{
		plBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
		sound = GetComponentInChildren<SoundObj>();
		iM = GameObject.FindGameObjectWithTag("Manager").GetComponent<InputManager>();
        box = GetComponent<BoxCollider>();
        cureMat = GetComponentInChildren<MeshRenderer>();
		brillo = GetComponentInChildren<ParticleSystem>();
	}
    void Update()
    {
        if (isInsideTrigger && Input.GetButtonDown("Action") && !iM.isPaused && !iM.isInventoryOpened && !iM.isMapOpened)
        {
            //plBehaviour.GetKeys(key);
            //keyObject.SetActive(false);
            if (messageReaded)
            {
                ReadEnd();
            }
            else Read();
        }
        else return;
    }
    void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") //solo funciona con player
		{
			isInsideTrigger = true; //cambia el bool
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			isInsideTrigger = false;
		}
	}
	public void Read()
	{
		TextPanel.SetActive(true);
		eText.text = message;
		//Debug.Log("reading");
		messageReaded = true;
		Time.timeScale = 0;
		plBehaviour.GetPotions(potion);
		sound.Play(this.gameObject, 0);
        iM.canPause = false;
    }
	public void ReadEnd()
	{
		TextPanel.SetActive(false);
		//Debug.Log("quit");
		messageReaded = false;
		Time.timeScale = 1;
        getObj = true;
		brillo.Stop();
        //gameObject.SetActive(false);
        box.enabled = false;
        cureMat.enabled = false;
        isInsideTrigger = false;
        iM.canPause = true;
    }
}
