using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Numerics;
using UnityEngine.UI;

public class Save : MonoBehaviour
{
    [Header("Save")] //���̺� �κ�
    public Savedata savedata = new Savedata();//���� ���
    public GameObject[] save_slot;//���� ��ġ
    public GameObject save;
    [SerializeField]
    private int select_index = 0;//����Ʈ �� ���� �ε���
    [SerializeField]
    private int slot_index = 1;//���� ���̺� �� ���� �ε���
    public GameObject save_select;
    public bool IsSave = false;

    public void Save_Open()
    {
        //���̺갡 ���������� ����
        if (Input.GetKeyDown(KeyCode.Z))
        {//ZŰ�� ������ �� ���Կ� ����
            SaveData(slot_index);
            save_slot[select_index].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {//��Ű�� ������ ����
            select_index--;
            slot_index--;
            if (select_index <= 0) select_index = 0;//0���� �������� ����
            if (slot_index <= 1) slot_index = 1;//1���� �������� ����
            if (slot_index <= 7)
            {//�ִ� �ε��� 7�϶����Ͱ� ĭ�� �ø��� ȿ���� �� �� �ֱ� ������
                for (int i = 0; i <= 3; i++)
                {//3�� �ݺ�
                    if (!(select_index > 0 && select_index < 3))
                    {//0�� 3�� �ƴҶ� �� ���̴� ù��° ���԰� �׹�° ���� ���̸� �����϶��� SetActive�� �ǵ��� �ʱ�
                        if (SaveSearch(slot_index + i))
                        {//ȭ�鿡 ���̴� �װ��� ���Կ� ���� �����Ͱ� ������ �ѱ�
                            save_slot[i].SetActive(true);
                        }
                        else
                        {//������ ����
                            save_slot[i].SetActive(false);
                        }
                    }
                    if (select_index <= 0)
                    {//����Ʈ �ε����� ���� �� ������ �� ���ϵ� ���ڸ� �ٲ��ֱ�(ĭ�� �ø��� ȿ��)
                        savedata.Files[i].text = "���� " + (slot_index + i);
                    }
                }
            }
            save_select.transform.position = savedata.Files[select_index].gameObject.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            select_index++;
            slot_index++;
            if (select_index >= 4) select_index = 3;//3���� �������� ����
            if (slot_index >= 10) slot_index = 10;//10���� �������� ����

            if (slot_index >= 4)
            {//�ּ� �ε��� 4�϶����Ͱ� ĭ�� ������ ȿ���� �� �� �ֱ� ������
                for (int i = 3; i >= 0; i--)
                {//3�� �ݺ�(���̺� �ε����� �Ʒ��� �ֱ� ������ ���� �ε������� ���ʷ� �����ϱ� ����)
                    if (!(select_index > 0 && select_index < 3))
                    {//0�� 3�� �ƴҶ� �� ���̴� ù��° ���԰� �׹�° ���� ���̸� �����϶��� SetActive�� �ǵ��� �ʱ�
                        if (SaveSearch(slot_index - i))
                        {//ȭ�鿡 ���̴� �װ��� ���Կ� ���� �����Ͱ� ������ �ѱ�
                            save_slot[3 - i].SetActive(true);
                        }
                        else
                        {//������ ����
                            save_slot[3 - i].SetActive(false);
                        }
                    }
                    if (select_index >= 3)
                    {//����Ʈ �ε����� ���� �Ʒ� ������ �� ���ϵ� ���ڸ� �ٲ��ֱ�(ĭ�� ������ ȿ��)
                        savedata.Files[3 - i].text = "���� " + (slot_index - i);
                    }
                }
            }
            save_select.transform.position = savedata.Files[select_index].gameObject.transform.position;

        }
        if (Input.GetKeyDown(KeyCode.X))
        {//���̺� ����
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
            savedata.Files[i - 1].text = "���� " + i;
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

        //������ ����
        PlayerData myData = new PlayerData();
        //������ �����ϱ� �� ���̺� ������ ������ ������ �����
        if (!Directory.Exists(Application.dataPath + "/SaveData/"))
        {
            Directory.CreateDirectory(Application.dataPath + "/SaveData/");
        }
        //���̺� ������ ���� �ȿ� �÷��̾� ������(����)���� ������ ���� ����
        File.WriteAllText(Application.dataPath + "/SaveData/PlayerData" + slot + ".json", JsonUtility.ToJson(myData));

        //�����ϰ� �ٷ� �ҷ�����
        myData.GetData();
    }

    public bool SaveSearch(int slot)
    {
        return File.Exists(Application.dataPath + "/SaveData/PlayerData" + slot + ".json");
    }

    public void LoadData(int slot)
    {
        try
        {//���̺갡 �Ǿ������� �ҷ�����
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

