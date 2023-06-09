using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class CharSpriteList
{
    public string charName;
    public List<Sprite> charSprite = new List<Sprite>();
}

public class MassageManager : MonoBehaviour
{
    public static MassageManager instance;

    private void Awake()
    {
        instance = this;
    }

    public Text massageTxt;

    //public SpriteRenderer sp1;
    public SpriteRenderer sp2;

    public TextAsset system;
    public TextAsset speech;

    //List<string> SystemMassage;
    public List<string> SpeechMassage;

    public List<CharSpriteList> spriteList = new List<CharSpriteList>();

    void Start()
    {
        //SystemMassage = SetMassage(system);
        SpeechMassage = SetMassage(speech);
        this.gameObject.SetActive(false);
    }

    int inx = 0;
    void Update()
    {
        if (this.gameObject.activeSelf == true && Input.GetKeyDown(KeyCode.Escape))
        {
            this.gameObject.SetActive(false);
        }
    }

    public List<string> CharName;
    List<string> SetMassage(TextAsset script)
    {
        List<string> Massage = new List<string>();

        string allScript = script.text;

        string[] scriptLine = allScript.Split("\n");
        for (int i = 0; i < scriptLine.Length; i++)
        {
            string[] nameSpl = scriptLine[i].Split('/');

            CharName.Add(nameSpl[0]);
            Massage.Add('[' + nameSpl[0] + "]\n" + nameSpl[1]);
        }
        return Massage;
    }

    //string Ori_name = null;
    public void PrintMassage(List<string> script, int sc_Inx, int sp_Inx, SpriteRenderer spPos)
    {
        string c_name = CharName[sc_Inx];

        Debug.Log(c_name);

        massageTxt.text = script[sc_Inx];
        spPos.sprite = SetCharSprite(c_name, sp_Inx);

        /*f (c_name != Ori_name && spPos == sp1)
        {
            sp2.color = new Color(255, 255, 255, 90);
            sp1.color = new Color(255, 255, 255, 255);
        }
        else if (c_name != Ori_name && spPos == sp2)
        {
            sp1.color = new Color(255, 255, 255, 90);
            sp2.color = new Color(255, 255, 255, 255);
        }

        Ori_name = c_name; */
    }

    Sprite SetCharSprite(string c_name, int sp_Inx)
    {
        Sprite curSp = null;

        for (int i = 0; i < spriteList.Count; i++)
        {
            if (c_name == spriteList[i].charName)
                curSp = spriteList[i].charSprite[sp_Inx];
        }

        return curSp;
    }

    public void PrintTalk(int i)
    {
        PrintMassage(SpeechMassage, i, 0, sp2);
    }

    /* List<string> SetMassage(TextAsset script)
     {
         List<string> Massage = new List<string>();

         string allScript = script.text;

         string[] scriptLine = allScript.Split("\n");
         for (int i = 0; i < scriptLine.Length; i++)
         {
             Massage.Add(scriptLine[i].Replace("/", " :\n"));
         }
         return Massage;
     }

     Sprite SetCharSprite(string script, int sp_Inx)
     {
         Sprite curSp = null;

         string[] nameSpl = script.Split(" ");
         string c_name = nameSpl[0];

         for (int i = 0; i < spriteList.Count; i++)
         {
             if (c_name == spriteList[i].charName)
                 curSp = spriteList[i].charSprite[sp_Inx];
             else
                 curSp = null;
         }

         return curSp;
     }*/
}