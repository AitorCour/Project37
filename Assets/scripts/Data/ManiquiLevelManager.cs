using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Maniqui_Data
{
    public bool enemyDead;
    public bool ammo;

    public Maniqui_Data()
    {
        enemyDead = false;
        ammo = true;
    }
}

public class ManiquiLevelManager : LevelManager
{
    public Maniqui_Data data;
    private EnemyBehaviour3 enemy;
    public GameObject enemyObj;
    public GameObject ammoObj;
    private Munition munition;
    protected override void Awake()
    {
        // Cargar si existe datos de Pasillo1
        try//nueva funcion, lo usaremos para comprobar si ha fallado
        {
            data = (Maniqui_Data)DataManager.LoadFromText<Maniqui_Data>("ManiquiData", Application.persistentDataPath + "/Levels");
            Debug.Log("[GDM] Load succeed!");
        }
        catch (Exception e) //guarda el motivo de fallo en exception
        {
            Debug.Log("[GDM] Load error: " + e);
            NewGame();
        }
        // fileName = "Pasillo1Data"
        // Si existen, inicializar cambios dependiendo de los datos
        if (data.enemyDead)
        {
            enemyObj.SetActive(false);
            enemy = null;
        }
        if (data.ammo == false)
        {
            ammoObj.SetActive(false);
        }
    }
    public void NewGame()
    {
        data = new Maniqui_Data();
    }
    void Start()
    {
        if (!data.enemyDead)
        {
            enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyBehaviour3>();
        }
        if (data.ammo == true)
        {
            munition = GameObject.FindGameObjectWithTag("ammo").GetComponent<Munition>();
        }
    }

    public override void SaveLevelData()
    {
        // Actualizar datos
        if (enemy != null)
        {
            if (enemy.enemyIsDead)
            {
                data.enemyDead = true;
            }
        }
        if (data.ammo == true)
        {
            if (munition.getObj == true)
            {
                data.ammo = false;
            }
        }
        // Guardarlos
        try
        {
            DataManager.SaveToText<Maniqui_Data>(data, "ManiquiData", Application.persistentDataPath + "/Levels");
            Debug.Log("[GDM] Save succeed!");
        }
        catch (Exception e)
        {
            Debug.Log("[GDM] Save error: " + e);
        }
        // Cambiar de escenas
    }
}
