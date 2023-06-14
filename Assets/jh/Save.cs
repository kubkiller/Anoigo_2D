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
        //데이터 저장
        PlayerData myData = new PlayerData();
        //데이터 저장하기 전 세이브 데이터 폴더가 없으면 만들기
        if (!Directory.Exists(Application.dataPath + "/SaveData/"))
        {
            Directory.CreateDirectory(Application.dataPath + "/SaveData/");
        }
        //세이브 데이터 폴더 안에 플레이어 데이터(숫자)식의 데이터 파일 저장
        File.WriteAllText(Application.dataPath + "/SaveData/PlayerData"+ 1 +".json", JsonUtility.ToJson(myData));

        //저장하고 바로 불러오기
        myData.GetData();
    }

    public void LoadData()
    {
        try
        {//세이브가 되어있으면 불러오기
            string jsonData = File.ReadAllText(Application.dataPath + "/SaveData/PlayerData" + 1 + ".json");
            PlayerData data = JsonUtility.FromJson<PlayerData>(jsonData); 
            data.GetData();
        }
        catch (Exception)
        {
            return;
        }
    }
}
