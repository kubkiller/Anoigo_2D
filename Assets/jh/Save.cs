using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Numerics;
[Serializable]
public class PlayerData
{


    public void GetData()
    {

    }
}

public class Save : MonoBehaviour
{
    void Start()
    {
        SaveData();
    }
    public void SaveData()
    {
        PlayerData myData = new PlayerData();
        if (!Directory.Exists(Application.dataPath + "/SaveData/"))
        {
            Directory.CreateDirectory(Application.dataPath + "/SaveData/");
        }

        File.WriteAllText(Application.dataPath + "/SaveData/PlayerData"+ 1 +".json", JsonUtility.ToJson(myData));

        myData.GetData();
    }

    public void LoadData()
    {
        try
        {
            string jsonData = File.ReadAllText(Application.dataPath + "/SaveData/PlayerData" + 1 + ".json");
            PlayerData data = JsonUtility.FromJson<PlayerData>(jsonData); 
            data.GetData();
        }
        catch (Exception)
        {
            SaveData();
        }
    }
}
