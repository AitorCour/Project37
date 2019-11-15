using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Library_Data
{
    public bool nota_1;
    public bool nota_2;
    public bool rayo;
    public bool ammo;

    public Library_Data()
    {
        nota_1 = true;
        nota_2 = true;
        rayo = false;
        ammo = true;
    }
}

public class LibraryLevelManager : LevelManager
{
    public Library_Data data;
    public GameObject noteObj_1;
    private Nota_2 note_1;
    public GameObject noteObj_2;
    private Note_3 note_2;
    private Trigger_Rayo rayo;
    private Munition munition;
    public GameObject ammoObj;

    protected override void Awake()
    {
        // Cargar si existe datos de Pasillo1

        try//nueva funcion, lo usaremos para comprobar si ha fallado
        {
            data = (Library_Data)DataManager.LoadFromText<Library_Data>("LibraryData", Application.persistentDataPath + "/Levels");
            //Debug.Log("[GDM] Load succeed!");
        }
        catch (Exception e) //guarda el motivo de fallo en exception
        {
            //Debug.Log("[GDM] Load error: " + e);
            NewGame();
        }
        // fileName = "Pasillo1Data"
        // Si existen, inicializar cambios dependiendo de los datos
        if (data.nota_1 == false)
        {
            noteObj_1.SetActive(false);
        }
        if (data.nota_2 == false)
        {
            noteObj_2.SetActive(false);
        }
        if (data.ammo == false)
        {
            ammoObj.SetActive(false);
        }
    }
    public void NewGame()
    {
        data = new Library_Data();
    }
    void Start()
    {
        if (data.nota_1 == true)
        {
            note_1 = GameObject.FindGameObjectWithTag("note").GetComponent<Nota_2>();
        }
        if (data.nota_2 == true)
        {
            note_2 = GameObject.FindGameObjectWithTag("Misc").GetComponent<Note_3>();
        }
        rayo = GameObject.FindGameObjectWithTag("rayo").GetComponent<Trigger_Rayo>();
        if (data.rayo)
        {
            rayo.activated = true;
        }
        else rayo.activated = false;
        if (data.ammo == true)
        {
            munition = GameObject.FindGameObjectWithTag("ammo").GetComponent<Munition>();
        }
    }

    public override void SaveLevelData()
    {
        if (data.nota_1 == true)
        {
            if (note_1.getObj == true)
            {
                data.nota_1 = false;
            }
        }
        if (data.nota_2 == true)
        {
            if (note_2.getObj == true)
            {
                data.nota_2 = false;
            }
        }
        if (!data.rayo)
        {
            if (rayo.activated)
            {
                data.rayo = true;
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
            DataManager.SaveToText<Library_Data>(data, "LibraryData", Application.persistentDataPath + "/Levels");
            //Debug.Log("[GDM] Save succeed!");
        }
        catch (Exception e)
        {
            Debug.Log("[GDM] Save error: " + e);
        }
    }
}
