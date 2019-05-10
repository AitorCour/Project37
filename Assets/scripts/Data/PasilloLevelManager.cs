using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Pasillo1_Data
{
    public bool enemyDead;

    public Pasillo1_Data()
    {
        enemyDead = false;
    }
}

public class PasilloLevelManager : LevelManager
{
    public Pasillo1_Data data;
    private EnemyBehaviour3 enemy;
    public GameObject enemyObj;
    
    protected override void Awake()
    {
        // Cargar si existe datos de Pasillo1
        try//nueva funcion, lo usaremos para comprobar si ha fallado
        {
            data = (Pasillo1_Data)DataManager.LoadFromText<Pasillo1_Data>("Pasillo1Data", Application.persistentDataPath + "/Levels");
            Debug.Log("[GDM] Load succeed!");
        }
        catch (Exception e) //guarda el motivo de fallo en exception
        {
            Debug.Log("[GDM] Load error: " + e);
            NewGame();
        }
        // fileName = "Pasillo1Data"
        // Si existen, inicializar cambios dependiendo de los datos
        if(data.enemyDead)
        {
            enemyObj.SetActive(false);
            enemy = null;
        }
    }
    public void NewGame()
    {
        data = new Pasillo1_Data();
    }
    void Start()
    {
        if(!data.enemyDead)
        {
            enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyBehaviour3>();
        }
    }

    public override void SaveLevelData()
    {

        // Actualizar datos
        if(enemy != null)
        {
            if (enemy.enemyIsDead)
            {
                data.enemyDead = true;
            }
        }
        else return;
        
        // Guardarlos
        try
        {
            DataManager.SaveToText<Pasillo1_Data>(data, "Pasillo1Data", Application.persistentDataPath + "/Levels");
            Debug.Log("[GDM] Save succeed!");
        }
        catch (Exception e)
        {
            Debug.Log("[GDM] Save error: " + e);
        }
        // Cambiar de escenas
    }
}
