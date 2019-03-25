using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LaderPos : MonoBehaviour
{
    private bool isInsideTrigger;
    private InputManager iM;
    private PlayerBehaviour plBehaviour;
    public GameObject laderObj;

    public GameObject laderButton;

    public GameObject TextPanel = null;
	public string message = "Hello World";
	public Text eText;
	private bool MessageReaded = false;

    // Use this for initialization
    void Start ()
    {
        plBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        iM = GameObject.FindGameObjectWithTag("Manager").GetComponent<InputManager>();
	}

    void Update ()
    {
        if (isInsideTrigger && Input.GetButtonDown("Action") && !iM.isPaused && !iM.isInventoryOpened && !iM.isMapOpened)
        {
            if (MessageReaded)
            {
                ReadEnd();
            }
            else Read();
        }
        if(plBehaviour.lader)
        {
            if(isInsideTrigger)
            {
                laderButton.SetActive(true);
            }
            else laderButton.SetActive(false);
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
    public void UseLader()
    {
        laderObj.transform.position = this.transform.position;
        plBehaviour.LoseLader();
    }
}
