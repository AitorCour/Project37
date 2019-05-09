using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Segura_Data
{
    public bool potion;

    public Segura_Data()
    {
        potion = true;
    }
}

public class SeguraLevelManager : LevelManager
{
    public Segura_Data data;
    //private EnemyBehaviour3 enemy;
    public GameObject potionObj;

    protected override void Awake()
    {
        // Cargar si existe datos de Pasillo1

        try//nueva funcion, lo usaremos para comprobar si ha fallado
        {
            data = (Segura_Data)DataManager.LoadFromText<Segura_Data>("SeguraData", Application.persistentDataPath + "/Levels");
            Debug.Log("[GDM] Load succeed!");
        }
        catch (Exception e) //guarda el motivo de fallo en exception
        {
            Debug.Log("[GDM] Load error: " + e);
            NewGame();
        }
        // fileName = "Pasillo1Data"
        // Si existen, inicializar cambios dependiendo de los datos
        if(!data.potion)
        {
            potionObj.SetActive(false);
        }
    }
    public void NewGame()
    {
        data = new Segura_Data();
    }
    void Start()
    {
        //enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyBehaviour3>();
    }

    public override void SaveLevelData()
    {
        data.potion = false;
        try
        {
            DataManager.SaveToText<Segura_Data>(data, "SeguraData", Application.persistentDataPath + "/Levels");
            Debug.Log("[GDM] Save succeed!");
        }
        catch (Exception e)
        {
            Debug.Log("[GDM] Save error: " + e);
        }
    }
}
