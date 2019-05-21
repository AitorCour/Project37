using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Hall_Data
{
    public bool wDoorOpen;
    public bool nDoorOpen;
    public Hall_Data()
    {
        wDoorOpen = false;
        nDoorOpen = false;
    }
}

public class HallLevelManager : LevelManager
{
    public Hall_Data data;
    private DoorBlocked door;
    private DoorBlocked door2;
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

        if (data.wDoorOpen)
        {
            door.isDoorOpen = true;
        }
        else door.isDoorOpen = false;
        if (data.nDoorOpen)
        {
            door2.isDoorOpen = true;
        }
        else door2.isDoorOpen = false;
    }

    public override void SaveLevelData()
    {
        /*if (data.nota == true)
        {
            if (note.getObj == true)
            {
                data.nota = false;
            }
        }*/

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
