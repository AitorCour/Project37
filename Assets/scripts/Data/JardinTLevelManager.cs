using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class JardinT_Data
{
    public bool potion;

    public JardinT_Data()
    {
        potion = true;
    }
}

public class JardinTLevelManager : LevelManager
{
    public JardinT_Data data;
    public GameObject potionObj;
    private Potion pot;

    protected override void Awake()
    {
        // Cargar si existe datos de Pasillo1

        try//nueva funcion, lo usaremos para comprobar si ha fallado
        {
            data = (JardinT_Data)DataManager.LoadFromText<JardinT_Data>("JardinTData", Application.persistentDataPath + "/Levels");
            Debug.Log("[GDM] Load succeed!");
        }
        catch (Exception e) //guarda el motivo de fallo en exception
        {
            Debug.Log("[GDM] Load error: " + e);
            NewGame();
        }
        // fileName = "Pasillo1Data"
        // Si existen, inicializar cambios dependiendo de los datos
        if (data.potion == false)
        {
            potionObj.SetActive(false);
        }
    }
    public void NewGame()
    {
        data = new JardinT_Data();
    }
    void Start()
    {
        if (data.potion == true)
        {
            pot = GameObject.FindGameObjectWithTag("cure").GetComponent<Potion>();
        }
    }

    public override void SaveLevelData()
    {
        if (data.potion == true)
        {
            if (pot.getObj == true)
            {
                data.potion = false;
            }
        }
        try
        {
            DataManager.SaveToText<JardinT_Data>(data, "JardinTData", Application.persistentDataPath + "/Levels");
            Debug.Log("[GDM] Save succeed!");
        }
        catch (Exception e)
        {
            Debug.Log("[GDM] Save error: " + e);
        }
    }
}
