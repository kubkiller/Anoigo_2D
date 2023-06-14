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

    [Header("Sound")] //���� �κ�
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
        //������ ����ֱ�
        Itemdic.Add("�� ����", "����ִ� ���� �� �� ���� �� �ϴ�.");
        Itemdic.Add("�� �絿��", "���� ���� ���ִ�.");
        //��ųʸ����� �̸��� �����ͼ� ����Ʈȭ
        foreach (string item in new List<string>(Itemdic.Keys))
        {
            ItemList.Add(item);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {//C Ű ������ �޴� ����
            IsMenu = true;
            menu.SetActive(true);
        }
        if (IsMenu)
        {
            if (!IsInven && !IsSound)
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
                        Inventory();
                    }
                    else if (M_index == 1)
                    {//�ι�°�� ����
                        Sound();
                    }
                }
                if (Input.GetKeyDown(KeyCode.X))
                {//�޴� ����
                    M_index = 0;
                    menu.SetActive(false);
                    IsMenu = false;
                }
            }

            else if (IsInven)
            {//�κ��丮�� ���������� ����
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

            else if (IsSound)
            {

            }
        }
    }

    private void Inventory()
    {
        IsInven = true;//�κ��丮 ����
        inventory.SetActive(true);
        if (ItemList.Count == 0)//�������� ���ٸ� ù��°�� ����
        {
            in_select.transform.position = Items[0].transform.position;
        }
        else   //�ƴ϶�� ������ ������ŭ �ؽ�Ʈ�� Ȱ��ȭ
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