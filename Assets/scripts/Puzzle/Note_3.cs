using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note_3 : MonoBehaviour
{
    private bool isInsideTrigger = false;

    public GameObject TextPanel = null;
    public string message = "Hello World";
    public Text eText;
    private bool MessageReaded = false;
    public bool getObj;
    private SoundObj sound;
    private InputManager iM;
    private HUD hud;
    // Use this for initialization
    void Start()
    {
        sound = GetComponentInChildren<SoundObj>();
        iM = GameObject.FindGameObjectWithTag("Manager").GetComponent<InputManager>();
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInsideTrigger && Input.GetButtonDown("Action") && !iM.isPaused && !iM.isInventoryOpened && !iM.isMapOpened)
        {
            if (MessageReaded)
            {
                ReadEnd();
            }
            else Read();
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
        Debug.Log("reading");
        MessageReaded = true;
        Time.timeScale = 0;
        sound.Play(0);
        hud.hasNote_2 = true;
        hud.SetKey();
        Data.SetNote_2();
        iM.canPause = false;
    }
    private void ReadEnd()
    {
        TextPanel.SetActive(false);
        Debug.Log("quit");
        MessageReaded = false;
        getObj = true;
        Time.timeScale = 1;
        gameObject.SetActive(false);
        iM.canPause = true;
    }
}
