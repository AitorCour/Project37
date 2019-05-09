﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Pasillo_1_Data
{
    public bool enemyDead;

    public Pasillo_1_Data()
    {
        enemyDead = false;
    }
}

public class PasilloLevelManager : LevelManager
{
    public Pasillo_1_Data data;
    private EnemyBehaviour3 enemy;
    public GameObject enemyObj;
    
    protected override void Awake()
    {
        // Cargar si existe datos de Pasillo1
        try//nueva funcion, lo usaremos para comprobar si ha fallado
        {
            data = (Pasillo_1_Data)DataManager.LoadFromText<Pasillo_1_Data>("Pasillo1Data", Application.persistentDataPath + "/Levels");
            Debug.Log("[GDM] Load succeed!");
        }
        catch (Exception e) //guarda el motivo de fallo en exception
        {
            Debug.Log("[GDM] Load error: " + e);
            NewGame();
        }
        // fileName = "Pasillo1Data"
        // Si existen, inicializar cambios dependiendo de los datos
    }
    public void NewGame()
    {
        data = new Pasillo_1_Data();
    }
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyBehaviour3>();
    }

    public override void SaveLevelData()
    {
        //Debug.Log(enemy.enemyIsDead);
        // Actualizar datos
        if (enemy.enemyIsDead)
        {
            //data.enemyDead = true;
            Debug.Log("Save");
        }
        // Guardarlos
        try
        {
            DataManager.SaveToText<Pasillo_1_Data>(data, "Pasillo1Data", Application.persistentDataPath + "/Levels");
            Debug.Log("[GDM] Save succeed!");
        }
        catch (Exception e)
        {
            Debug.Log("[GDM] Save error: " + e);
        }
        // Cambiar de escenas
    }
}
