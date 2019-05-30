using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SavePlayerData : MonoBehaviour
{
    
    public void SaveLevelData()
    {
        Data.Save("Player");
    }
}
