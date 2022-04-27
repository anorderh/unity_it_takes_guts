using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeriveColor : MonoBehaviour
{

    [SerializeField] private Image colorBlock;
    [SerializeField] private ColorSO colorSO;

    private float hue;
    private float saturation;
    private float brightness;

    public void ChooseHue() {
        Color.RGBToHSV(colorBlock.color, out hue, out saturation, out brightness);
        colorSO.hue = hue;
        GameObject.FindWithTag("Player").GetComponent<PlayerColor>().Change(Color.HSVToRGB(hue, saturation, brightness));
    }
}
