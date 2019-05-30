using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;//para try y catch
//Utilizar struct cuando se usan para crear datos efimeros, datos que se usaran durante poco tiempo.
//Para datos que tengan que durar mucho en el tiempo, utilizar class. Basicamente tema de optimización.

public class Playerdata
{
    public bool[] hasKey;
    public bool hasFragNote_1;
    public bool hasFragNote_2;
    public bool hasNote_2;
    public bool hasLader;
    public int plLife;
    public int plAmmo;
    public int plMunition;
    public int plBandages;

    //PLAYER POSITION
    public float pX;
    public float pZ;
    public float pY;

    public Playerdata()
    {
        hasKey = new bool[2];
        for (int i = 0; i < hasKey.Length; i++)
        {
            hasKey[i] = false;
        }
        //Valores al inicio de la partida
        plLife = 3;
        plAmmo = 7;
        plMunition = 0;
        plBandages = 0;
        hasFragNote_1 = false;
        hasFragNote_2 = false;
        hasNote_2 = false;
        hasLader = false;
        pX = -8;
        pZ = 1;
        pY = 1;
    }
}

[System.Serializable] //Utilizar esto para que se muestre en el inspector
public class GameData //Datos permanentes entre partida y partida
{
    public Playerdata pData;
    //public Scene1Data scene1;
   
    public GameData()
    {
        pData = new Playerdata();
    }
}

//string path = Application.persistentDataPath + "/Data";

public static class Data
{
    public static GameData gameData;
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
    //PRUEBA
    public static void DeleteFolder()
    {
        string folderPath = Application.persistentDataPath + "/Levels";

        DataManager.DeleteFolder(folderPath);
        Debug.Log("Deleting");
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
        if (gameData.pData.hasKey.Length - 1 >= key)
        {
            gameData.pData.hasKey[key] = true;
        }
        else Debug.LogError("Key doesn't exist");
    }
    public static bool IsKeyUnlock(int key)
    {
        if (gameData.pData.hasKey.Length - 1 >= key)
        {
            return gameData.pData.hasKey[key];
        }
        else
        {
            Debug.LogError("Key doesn't exist");
            return false;
        }
    }
    public static void SetLife(int i)
    {
        gameData.pData.plLife = i;
    }
    public static void SetAmmo(int i)
    {
        gameData.pData.plAmmo = i;
    }
    public static void SetMunition(int i)
    {
        gameData.pData.plMunition = i;
    }
    public static void SetBandages(int i)
    {
        gameData.pData.plBandages = i;
    }
    //Posiciones Player
    public static void SetPosition(float x, float y, float z)
    {
        gameData.pData.pX = x;
        gameData.pData.pY = y;
        gameData.pData.pZ = z;
    }
    public static void SetNoteFrag_1()
    {
        gameData.pData.hasFragNote_1 = true;
    }
    public static void SetNoteFrag_2()
    {
        gameData.pData.hasFragNote_2 = true;
    }
    public static void SetNote_2()
    {
        gameData.pData.hasNote_2 = true;
    }
    public static void SetLader()
    {
        gameData.pData.hasLader = true;
    }
    public static void LoseLader()
    {
        gameData.pData.hasLader = false;
    }
    public static int GetLife()
    { return gameData.pData.plLife; }
    public static int GetAmmo()
    { return gameData.pData.plAmmo; }
    public static int GetMunition()
    { return gameData.pData.plMunition; }
    public static int GetBandages()
    { return gameData.pData.plBandages; }
    public static bool GetNoteFrag_1()
    { return gameData.pData.hasFragNote_1; }
    public static bool GetNoteFrag_2()
    { return gameData.pData.hasFragNote_2; }
    public static bool GetNote_2()
    { return gameData.pData.hasNote_2; }
    public static bool IsLaderUnlock()
    {
        return gameData.pData.hasLader;
    }
    public static float GetPositionX()
    {
        return gameData.pData.pX;
    }
    public static float GetPositionY()
    {
        return gameData.pData.pY;
    }
    public static float GetPositionZ()
    {
        return gameData.pData.pZ;
    }
}