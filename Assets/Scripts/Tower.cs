using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Tower 
{
    public string name;
    public int cost;
    public GameObject pref;

    public Tower (string _name, int _cos, GameObject _pref)
    {
        name = _name; cost = _cos; pref = _pref;
    }
}
