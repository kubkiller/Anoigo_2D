using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Dictionary<string, string> Itemdic = new Dictionary<string, string>(); //아이템 딕셔너리
    [HideInInspector]
    public List<string> ItemList = new List<string>(); //아이템 이름 리스트

    [Header("Inventory")] //인벤토리 부분
    public Text[] Items;
    public GameObject inventory;
    [SerializeField]
    private int In_index = 0;
    public GameObject in_select;
    public Text explan_text;
    public bool IsInven = false;


    void Start()
    {
        //아이템 집어넣기
        Itemdic.Add("문 열쇠", "잠겨있는 문을 열 수 있을 듯 하다.");
        Itemdic.Add("물 양동이", "물이 가득 차있다.");
        //딕셔너리에서 이름만 가져와서 리스트화
        foreach (string item in new List<string>(Itemdic.Keys))
        {
            ItemList.Add(item);
        }
    }
    public void Inventory_Open()
    {
        //인벤토리가 켜져있으면 실행
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {//메뉴랑 같음
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
        {//인벤토리 끄기. 끄면서 텍스트 초기화 해주기
            for (int i = 0; i < ItemList.Count; i++)
            {
                Items[i].text = "";
                Items[i].gameObject.SetActive(false);
            }
            In_index = 0;
            IsInven = false;
            inventory.SetActive(false);
        }//아이템 설명 텍스트는 항상 실행
        explan_text.text = Itemdic[ItemList[In_index]];
    }
    public void Inventory_Setting()
    {
        IsInven = true;//인벤토리 열기
        inventory.SetActive(true);
        in_select.transform.position = Items[0].transform.position;//첫번째로 고정
        if (ItemList.Count > 0)   //아이템이 있다면 아이템 갯수만큼 텍스트를 활성화
        {
            for (int i = 0; i < ItemList.Count; i++)
            {
                Items[i].gameObject.SetActive(true);
                Items[i].text = ItemList[i];
            }
        }
    }
}
