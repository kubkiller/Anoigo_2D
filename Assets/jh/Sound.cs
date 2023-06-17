using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{ 
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

    public void Sound_Open()
    {
        //사운드가 켜져있으면 실행
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {//볼륨 줄이기
            Sound_Slider[Sound_index].GetComponent<Slider>().value -= 0.1f;
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
            if (Sound_index >= Sound_Slider.Length) Sound_index = Sound_Slider.Length - 1;//인덱스가 넘으면 최대로 고정
            sound_select.transform.position = new Vector2(sound_select.transform.position.x, Sound_Slider[Sound_index].transform.position.y);//선택 위치 바꾸기
        }
        if (Input.GetKeyDown(KeyCode.X))
        {//사운드 끄기
            Sound_index = 0;
            IsSound = false;
            sound.SetActive(false);
            gameObject.GetComponent<Menu>().menu_select.GetComponent<Animator>().enabled = true;
        }
    }
    public void Sound_Setting()
    {
        IsSound = true;
        sound.SetActive(true);
        sound_select.transform.position = new Vector2(sound_select.transform.position.x, Sound_Slider[Sound_index].transform.position.y);
        gameObject.GetComponent<Menu>().menu_select.GetComponent<Animator>().enabled = false;
    }
}
