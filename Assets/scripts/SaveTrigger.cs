using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SaveTrigger : MonoBehaviour
{
    public GameObject TextPanel;
    public GameObject savingText;
    //public GameObject player;

    private bool isInsideTrigger = false;
    private bool messageReaded = false;
    private bool saving;
    private InputManager iM;
    private SavePlayerData save;
    private TankControls2 tank;
    public EventSystem eventSystem;
    public GameObject yesButton;
    public GameObject panelSave;
    private float timeCounter;
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        iM = GameObject.FindGameObjectWithTag("Manager").GetComponent<InputManager>();
        tank = GameObject.FindGameObjectWithTag("Player").GetComponent<TankControls2>();
        save = GetComponent<SavePlayerData>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isInsideTrigger && Input.GetButtonDown("Action") && !iM.isPaused && !iM.isInventoryOpened && !iM.isMapOpened && !saving)
        {
            Read();
        }
        if(saving)
        {
            if (timeCounter >= 3)
            {
                ReadEnd();
            }
            else timeCounter += Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") //solo funciona con player
        {
            isInsideTrigger = true; //cambia el bool
                                    //Debug.Log ("Enterd 2");
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
        panelSave.SetActive(true);
        savingText.SetActive(false);
        //Debug.Log("reading");
        messageReaded = true;
        //Time.timeScale = 0;
        iM.canPause = false;
        tank.canWalk = false;
        eventSystem.SetSelectedGameObject(yesButton);
        saving = false;
    }

    public void ReadEnd()
    {
        TextPanel.SetActive(false);
        savingText.SetActive(false);
        //Debug.Log("quit");
        messageReaded = false;
        //Time.timeScale = 1;
        iM.canPause = true;
        tank.canWalk = true;
        saving = false;
        timeCounter = 0;
    }
    public void Save()
    {
        save.SaveLevelData();
        saving = true;
    }
}
