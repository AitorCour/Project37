using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;//para try y catch
//Utilizar struct cuando se usan para crear datos efimeros, datos que se usaran durante poco tiempo.
//Para datos que tengan que durar mucho en el tiempo, utilizar class. Basicamente tema de optimización.

[System.Serializable] //Utilizar esto para que se muestre en el inspector
public class GameData //Datos permanentes entre partida y partida
{
    //Player
    public bool[] hasKey;
    public int plLife;
    //public Scene1Data scene1;
   
    public GameData()
    {
        hasKey = new bool[2];
        for (int i = 0; i < hasKey.Length; i++)
        {
            hasKey[i] = false;
        }
    }
}

//string path = Application.persistentDataPath + "/Data";

public static class Data
{
    public static GameData gameData;
    public static int life;
    public static bool lifeSet;
    // Load/save persistent data
    public static void Save(string fileName)
    {
        string path = Application.persistentDataPath + "/Data";

        try//nueva funcion, lo usaremos para comprobar si ha fallado
        {
            //DataManager.SaveToXML<GameData>(data, fileName, path);
            DataManager.SaveToText<GameData>(gameData, fileName, path);
            Debug.Log("[GDM] Save succeed!");
        }
        catch(Exception e) //guarda el motivo de fallo en exception
        {
            Debug.Log("[GDM] Save error: " + e);
        }        
    }
    public static void Load(string fileName)
    {
        string path = Application.persistentDataPath + "/Data";

        try//nueva funcion, lo usaremos para comprobar si ha fallado
        {
            //data = (GameData)DataManager.LoadToXML<GameData>(fileName, path);
            gameData = (GameData)DataManager.LoadFromText<GameData>(fileName, path);
            Debug.Log("[GDM] Load succeed!");
        }
        catch (Exception e) //guarda el motivo de fallo en exception
        {
            Debug.Log("[GDM] Load error: " + e);
            NewGame();
        }
    }
    public static void NewGame()
    {
        gameData = new GameData();
    }
    public static void Delete(string fileName)
    {
        string filePath = Application.persistentDataPath + "/Data/" + fileName;

        DataManager.DeleteFile(filePath);
    }
    public static bool DataExists(string fileName)
    {
        string filePath = Application.persistentDataPath + "/Data/" + fileName;

        return DataManager.FileExists(filePath);
    }

    // Change gamedata
    public static void InitGameData()
    {
        if (gameData == null) gameData = new GameData();
    }

    public static void UnlockKey(int key)
    {
        if (gameData.hasKey.Length - 1 >= key)
        {
            gameData.hasKey[key] = true;
        }
        else Debug.LogError("Key doesn't exist");
    }
    public static bool IsKeyUnlock(int key)
    {
        if (gameData.hasKey.Length - 1 >= key)
        {
            return gameData.hasKey[key];
        }
        else
        {
            Debug.LogError("Key doesn't exist");
            return false;
        }
    }
    public static void SetLife(int i)
    {
        gameData.plLife = i;
        life = gameData.plLife;
        lifeSet = true;
    }
}