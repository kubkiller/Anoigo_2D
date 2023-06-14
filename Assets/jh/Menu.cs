using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("Menu")] // 메뉴 부분
    public GameObject menu;
    public GameObject[] selection_position;
    public GameObject menu_select;
    public bool IsMenu = false;
    [SerializeField]
    private int M_index = 0;

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

    [Header("Sound")] //사운드 부분
    public GameObject sound;
    [SerializeField]
    private int Sound_index = 0;
    public GameObject sound_select;
    public bool IsSound = false;
    public GameObject background_Sound_Slider;
    public GameObject active_Sound_Slider;
    public List<AudioSource> SoundList = new List<AudioSource>();
    public Text background_Sound_text;
    public Text active_Sound_text;

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {//C 키 누르면 메뉴 열기
            IsMenu = true;
            menu.SetActive(true);
        }
        if (IsMenu)
        {
            if (!IsInven && !IsSound)
            {//다른 창이 안열려 있을때 실행
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {//윗키 누르면 인덱스줄여주기(0이 가장 처음)
                    M_index--;
                    if (M_index <= 0) M_index = 0;
                    menu_select.transform.position = selection_position[M_index].transform.position;
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {//아랫키 누르면 인덱스 늘려주기
                    M_index++;
                    if (M_index >= 3) M_index = 3;
                    menu_select.transform.position = selection_position[M_index].transform.position;
                }
                if (Input.GetKeyDown(KeyCode.Z))
                {//인덱스 번호에 따라서 다른 UI띄우기
                    if (M_index == 0)
                    {//첫번째는 인벤토리
                        Inventory();
                    }
                    else if (M_index == 1)
                    {//두번째는 음량
                        Sound();
                    }
                }
                if (Input.GetKeyDown(KeyCode.X))
                {//메뉴 끄기
                    M_index = 0;
                    menu.SetActive(false);
                    IsMenu = false;
                }
            }

            else if (IsInven)
            {//인벤토리가 켜져있으면 실행
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

            else if (IsSound)
            {

            }
        }
    }

    private void Inventory()
    {
        IsInven = true;//인벤토리 열기
        inventory.SetActive(true);
        if (ItemList.Count == 0)//아이템이 없다면 첫번째로 고정
        {
            in_select.transform.position = Items[0].transform.position;
        }
        else   //아니라면 아이템 갯수만큼 텍스트를 활성화
        {
            for (int i = 0; i < ItemList.Count; i++)
            {
                Items[i].gameObject.SetActive(true);
                Items[i].text = ItemList[i];
            }
        }
    }

    private void Sound()
    {
        IsSound = true;
        sound.SetActive(true);
    }
}