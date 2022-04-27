using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ColorSO : ScriptableObject
{
    [SerializeField]
    private float _hue = 0f;
    private float _saturation = 0f;
    private float _brightness = 1f;

    public float[] color {
        set {
            _hue = value[0];
            _saturation = value[1];
        }
    }

    public float Hue {
        get {return _hue;}
    }

    public float Saturation {
        get {return _saturation;}
    }

    public float Brightness {
        get {return _brightness;}
    }

    public void Reset() {
        _hue = 0f;
        _saturation = 0f;
    }
}
