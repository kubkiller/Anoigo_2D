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
    public GameObject[] Sound_Slider;
    public List<AudioSource> Background_SoundList = new List<AudioSource>();
    public List<AudioSource> Active_SoundList = new List<AudioSource>();
    public Text background_Sound_text;
    public Text active_Sound_text;

    [Header("Save")] //세이브 부분
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
            if (!IsInven && !IsSound && !IsSave)
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
                    else if(M_index == 2)
                    {//세번째는 세이브
                        Save();
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
            {//사운드가 켜져있으면 실행
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {//볼륨 줄이기
                    Sound_Slider[Sound_index].GetComponent<Slider>().value -= 0.1f;
                    if(Sound_index == 0)
                    {//배경음악 선택
                        for (int i = 0; i < Background_SoundList.Count; i++)
                        {
                            Background_SoundList[i].volume = Sound_Slider[Sound_index].GetComponent<Slider>().value;//모든 사운드에 볼륨 적용
                        }
                        background_Sound_text.text = ((int)Mathf.Round(Sound_Slider[Sound_index].GetComponent<Slider>().value*10)).ToString();//텍스트 적용
                    }
                    else
                    {//효과음 선택
                        for (int i = 0; i < Active_SoundList.Count; i++)
                        {
                            Active_SoundList[i].volume = Sound_Slider[Sound_index].GetComponent<Slider>().value;//모든 사운드에 볼륨 적용
                        }
                        active_Sound_text.text = ((int)Mathf.Round(Sound_Slider[Sound_index].GetComponent<Slider>().value * 10)).ToString();//텍스트 적용
                    }
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {//볼륨 키우기
                    Sound_Slider[Sound_index].GetComponent<Slider>().value += 0.1f;
                    if (Sound_index == 0)
                    {//배경음악 선택
                        for (int i = 0; i < Background_SoundList.Count; i++)
                        {
                            Background_SoundList[i].volume = Sound_Slider[Sound_index].GetComponent<Slider>().value;//모든 사운드에 볼륨 적용
                        }
                        background_Sound_text.text = ((int)Mathf.Round(Sound_Slider[Sound_index].GetComponent<Slider>().value * 10)).ToString();//텍스트 적용
                    }
                    else
                    {//효과음 선택
                        for (int i = 0; i < Active_SoundList.Count; i++)
                        {
                            Active_SoundList[i].volume = Sound_Slider[Sound_index].GetComponent<Slider>().value;//모든 사운드에 볼륨 적용
                        }
                        active_Sound_text.text = ((int)Mathf.Round(Sound_Slider[Sound_index].GetComponent<Slider>().value * 10)).ToString();//텍스트 적용
                    }
                }
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {//인덱스 줄이기
                    Sound_index--;
                    if (Sound_index <= 0) Sound_index = 0; //0보다 작아지면 0으로 고정
                    sound_select.transform.position = new Vector2(sound_select.transform.position.x, Sound_Slider[Sound_index].transform.position.y);//선택 위치 바꾸기
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {//인덱스 올리기
                    Sound_index++;
                    if (Sound_index >= Sound_Slider.Length) Sound_index = Sound_Slider.Length-1;//인덱스가 넘으면 최대로 고정
                    sound_select.transform.position = new Vector2(sound_select.transform.position.x, Sound_Slider[Sound_index].transform.position.y);//선택 위치 바꾸기
                }
                if (Input.GetKeyDown(KeyCode.X))
                {//사운드 끄기
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
        IsInven = true;//인벤토리 열기
        inventory.SetActive(true);
        in_select.transform.position = Items[0].transform.position;//첫번째로 고정
        if(ItemList.Count > 0)   //아이템이 있다면 아이템 갯수만큼 텍스트를 활성화
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
            savedata.Files[i].text = "파일 " + i;
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