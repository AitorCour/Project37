using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;//para try y catch
//Utilizar struct cuando se usan para crear datos efimeros, datos que se usaran durante poco tiempo.
//Para datos que tengan que durar mucho en el tiempo, utilizar class. Basicamente tema de optimización.

[System.Serializable] //Utilizar esto para que se muestre en el inspector
public class GameData //Datos permanentes entre partida y partida
{
    public enum Class { Archer, Warrior, Priest, Mage, Civil }
    public string name;
    public float xp;
    public Class playerClass;

    //
    public int[] numeros;
    //public EnemyData enemyData;
    //public MyColor color;

    public List<EnemyData> enemyList; //buscar List<T>Class


    //Declar unn constructor. Se hacen automaticamente, pero de esta forma se ponen unos valores por defecto
    public GameData()
    {
        name = "Insert name";
        xp = 0;
        playerClass = Class.Civil;
    }
}

//string path = Application.persistentDataPath + "/Data";

public static class GameDataManager
{
    public static void Save(GameData data, string fileName)
    {
        string path = Application.persistentDataPath + "/Data";

        try//nueva funcion, lo usaremos para comprobar si ha fallado
        {
            //DataManager.SaveToXML<GameData>(data, fileName, path);
            DataManager.SaveToText<GameData>(data, fileName, path);
            Debug.Log("[GDM] Save succeed!");
        }
        catch(Exception e) //guarda el motivo de fallo en exception
        {
            Debug.Log("[GDM] Save error: " + e);
        }
        
    }

    public static GameData Load(string fileName)
    {
        string path = Application.persistentDataPath + "/Data";

        GameData data;

        try//nueva funcion, lo usaremos para comprobar si ha fallado
        {
            //data = (GameData)DataManager.LoadToXML<GameData>(fileName, path);
            data = (GameData)DataManager.LoadFromText<GameData>(fileName, path);
            Debug.Log("[GDM] Load succeed!");
        }
        catch (Exception e) //guarda el motivo de fallo en exception
        {
            Debug.Log("[GDM] Load error: " + e);
            data = NewGame();
        }

        return data;
    }

    public static GameData NewGame()
    {
        return new GameData();
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
}