using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class PasilloW_Data
{
    public bool lader;

    public PasilloW_Data()
    {
        lader = true;
    }
}

public class PasilloWLevelManager : LevelManager
{
    public PasilloW_Data data;
    public GameObject laderObj;
    private Lader lader;

    protected override void Awake()
    {
        // Cargar si existe datos de Pasillo1

        try//nueva funcion, lo usaremos para comprobar si ha fallado
        {
            data = (PasilloW_Data)DataManager.LoadFromText<PasilloW_Data>("PasilloWData", Application.persistentDataPath + "/Levels");
            Debug.Log("[GDM] Load succeed!");
        }
        catch (Exception e) //guarda el motivo de fallo en exception
        {
            Debug.Log("[GDM] Load error: " + e);
            NewGame();
        }
        // fileName = "Pasillo1Data"
        // Si existen, inicializar cambios dependiendo de los datos
        if (data.lader == false)
        {
            laderObj.SetActive(false);
        }
    }
    public void NewGame()
    {
        data = new PasilloW_Data();
    }
    void Start()
    {
        if (data.lader == true)
        {
            lader = GameObject.FindGameObjectWithTag("Misc").GetComponent<Lader>();
        }
    }

    public override void SaveLevelData()
    {
        if (data.lader == true)
        {
            if (lader.getObj == true)
            {
                data.lader = false;
            }
        }
        try
        {
            DataManager.SaveToText<PasilloW_Data>(data, "PasilloWData", Application.persistentDataPath + "/Levels");
            Debug.Log("[GDM] Save succeed!");
        }
        catch (Exception e)
        {
            Debug.Log("[GDM] Save error: " + e);
        }
    }
}
