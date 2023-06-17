using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{ 
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

    public void Sound_Open()
    {
        //���尡 ���������� ����
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {//���� ���̱�
            Sound_Slider[Sound_index].GetComponent<Slider>().value -= 0.1f;
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
            if (Sound_index >= Sound_Slider.Length) Sound_index = Sound_Slider.Length - 1;//�ε����� ������ �ִ�� ����
            sound_select.transform.position = new Vector2(sound_select.transform.position.x, Sound_Slider[Sound_index].transform.position.y);//���� ��ġ �ٲٱ�
        }
        if (Input.GetKeyDown(KeyCode.X))
        {//���� ����
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
