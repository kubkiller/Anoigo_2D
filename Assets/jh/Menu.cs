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
    public GameObject[] Sound_Slider;
    public List<AudioSource> Background_SoundList = new List<AudioSource>();
    public List<AudioSource> Active_SoundList = new List<AudioSource>();
    public Text background_Sound_text;
    public Text active_Sound_text;

    [Header("Save")] //���̺� �κ�
    public Savedata savedata = new Savedata();
    public GameObject[] save_slot;
    public GameObject save;
    [SerializeField]
    private int save_index = 0;
    private int max_index = 0;
    public GameObject save_select;
    public bool IsSave = false;
    public Save SaveScript;

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
            if (!IsInven && !IsSound && !IsSave)
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
                    else if(M_index == 2)
                    {//����°�� ���̺�
                        Save();
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
            {//���尡 ���������� ����
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {//���� ���̱�
                    Sound_Slider[Sound_index].GetComponent<Slider>().value -= 0.1f;
                    if(Sound_index == 0)
                    {//������� ����
                        for (int i = 0; i < Background_SoundList.Count; i++)
                        {
                            Background_SoundList[i].volume = Sound_Slider[Sound_index].GetComponent<Slider>().value;//��� ���忡 ���� ����
                        }
                        background_Sound_text.text = ((int)Mathf.Round(Sound_Slider[Sound_index].GetComponent<Slider>().value*10)).ToString();//�ؽ�Ʈ ����
                    }
                    else
                    {//ȿ���� ����
                        for (int i = 0; i < Active_SoundList.Count; i++)
                        {
                            Active_SoundList[i].volume = Sound_Slider[Sound_index].GetComponent<Slider>().value;//��� ���忡 ���� ����
                        }
                        active_Sound_text.text = ((int)Mathf.Round(Sound_Slider[Sound_index].GetComponent<Slider>().value * 10)).ToString();//�ؽ�Ʈ ����
                    }
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {//���� Ű���
                    Sound_Slider[Sound_index].GetComponent<Slider>().value += 0.1f;
                    if (Sound_index == 0)
                    {//������� ����
                        for (int i = 0; i < Background_SoundList.Count; i++)
                        {
                            Background_SoundList[i].volume = Sound_Slider[Sound_index].GetComponent<Slider>().value;//��� ���忡 ���� ����
                        }
                        background_Sound_text.text = ((int)Mathf.Round(Sound_Slider[Sound_index].GetComponent<Slider>().value * 10)).ToString();//�ؽ�Ʈ ����
                    }
                    else
                    {//ȿ���� ����
                        for (int i = 0; i < Active_SoundList.Count; i++)
                        {
                            Active_SoundList[i].volume = Sound_Slider[Sound_index].GetComponent<Slider>().value;//��� ���忡 ���� ����
                        }
                        active_Sound_text.text = ((int)Mathf.Round(Sound_Slider[Sound_index].GetComponent<Slider>().value * 10)).ToString();//�ؽ�Ʈ ����
                    }
                }
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {//�ε��� ���̱�
                    Sound_index--;
                    if (Sound_index <= 0) Sound_index = 0; //0���� �۾����� 0���� ����
                    sound_select.transform.position = new Vector2(sound_select.transform.position.x, Sound_Slider[Sound_index].transform.position.y);//���� ��ġ �ٲٱ�
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {//�ε��� �ø���
                    Sound_index++;
                    if (Sound_index >= Sound_Slider.Length) Sound_index = Sound_Slider.Length-1;//�ε����� ������ �ִ�� ����
                    sound_select.transform.position = new Vector2(sound_select.transform.position.x, Sound_Slider[Sound_index].transform.position.y);//���� ��ġ �ٲٱ�
                }
                if (Input.GetKeyDown(KeyCode.X))
                {//���� ����
                    IsSound = false;
                    sound.SetActive(false); 
                    menu_select.GetComponent<Animator>().enabled = true;
                }
            }

            else if (IsSave)
            {

            }
        }
    }

    private void Inventory()
    {
        IsInven = true;//�κ��丮 ����
        inventory.SetActive(true);
        in_select.transform.position = Items[0].transform.position;//ù��°�� ����
        if(ItemList.Count > 0)   //�������� �ִٸ� ������ ������ŭ �ؽ�Ʈ�� Ȱ��ȭ
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
        menu_select.GetComponent<Animator>().enabled = false;
    }

    private void Save()
    {
        IsSave = true;
        for (int i = 1; i < 5; i++)
        {
            savedata.Files[i].text = "���� " + i;
        }
        max_index = SaveScript.SaveSearch();
        if (max_index != 0)
        {
            for (int i = 0; i < save_slot.Length; i++)
            {
                save_slot[i].SetActive(true);
            }
        }
    }
}
[System.Serializable]
public class Savedata {

    public Text[] Files;
    public Image[] Images;
    public Text[] Times;
    public Text[] Positions;

}