using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Segura_Data
{
    public bool potion;
    public bool ammo;

    public Segura_Data()
    {
        potion = true;
        ammo = true;
    }
}

public class SeguraLevelManager : LevelManager
{
    public Segura_Data data;
    public GameObject potionObj;
    public GameObject ammoObj;
    private Potion pot;
    private Munition munition;

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
        if(data.potion == false)
        {
            potionObj.SetActive(false);
        }
        if(data.ammo == false)
        {
            ammoObj.SetActive(false);
        }
    }
    public void NewGame()
    {
        data = new Segura_Data();
    }
    void Start()
    {
        if(data.potion == true)
        {
            pot = GameObject.FindGameObjectWithTag("cure").GetComponent<Potion>();
        }
        if (data.ammo == true)
        {
            munition = GameObject.FindGameObjectWithTag("ammo").GetComponent<Munition>();
        }
    }

    public override void SaveLevelData()
    {
        if(data.potion == true)
        {
            if (pot.getObj == true)
            {
                data.potion = false;
            }
        }
        
        if (data.ammo == true)
        {
            if (munition.getObj == true)
            {
                data.ammo = false;
            }
        }

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
