using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Nina_Data
{
    public bool puzzle;
    public Nina_Data()
    {
        puzzle = true;
    }
}

public class NinaLevelManager : LevelManager
{
    public Nina_Data data;
    private ActiveSafe safe;
    public GameObject safeObj;
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
        // fileName = "Pasillo1Data"
        // Si existen, inicializar cambios dependiendo de los datos
        if (data.puzzle == false)
        {
            safeObj.SetActive(false);
        }
    }
    public void NewGame()
    {
        data = new Nina_Data();
    }
    void Start()
    {
        if (data.puzzle == true)
        {
            safe = GameObject.FindGameObjectWithTag("Misc").GetComponent<ActiveSafe>();
        }
    }

    public override void SaveLevelData()
    {
        if (data.puzzle == true)
        {
            if (safe.puzzleActive == false)
            {
                data.puzzle = false;
            }
        }

        try
        {
            DataManager.SaveToText<Nina_Data>(data, "NinaData", Application.persistentDataPath + "/Levels");
            Debug.Log("[GDM] Save succeed!");
        }
        catch (Exception e)
        {
            Debug.Log("[GDM] Save error: " + e);
        }
    }
}
