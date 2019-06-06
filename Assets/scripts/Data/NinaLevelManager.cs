using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Nina_Data
{
    public bool puzzle;
    public bool door;
    public Nina_Data()
    {
        puzzle = true;
        door = false;
    }
}

public class NinaLevelManager : LevelManager
{
    public Nina_Data data;
    public Hall_Data data_Hall;
    private ActiveSafe safe;
    public GameObject safeObj;
    private KeyDoor_2 door;
    protected override void Awake()
    {
        // Cargar si existe datos de Pasillo1

        try//nueva funcion, lo usaremos para comprobar si ha fallado
        {
            data = (Nina_Data)DataManager.LoadFromText<Nina_Data>("NinaData", Application.persistentDataPath + "/Levels");
            Debug.Log("[GDM] Load succeed!");
        }
        catch (Exception e) //guarda el motivo de fallo en exception
        {
            Debug.Log("[GDM] Load error: " + e);
            NewGame();
        }
        try//nueva funcion, lo usaremos para comprobar si ha fallado
        {
            data_Hall = (Hall_Data)DataManager.LoadFromText<Hall_Data>("HallData", Application.persistentDataPath + "/Levels");
            Debug.Log("[GDM] Load succeed!");
        }
        catch (Exception e) //guarda el motivo de fallo en exception
        {
            Debug.Log("[GDM] Load error: " + e);
            NewGameHall();
        }
        // fileName = "Pasillo1Data"
        // Si existen, inicializar cambios dependiendo de los datos
        if (data.puzzle == false && Data.IsPuzzle2Solved() == true)
        {
            safeObj.SetActive(false);
        }
        else if (Data.IsPuzzle2Solved() == false)
        {
            data.puzzle = true;
            NewGame();
        }
    }
    public void NewGame()
    {
        data = new Nina_Data();
    }
    public void NewGameHall()
    {
        data_Hall = new Hall_Data();
    }
    void Start()
    {
        if (data.puzzle == true)
        {
            safe = GameObject.FindGameObjectWithTag("Misc").GetComponent<ActiveSafe>();
        }
        door = GameObject.FindGameObjectWithTag("door").GetComponent<KeyDoor_2>();
    }

    public override void SaveLevelData()
    {
        if (data.puzzle == true)
        {
            if (safe.puzzleActive == false)
            {
                data.puzzle = false;
                Data.Puzzle2Solved();
            }
        }
        if (door.isDoorOpen || data.door)
        {
            data_Hall.nDoorOpen = true;
            data.door = true;
        }
        try
        {
            DataManager.SaveToText<Nina_Data>(data, "NinaData", Application.persistentDataPath + "/Levels");
            DataManager.SaveToText<Hall_Data>(data_Hall, "HallData", Application.persistentDataPath + "/Levels");
            Debug.Log("[GDM] Save succeed!");
        }
        catch (Exception e)
        {
            Debug.Log("[GDM] Save error: " + e);
        }
    }
}
