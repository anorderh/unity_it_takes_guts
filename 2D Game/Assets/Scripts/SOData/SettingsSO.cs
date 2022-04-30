using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SettingsSO : ScriptableObject
{
    [SerializeField]
    private float soundFactor = 1f;
    [SerializeField]
    private int[] res = {1600, 900};

    public float Sound {
        set {soundFactor = value;}
        get {return soundFactor;}
    }

    public int[] Resolution {
        get {return res;}
    }

    public int width {
        set {res[0] = value;}
    }

    public int height {
        set {res[1] = value;}
    }

    public void Reset() {
        soundFactor = 1f;
    }
}
