using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    [Header("Exit")]
    public GameObject exit;
    public GameObject[] exits;
    public bool IsExit = false;
    public int exit_index;
    public GameObject exit_select;
    
    public void Exit_Open()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (exit_index == 1)
            {
                GameExit();
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {//¸Þ´º¶û °°À½
            exit_index--;
            if (exit_index <= 0) exit_index = 0;
            exit_select.transform.position = new Vector2(exit_select.transform.position.x, exits[exit_index].gameObject.transform.position.y);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            exit_index++;
            if (exit_index >= 1) exit_index = 1;
            exit_select.transform.position = new Vector2(exit_select.transform.position.x, exits[exit_index].gameObject.transform.position.y);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            exit_index = 0;
            IsExit = false;
            exit.SetActive(false);
        }
    }

    public void Exit_Setting()
    {
        exit_index = 0;
        exit_select.transform.position = new Vector2(exit_select.transform.position.x, exits[exit_index].gameObject.transform.position.y);
        IsExit = true;
        exit.SetActive(true);
    }

    private void GameExit()
    {
        Application.Quit();
    }
}
