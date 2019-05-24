using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    protected override void Awake()
    {
        // Cargar si existe datos de Pasillo1

        try//nueva funcion, lo usaremos para comprobar si ha fallado
        {
            data = (Hall_Data)DataManager.LoadFromText<Hall_Data>("HallData", Application.persistentDataPath + "/Levels");
            Debug.Log("[GDM] Load succeed!");
        }
        catch (Exception e) //guarda el motivo de fallo en exception
        {
            Debug.Log("[GDM] Load error: " + e);
            NewGame();
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
