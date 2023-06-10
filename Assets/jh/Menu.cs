using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("Menu")]
    public GameObject menu;
    public GameObject[] selection_position;
    public GameObject menu_select;
    public bool IsMenu = false;
    private int M_index = 0;

    public Dictionary<string, string> Itemdic = new Dictionary<string, string>();
    [HideInInspector]
    public List<string> ItemList = new List<string>();

    [Header("Inventory")]
    public Text[] Items;
    public GameObject inventory;
    private int In_index = 0;
    public GameObject in_select;
    public Text explan_text;
    public bool IsInven = false;


    void Start()
    {
        Itemdic.Add("문 열쇠", "잠겨있는 문을 열 수 있을 듯 하다.");
        Itemdic.Add("물 양동이", "물이 가득 차있다.");
        foreach (string item in new List<string>(Itemdic.Keys))
        {
            ItemList.Add(item);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            IsMenu = true;
            menu.SetActive(true);
        }
        if (IsMenu == true)
        {
            if (IsInven)
            {

            }
            else
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    M_index--;
                    if (M_index <= 0) M_index = 0;
                    menu_select.transform.position = selection_position[M_index].transform.position;
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    M_index++;
                    if (M_index >= 4) M_index = 4;
                    menu_select.transform.position = selection_position[M_index].transform.position;
                }
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    if (M_index == 0)
                    {
                        Inventory();
                    }
                }
                if (Input.GetKeyDown(KeyCode.X))
                {
                    menu.SetActive(false);
                    IsMenu = false;
                }

            }
        }
    }

    private void Inventory()
    {
        IsInven = true;
        inventory.SetActive(true);
        if (ItemList.Count == 0)
        {
            in_select.transform.position = Items[0].transform.position;
        }
        else
        {
            for (int i = 0; i < ItemList.Count; i++)
            {
                Items[i].gameObject.SetActive(true);
                Items[i].text = ItemList[i];
            }
        }
        while (!Input.GetKeyDown(KeyCode.X)) 
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                In_index--;
                if (In_index <= 0) In_index = 0;
                in_select.transform.position = Items[In_index].transform.position;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                In_index++;
                if (In_index >= ItemList.Count) In_index = ItemList.Count;
                in_select.transform.position = Items[M_index].transform.position;
            }
            explan_text.text = Itemdic[ItemList[In_index]];
        }
        for (int i = 0; i < ItemList.Count; i++)
        {
            Items[i].text = "";
            Items[i].gameObject.SetActive(false);
        }
        IsInven = false;
        inventory.SetActive(false);
    }
}