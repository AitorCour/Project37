using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LaderPos : MonoBehaviour
{
    private InputManager iM;
    private PlayerBehaviour plBehaviour;
    private ChangeScene changeSc;
    public GameObject lader;
    public GameObject laderButton;

    public int scene;
    public GameObject TextPanel = null;
	public string message = "Hello World";
	public Text eText;
	private bool MessageReaded = false;
    public bool laderActive;
    private bool isInsideTrigger;
    public bool used;
    public float xPos;
    public float zPos;
    public Pasillo1_Data dataPas;
    // Use this for initialization
    void Start ()
    {
        plBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        iM = GameObject.FindGameObjectWithTag("Manager").GetComponent<InputManager>();
        changeSc = GameObject.FindGameObjectWithTag("Manager").GetComponent<ChangeScene>();
        //used = false;
    }

    void Update ()
    {
        if (isInsideTrigger && Input.GetButtonDown("Action") && !iM.isPaused && !iM.isInventoryOpened && !iM.isMapOpened && !laderActive)
        {
            if (MessageReaded)
            {
                ReadEnd();
            }
            else Read();
        }
        if(plBehaviour.lader == true)
        {
            if(isInsideTrigger)
            {
                laderButton.SetActive(true);
            }
            else laderButton.SetActive(false);
        }
        if(laderActive && isInsideTrigger && Input.GetButtonDown("Action") && !iM.isPaused && !iM.isInventoryOpened && !iM.isMapOpened)
        {
            ChangeScene();
            used = true;
        }
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
    private void Read()
	{
		TextPanel.SetActive(true);
		eText.text = message;
		MessageReaded = true;
		Time.timeScale = 0;
        iM.canPause = false;
    }
	private void ReadEnd()
	{
		TextPanel.SetActive(false);
		MessageReaded = false;
		Time.timeScale = 1;
        iM.canPause = true;
    }
    public void ActiveLader()
    {
        lader.SetActive(true);//se activa el obj de lader
        laderActive = true;
        
    }
    void ChangeScene()
    {
        changeSc.FadeChangeScene(scene);
        changeSc.pX = xPos;
        changeSc.pZ = zPos;
        changeSc.SavePosition();
    }
}
