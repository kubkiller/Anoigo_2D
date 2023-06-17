using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Dictionary<string, string> Itemdic = new Dictionary<string, string>(); //������ ��ųʸ�
    [HideInInspector]
    public List<string> ItemList = new List<string>(); //������ �̸� ����Ʈ

    [Header("Inventory")] //�κ��丮 �κ�
    public Text[] Items;
    public GameObject inventory;
    [SerializeField]
    private int In_index = 0;
    public GameObject in_select;
    public Text explan_text;
    public bool IsInven = false;


    void Start()
    {
        //������ ����ֱ�
        Itemdic.Add("�� ����", "����ִ� ���� �� �� ���� �� �ϴ�.");
        Itemdic.Add("�� �絿��", "���� ���� ���ִ�.");
        //��ųʸ����� �̸��� �����ͼ� ����Ʈȭ
        foreach (string item in new List<string>(Itemdic.Keys))
        {
            ItemList.Add(item);
        }
    }
    public void Inventory_Open()
    {
        //�κ��丮�� ���������� ����
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {//�޴��� ����
            In_index--;
            if (In_index <= 0) In_index = 0;
            in_select.transform.position = Items[In_index].gameObject.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            In_index++;
            if (In_index >= ItemList.Count) In_index = ItemList.Count - 1;
            in_select.transform.position = Items[In_index].gameObject.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {//�κ��丮 ����. ���鼭 �ؽ�Ʈ �ʱ�ȭ ���ֱ�
            for (int i = 0; i < ItemList.Count; i++)
            {
                Items[i].text = "";
                Items[i].gameObject.SetActive(false);
            }
            In_index = 0;
            IsInven = false;
            inventory.SetActive(false);
        }//������ ���� �ؽ�Ʈ�� �׻� ����
        explan_text.text = Itemdic[ItemList[In_index]];
    }
    public void Inventory_Setting()
    {
        IsInven = true;//�κ��丮 ����
        inventory.SetActive(true);
        in_select.transform.position = Items[0].transform.position;//ù��°�� ����
        if (ItemList.Count > 0)   //�������� �ִٸ� ������ ������ŭ �ؽ�Ʈ�� Ȱ��ȭ
        {
            for (int i = 0; i < ItemList.Count; i++)
            {
                Items[i].gameObject.SetActive(true);
                Items[i].text = ItemList[i];
            }
        }
    }
}
