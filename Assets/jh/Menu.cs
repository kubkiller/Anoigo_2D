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
    public float play_time;

    public Inventory inventory;
    public Sound sound;
    public Save save;
    public Exit exit;



    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {//C 키 누르면 메뉴 열기
            IsMenu = true;
            menu_select.transform.position = selection_position[M_index].transform.position;
            menu.SetActive(true);
        }
        if (IsMenu)
        {
            if (inventory.IsInven)
            {
                inventory.Inventory_Open();
            }

            else if (sound.IsSound)
            {
                sound.Sound_Open();
            }

            else if (save.IsSave)
            {
                save.Save_Open();
            }
            else if (exit.IsExit)
            {
                exit.Exit_Open();
            }
            else
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
                        inventory.Inventory_Setting();
                    }
                    else if (M_index == 1)
                    {//두번째는 음량
                        sound.Sound_Setting();
                    }
                    else if (M_index == 2)
                    {//세번째는 세이브
                        save.Save_Setting();
                    }
                    else if (M_index == 3)
                    {//네번째는 나가기
                        exit.Exit_Setting();
                    }
                }
                if (Input.GetKeyDown(KeyCode.X))
                {//메뉴 끄기
                    M_index = 0;
                    menu.SetActive(false);
                    IsMenu = false;
                }
            }
        }
    }
}