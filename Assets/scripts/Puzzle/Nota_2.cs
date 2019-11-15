using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nota_2 : MonoBehaviour
{
    private bool isInsideTrigger = false;

    public GameObject TextPanel = null;
    public string message = "Hello World";
    public string message3 = "Hello World";
    public Text eText;
    private bool messageReaded = false;
    public bool getObj;
    private bool stRead = false;
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
            if (messageReaded && stRead)
            {
                ReadEnd();
            }
            else if(messageReaded && !stRead)
            {
                Read_2();
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
        //Debug.Log("reading");
        messageReaded = true;
        Time.timeScale = 0;
        getObj = true;
        iM.canPause = false;
        eText.text = message;
    }
    private void Read_2()
    {
        TextPanel.SetActive(true);
        sound.Play(this.gameObject, 0);
        hud.hasFragNote_2 = true;
        hud.SetKey();
        getObj = true;
        Data.SetNoteFrag_2();
        stRead = true;
        eText.text = message3;
    }
    private void ReadEnd()
    {
        TextPanel.SetActive(false);
        //Debug.Log("quit");
        messageReaded = false;
        getObj = true;
        Time.timeScale = 1;
        //gameObject.SetActive(false);
        iM.canPause = true;
    }
}
