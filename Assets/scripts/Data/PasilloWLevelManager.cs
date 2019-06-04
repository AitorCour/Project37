using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class PasilloW_Data
{
    public bool lader;
    public bool enemy;
    public bool door;
    public PasilloW_Data()
    {
        lader = true;
        enemy = true;
        door = false;
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
        if (data.lader == false)
        {
            laderObj.SetActive(false);
        }
    }
    public void NewGame()
    {
        data = new PasilloW_Data();
    }
    public void NewGameHall()
    {
        data_Hall = new Hall_Data();
    }
    void Start()
    {
        plBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyBehaviour3>();
        if (data.lader == true)
        {
            lader = GameObject.FindGameObjectWithTag("Misc").GetComponent<Lader>();
        }
        if(Data.IsKeyUnlock(0) == true && data.enemy == true)
        {
            //enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyBehaviour3>();
            enemyObj.transform.position = new Vector3(-2, 1, 0);
            //enemy.GetComponent<EnemyBehaviour3>().enabled = false;
            Debug.Log("ManequinActive");
        }
        else if(plBehaviour.key1 == false || !data.enemy)
        {
            //enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyBehaviour3>();
            enemyObj.transform.position = new Vector3(-20, 1, 0);
            //enemy.GetComponent<EnemyBehaviour3>().enabled = false;
            GameObject.Find("EnemyTocho").GetComponent<EnemyBehaviour3>().enabled = false;
            Debug.Log("NotActive");
        }
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
            if (/*enemy.enemyIsDead || */GameObject.Find("EnemyTocho").GetComponent<EnemyBehaviour3>().enemyIsDead == true)
            {
                data.enemy = false;
            }
        }
        if(door.doorOpened || data.door)
        {
            data_Hall.wDoorOpen = true;
            data.door = true;
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
