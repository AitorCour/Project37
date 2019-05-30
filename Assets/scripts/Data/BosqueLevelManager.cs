using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Bosque_Data
{
    public bool partida;

    public Bosque_Data()
    {
        partida = false;
    }
}

public class BosqueLevelManager : LevelManager
{
    public Bosque_Data data;
    public Segura_Data seguraData;
    public Playerdata playerData;
    public GameData gameData;
    public GameObject continueObj;
    public bool dataExist;

    protected override void Awake()
    {
        // Cargar si existe datos de Pasillo1

        try//nueva funcion, lo usaremos para comprobar si ha fallado
        {
            data = (Bosque_Data)DataManager.LoadFromText<Bosque_Data>("BosqueData", Application.persistentDataPath + "/Levels");
            Debug.Log("[GDM] Load succeed!");
        }
        catch (Exception e) //guarda el motivo de fallo en exception
        {
            Debug.Log("[GDM] Load error: " + e);
            NewGame();
        }
        try
        {
            seguraData = (Segura_Data)DataManager.LoadFromText<Segura_Data>("SeguraData", Application.persistentDataPath + "/Levels");
            playerData = (Playerdata)DataManager.LoadFromText<Playerdata>("Player", Application.persistentDataPath + "/Levels");
            dataExist = true;
        }
        catch (Exception e) //guarda el motivo de fallo en exception
        {
            Debug.Log("[GDM] Load error: " + e);
            dataExist = false;
            Debug.Log("Only new Game");
        }
        try
        {
            playerData = (Playerdata)DataManager.LoadFromText<Playerdata>("Player", Application.persistentDataPath + "/Levels");
            Debug.Log("[GDM] Load Player succeed!");
        }
        catch (Exception e) //guarda el motivo de fallo en exception
        {
            Debug.Log("[GDM] Load error: " + e);
        }
        // fileName = "Pasillo1Data"
        // Si existen, inicializar cambios dependiendo de los datos
        if (dataExist)
        {
            continueObj.SetActive(true);
        }
        else continueObj.SetActive(false);
    }
    public void NewGame()
    {
        data = new Bosque_Data();
    }
    void Start()
    {
    }

    public override void SaveLevelData()
    {
        try
        {
            DataManager.SaveToText<Bosque_Data>(data, "BosqueData", Application.persistentDataPath + "/Levels");
            Debug.Log("[GDM] Save succeed!");
        }
        catch (Exception e)
        {
            Debug.Log("[GDM] Save error: " + e);
        }
    }
}
