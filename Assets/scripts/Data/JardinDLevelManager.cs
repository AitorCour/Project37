using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class JardinD_Data
{
    //public bool nota;
    /*public Cuadros_Data()
    {
        nota = true;
    }*/
}

public class JardinDLevelManager : LevelManager
{
    public JardinD_Data data;
    /*public GameObject noteObj;
    private Notes note;
    */
    protected override void Awake()
    {
        // Cargar si existe datos de Pasillo1

        try//nueva funcion, lo usaremos para comprobar si ha fallado
        {
            data = (JardinD_Data)DataManager.LoadFromText<JardinD_Data>("JardinDData", Application.persistentDataPath + "/Levels");
            Debug.Log("[GDM] Load succeed!");
        }
        catch (Exception e) //guarda el motivo de fallo en exception
        {
            Debug.Log("[GDM] Load error: " + e);
            NewGame();
        }
        // fileName = "Pasillo1Data"
        // Si existen, inicializar cambios dependiendo de los datos
        /*if (data.nota == false)
        {
            noteObj.SetActive(false);
        }*/
    }
    public void NewGame()
    {
        data = new JardinD_Data();
    }
    void Start()
    {
        /*if (data.nota == true)
        {
            note = GameObject.FindGameObjectWithTag("TextReader").GetComponent<Notes>();
        }*/
    }

    public override void SaveLevelData()
    {
        /*if (data.nota == true)
        {
            if (note.getObj == true)
            {
                data.nota = false;
            }
        }*/

        try
        {
            DataManager.SaveToText<JardinD_Data>(data, "JardinDData", Application.persistentDataPath + "/Levels");
            Debug.Log("[GDM] Save succeed!");
        }
        catch (Exception e)
        {
            Debug.Log("[GDM] Save error: " + e);
        }
    }
}

