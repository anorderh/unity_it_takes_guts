using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SettingsSO : ScriptableObject
{
    [SerializeField]
    private float musicFactor = 1f;
    [SerializeField]
    private float soundFactor = 1f;

    public float Sound {
        set {soundFactor = value;}
        get {return soundFactor;}
    }

    public float Music {
        set {musicFactor = value;}
        get {return musicFactor;}
    }
}
