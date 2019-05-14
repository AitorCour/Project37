using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Puzzle_Data
{
    public bool puzzle;

    public Puzzle_Data()
    {
        puzzle = true;
    }
}

public class PuzzleLevelManager : LevelManager
{
    public Puzzle_Data data;
    public GameObject puzzleObj;
    public GameObject trigger1;
    public GameObject trigger2;
    public GameObject trigger3;
    private UseObject puzzle;

    protected override void Awake()
    {
        // Cargar si existe datos de Pasillo1
        try//nueva funcion, lo usaremos para comprobar si ha fallado
        {
            data = (Puzzle_Data)DataManager.LoadFromText<Puzzle_Data>("PuzzleData", Application.persistentDataPath + "/Levels");
            Debug.Log("[GDM] Load succeed!");
        }
        catch (Exception e) //guarda el motivo de fallo en exception
        {
            Debug.Log("[GDM] Load error: " + e);
            NewGame();
        }
        // fileName = "Pasillo1Data"
        // Si existen, inicializar cambios dependiendo de los datos
        if (data.puzzle == false)
        {
            puzzleObj.SetActive(false);
            trigger1.SetActive(false);
            trigger2.SetActive(false);
            trigger3.SetActive(false);
        }
    }
    public void NewGame()
    {
        data = new Puzzle_Data();
    }
    void Start()
    {
        if (data.puzzle == true)
        {
            puzzle = GameObject.FindGameObjectWithTag("ObjectPosition").GetComponent<UseObject>();
        }
    }

    public override void SaveLevelData()
    {
        if (data.puzzle == true)
        {
            if (puzzle.puzzleComplete == true)
            {
                data.puzzle = false;
            }
        }

        try
        {
            DataManager.SaveToText<Puzzle_Data>(data, "PuzzleData", Application.persistentDataPath + "/Levels");
            Debug.Log("[GDM] Save succeed!");
        }
        catch (Exception e)
        {
            Debug.Log("[GDM] Save error: " + e);
        }
    }
}
