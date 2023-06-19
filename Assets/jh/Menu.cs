using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("Menu")] // �޴� �κ�
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
    [SerializeField]
    TextAsset TMI_text;
    public Text inter_TMI;

    void Start()
    {

    }

    void Update()
    {
        if (!IsMenu && Input.GetKeyDown(KeyCode.C))
        {//C Ű ������ �޴� ����
            IsMenu = true;
            menu_select.transform.position = selection_position[M_index].transform.position;
            menu.SetActive(true);
            Player_interface();
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
            {//�ٸ� â�� �ȿ��� ������ ����
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {//��Ű ������ �ε����ٿ��ֱ�(0�� ���� ó��)
                    M_index--;
                    if (M_index <= 0) M_index = 0;
                    menu_select.transform.position = selection_position[M_index].transform.position;
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {//�Ʒ�Ű ������ �ε��� �÷��ֱ�
                    M_index++;
                    if (M_index >= 3) M_index = 3;
                    menu_select.transform.position = selection_position[M_index].transform.position;
                }
                if (Input.GetKeyDown(KeyCode.Z))
                {//�ε��� ��ȣ�� ���� �ٸ� UI����
                    if (M_index == 0)
                    {//ù��°�� �κ��丮
                        inventory.Inventory_Setting();
                    }
                    else if (M_index == 1)
                    {//�ι�°�� ����
                        sound.Sound_Setting();
                    }
                    else if (M_index == 2)
                    {//����°�� ���̺�
                        save.Save_Setting();
                    }
                    else if (M_index == 3)
                    {//�׹�°�� ������
                        exit.Exit_Setting();
                    }
                }
                if (Input.GetKeyDown(KeyCode.X))
                {//�޴� ����
                    M_index = 0;
                    menu.SetActive(false);
                    IsMenu = false;
                }
            }
        }
    }

    private void Player_interface(){
        string TMI = "";
        string[] TMIs = TMI_text.text.Split("\n");

        TMI = TMIs[(int)Random.Range(0, TMIs.Length)];

        inter_TMI.text = "TMI - " + TMI;
    }
}