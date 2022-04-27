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

    public float hue {
        set {
            if (value == 0) {
                _saturation = 0f;
            } else {
                _saturation = 0.4f;
                _hue = value;
            }
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
}
