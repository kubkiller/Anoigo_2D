using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Numerics;
using UnityEngine.UI;

public class Save : MonoBehaviour
{
    [Header("Save")] //세이브 부분
    public Savedata savedata = new Savedata();//슬롯 요소
    public GameObject[] save_slot;//슬롯 위치
    public GameObject save;
    [SerializeField]
    private int select_index = 0;//셀렉트 될 슬롯 인덱스
    [SerializeField]
    private int slot_index = 1;//실제 세이브 될 슬롯 인덱스
    public GameObject save_select;
    public bool IsSave = false;

    public void Save_Open()
    {
        //세이브가 켜져있으면 실행
        if (Input.GetKeyDown(KeyCode.Z))
        {//Z키를 누르면 그 슬롯에 저장
            SaveData(slot_index);
            save_slot[select_index].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {//윗키를 누르면 실행
            select_index--;
            slot_index--;
            if (select_index <= 0) select_index = 0;//0보다 낮아지면 고정
            if (slot_index <= 1) slot_index = 1;//1보다 낮아지면 고정
            if (slot_index <= 7)
            {//최대 인덱스 7일때부터가 칸을 올리는 효과를 줄 수 있기 때문에
                for (int i = 0; i <= 3; i++)
                {//3번 반복
                    if (!(select_index > 0 && select_index < 3))
                    {//0과 3이 아닐때 즉 보이는 첫번째 슬롯과 네번째 슬롯 사이를 움직일때는 SetActive를 건들지 않기
                        if (SaveSearch(slot_index + i))
                        {//화면에 보이는 네개의 슬롯에 저장 데이터가 있으면 켜기
                            save_slot[i].SetActive(true);
                        }
                        else
                        {//없으면 끄기
                            save_slot[i].SetActive(false);
                        }
                    }
                    if (select_index <= 0)
                    {//셀렉트 인덱스가 가장 위 슬롯일 때 파일뒤 숫자를 바꿔주기(칸을 올리는 효과)
                        savedata.Files[i].text = "파일 " + (slot_index + i);
                    }
                }
            }
            save_select.transform.position = savedata.Files[select_index].gameObject.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            select_index++;
            slot_index++;
            if (select_index >= 4) select_index = 3;//3보다 높아지면 고정
            if (slot_index >= 10) slot_index = 10;//10보다 높아지면 고정

            if (slot_index >= 4)
            {//최소 인덱스 4일때부터가 칸을 내리는 효과를 줄 수 있기 때문에
                for (int i = 3; i >= 0; i--)
                {//3번 반복(세이브 인덱스는 아래에 있기 때문에 적은 인덱스부터 차례로 접근하기 위해)
                    if (!(select_index > 0 && select_index < 3))
                    {//0과 3이 아닐때 즉 보이는 첫번째 슬롯과 네번째 슬롯 사이를 움직일때는 SetActive를 건들지 않기
                        if (SaveSearch(slot_index - i))
                        {//화면에 보이는 네개의 슬롯에 저장 데이터가 있으면 켜기
                            save_slot[3 - i].SetActive(true);
                        }
                        else
                        {//없으면 끄기
                            save_slot[3 - i].SetActive(false);
                        }
                    }
                    if (select_index >= 3)
                    {//셀렉트 인덱스가 가장 아래 슬롯일 때 파일뒤 숫자를 바꿔주기(칸을 내리는 효과)
                        savedata.Files[3 - i].text = "파일 " + (slot_index - i);
                    }
                }
            }
            save_select.transform.position = savedata.Files[select_index].gameObject.transform.position;

        }
        if (Input.GetKeyDown(KeyCode.X))
        {//세이브 끄기
            slot_index = 0;
            select_index = 0;
            IsSave = false;
            save.SetActive(false);
        }
    }



    public void Save_Setting()
    {
        slot_index = 1;
        IsSave = true;
        save.SetActive(true);
        save_select.transform.position = savedata.Files[0].gameObject.transform.position;
        for (int i = 1; i < 5; i++)
        {
            savedata.Files[i - 1].text = "파일 " + i;
        }
        for (int i = 0; i <= 3; i++)
        {
            if (SaveSearch(slot_index + i))
            {
                save_slot[i].SetActive(true);
            }
        }
    }

    public void SaveData(int slot)
    {

        //데이터 저장
        PlayerData myData = new PlayerData();
        //데이터 저장하기 전 세이브 데이터 폴더가 없으면 만들기
        if (!Directory.Exists(Application.dataPath + "/SaveData/"))
        {
            Directory.CreateDirectory(Application.dataPath + "/SaveData/");
        }
        //세이브 데이터 폴더 안에 플레이어 데이터(숫자)식의 데이터 파일 저장
        File.WriteAllText(Application.dataPath + "/SaveData/PlayerData" + slot + ".json", JsonUtility.ToJson(myData));

        //저장하고 바로 불러오기
        myData.GetData();
    }

    public bool SaveSearch(int slot)
    {
        return File.Exists(Application.dataPath + "/SaveData/PlayerData" + slot + ".json");
    }

    public void LoadData(int slot)
    {
        try
        {//세이브가 되어있으면 불러오기
            string jsonData = File.ReadAllText(Application.dataPath + "/SaveData/PlayerData" + slot + ".json");
            PlayerData data = JsonUtility.FromJson<PlayerData>(jsonData);
            data.GetData();
        }
        catch (Exception)
        {
            return;
        }
    }
}
class PlayerData
{


    public void GetData()
    {

    }
}
[System.Serializable]
public class Savedata
{

    public Text[] Files;
    public Image[] Images;
    public Text[] Times;
    public Text[] Positions;

}

