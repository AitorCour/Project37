using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
[System.Serializable]
public class Hall_Data
{
    public bool wDoorOpen;
    public bool nDoorOpen;
    public bool aDoorOpen;
    public Hall_Data()
    {
        wDoorOpen = false;
        nDoorOpen = false;
        aDoorOpen = false;
    }
}

public class HallLevelManager : LevelManager
{
    public Hall_Data data;
    private DoorBlocked door;
    private DoorBlocked door2;
    private DoorBlocked door3;
    private TankControls2 tank;
    private InputManager inM;
    private bool firstTime;
    public GameObject barrasCine;//Barras de cinematica
    public GameObject TextPanel = null;
    public string message = "Hello World";
    public Text eText;
    protected override void Awake()
    {
        // Cargar si existe datos de Pasillo1

        try//nueva funcion, lo usaremos para comprobar si ha fallado
        {
            data = (Hall_Data)DataManager.LoadFromText<Hall_Data>("HallData", Application.persistentDataPath + "/Levels");
            Debug.Log("[GDM] Load succeed!");
            firstTime = false;
        }
        catch (Exception e) //guarda el motivo de fallo en exception
        {
            Debug.Log("[GDM] Load error: " + e);
            NewGame();
            firstTime = true;
        }
        // fileName = "Pasillo1Data"
        // Si existen, inicializar cambios dependiendo de los datos
        /*if (data.nota == false)
        {
            noteObj.SetActive(false);
        }*/
    }
    public void NewGame()
    {
        data = new Hall_Data();
    }
    void Start()
    {
        
        door = GameObject.FindGameObjectWithTag("TextReader").GetComponent<DoorBlocked>();
        door2 = GameObject.FindGameObjectWithTag("cure").GetComponent<DoorBlocked>();
        door3 = GameObject.FindGameObjectWithTag("ammo").GetComponent<DoorBlocked>();
        tank = GameObject.FindGameObjectWithTag("Player").GetComponent<TankControls2>();
        inM = GetComponent<InputManager>();
        if (data.wDoorOpen)
        {
            door.isDoorOpen = true;
        }
        if (data.nDoorOpen)
        {
            door2.isDoorOpen = true;
        }
        if (data.aDoorOpen)
        {
            door3.isDoorOpen = true;
        }
        if(firstTime)
        {
            eText.text = message;
            barrasCine.SetActive(true);
            TextPanel.SetActive(true);
            tank.canWalk = false;
            inM.canPause = false;
            StartCoroutine(WaitForReload());
        }
    }
    private IEnumerator WaitForReload()
    {
        yield return new WaitForSeconds(5);
        barrasCine.SetActive(false);
        TextPanel.SetActive(false);
        tank.canWalk = true;
        inM.canPause = true;
    }
    public override void SaveLevelData()
    {
        if (door.isDoorOpen == true)
        {
            data.wDoorOpen = true;
        }
        if (door2.isDoorOpen == true)
        {
            data.nDoorOpen = true;
        }
        if (door3.isDoorOpen == true)
        {
            data.aDoorOpen = true;
        }

        try
        {
            DataManager.SaveToText<Hall_Data>(data, "HallData", Application.persistentDataPath + "/Levels");
            Debug.Log("[GDM] Save succeed!");
        }
        catch (Exception e)
        {
            Debug.Log("[GDM] Save error: " + e);
        }
    }
}
