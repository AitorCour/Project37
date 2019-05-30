using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SavePlayerData : LevelManager
{
    public Playerdata data;

    new public void SaveLevelData()
    {
        try
        {
            DataManager.SaveToText<Playerdata>(data, "Player", Application.persistentDataPath + "/Levels");
            Debug.Log("[GDM] Save succeed!");
        }
        catch (Exception e)
        {
            Debug.Log("[GDM] Save error: " + e);
        }
    }
}
