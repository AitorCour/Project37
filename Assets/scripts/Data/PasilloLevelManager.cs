using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    protected override void Awake()
    {
        // Cargar si existe datos de Pasillo1
        data = (Pasillo_1_Data)DataManager.LoadFromText<Pasillo_1_Data>("Pasillo1Data", Application.persistentDataPath + "/Levels");
        // fileName = "Pasillo1Data"
        // Si existen, inicializar cambios dependiendo de los datos

        // if(data.enemyDead) //Desactivar enemigo
    }

    public override void SaveLevelData()
    {
        // Actualizar datos
        // if (enemyisDead) data.enemyDead = true;
        // Guardarlos
        //DataManager.SaveToText<Pasillo_1_Data>(data, "Pasillo1Data", Application.persistentDataPath  + "/Levels");
        // Cambiar de escenas

    }
}
