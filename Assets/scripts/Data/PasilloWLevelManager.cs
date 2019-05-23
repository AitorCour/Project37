using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class PasilloW_Data
{
    public bool lader;
    public bool enemy;
    public PasilloW_Data()
    {
        lader = true;
        enemy = true;
    }
}

public class PasilloWLevelManager : LevelManager
{
    public PasilloW_Data data;
    public Hall_Data data_Hall;
    public GameObject laderObj;
    private Lader lader;
    private EnemyBehaviour3 enemy;
    public GameObject enemyObj;
    private PlayerBehaviour plBehaviour;
    private NormalDoor door;
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
        plBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        if (data.lader == true)
        {
            lader = GameObject.FindGameObjectWithTag("Misc").GetComponent<Lader>();
        }
        if(plBehaviour.key1 && data.enemy)
        {
            enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyBehaviour3>();
            enemyObj.SetActive(true);
        }
        else enemyObj.SetActive(false);
        door = GameObject.FindGameObjectWithTag("door").GetComponent<NormalDoor>();
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
        if(plBehaviour.key1 && data.enemy)
        {
            if (enemy.enemyIsDead)
            {
                data.enemy = false;
            }
        }
        if(door.doorOpened)
        {
            data_Hall.wDoorOpen = true;
        }
        try
        {
            DataManager.SaveToText<PasilloW_Data>(data, "PasilloWData", Application.persistentDataPath + "/Levels");
            DataManager.SaveToText<Hall_Data>(data_Hall, "HallData", Application.persistentDataPath + "/Levels");
            Debug.Log("[GDM] Save succeed!");
        }
        catch (Exception e)
        {
            Debug.Log("[GDM] Save error: " + e);
        }
    }
}
