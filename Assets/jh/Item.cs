using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;
    public string explan;
    public Item(string _name, string _explan)
    {
        name = _name;
        explan = _explan;
    }
}
