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
    [SerializeField]
    private int M_index = 0;

    public Dictionary<string, string> Itemdic = new Dictionary<string, string>();
    [HideInInspector]
    public List<string> ItemList = new List<string>();

    [Header("Inventory")]
    public Text[] Items;
    public GameObject inventory;
    [SerializeField]
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
        if (IsMenu)
        {
            if (!IsInven)
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
                    if (M_index >= 3) M_index = 3;
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
                    M_index = 0;
                    menu.SetActive(false);
                    IsMenu = false;
                }
            }
            else if (IsInven)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
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
                {
                    for (int i = 0; i < ItemList.Count; i++)
                    {
                        Items[i].text = "";
                        Items[i].gameObject.SetActive(false);
                    }
                    In_index = 0;
                    IsInven = false;
                    inventory.SetActive(false);
                }
                explan_text.text = Itemdic[ItemList[In_index]];
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
    }
}