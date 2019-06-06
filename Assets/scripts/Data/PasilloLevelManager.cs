using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Pasillo1_Data
{
    public bool enemyDead;
    public bool lader;
    public bool laderPoint;
    public bool rayo;
    public bool note;
    public Pasillo1_Data()
    {
        enemyDead = false;
        lader = false;
        laderPoint = true;
        rayo = false;
        note = true;
    }
}

public class PasilloLevelManager : LevelManager
{
    public Pasillo1_Data data;
    private EnemyBehaviour3 enemy;
    public GameObject enemyObj;
    private LaderPos lader;
    public GameObject laderObj;
    public GameObject laderPoint;
    private Trigger_Rayo rayo;
    public GameObject noteObj;
    private Note_4 note_4;
    //public GameObject rayoObj;

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
        if(data.lader && Data.IsLaderPositioned() == true)
        {
            laderObj.SetActive(true);
        }
        else if(Data.IsLaderPositioned() == false)
        {
            data.lader = false;
            laderObj.SetActive(false);
        }
        /*if(data.note)
        {
            noteObj.transform.position = new Vector3(-14f, -0.2f, 14.5f);
        }
        else if(!data.note)
        {
            noteObj.transform.position = new Vector3(-14f, -2.0f, 14.5f);
        }*/
        /*if (data.laderPoint)
        {
            laderPoint.transform.position = new Vector3(-14.5f, 0f, 14.5f);
        }
        else laderPoint.transform.position = new Vector3(-14.5f, -5f, 14.5f);*/
    }
    public void NewGame()
    {
        data = new Pasillo1_Data();
    }
    void Start()
    {
        note_4 = GameObject.FindGameObjectWithTag("note").GetComponent<Note_4>();
        if (!data.enemyDead)
        {
            enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyBehaviour3>();
        }
        lader = GameObject.FindGameObjectWithTag("Misc").GetComponent<LaderPos>();
        if(data.lader)
        {
            lader.laderActive = true;
        }
        rayo = GameObject.FindGameObjectWithTag("rayo").GetComponent<Trigger_Rayo>();
        if (data.rayo)
        {
            rayo.activated = true;
        }
        else rayo.activated = false;
        if (Data.IsKeyUnlock(0) == true && data.note == true)
        {
            laderPoint.transform.position = new Vector3(-14.5f, -5f, 14.5f);
            noteObj.transform.position = new Vector3(-14f, -0.2f, 14.5f);
            
            //Debug.Log("NoteActive");
        }
        else if (Data.IsKeyUnlock(0) == false)
        {
            laderPoint.transform.position = new Vector3(-14.5f, 0f, 14.5f);
            noteObj.transform.position = new Vector3(-14f, -2.0f, 14.5f);
            
            //Debug.Log("NoteNotActive");
        }
        else if (Data.IsKeyUnlock(0) == true && data.note == false)
        {
            noteObj.transform.position = new Vector3(-14f, -2.0f, 14.5f);
            laderPoint.transform.position = new Vector3(-14.5f, -5f, 14.5f);
            //all deactivated
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

        if(!data.lader && !lader.used)
        {
            if(lader.laderActive)
            {
                data.lader = true;
                //Debug.Log("keep lader");
            }
        }
        if(!data.rayo)
        { 
            if(rayo.activated)
            {
                data.rayo = true;
            }
        }
        if(data.note)
        {
            if (note_4.getObj == true)
            {
                data.note = false;
            }
        }
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
