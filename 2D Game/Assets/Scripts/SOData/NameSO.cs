using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class NameSO : ScriptableObject
{
    [SerializeField]
    private string _name = "Guts";

    public string playerName {
        set { _name = value; }
        get { return _name;}
    }
}
