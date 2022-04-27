using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MusicDisplay : MonoBehaviour
{
    public Slider slider;
    [SerializeField]
    private SettingsSO settings;

    [SerializeField]
    private TMP_Text valueHeader;

    private TMP_Text tmptext;
    private float curMusic = 1f;

    void Start() {
         tmptext = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tmptext.text = slider.value.ToString("0.00");
        if (slider.value != curMusic) {
            curMusic = slider.value;
            SaveValue(slider.value);
        }
    }

    void SaveValue(float newVal) {
        if (valueHeader.text == "MUSIC") {
            settings.Music = newVal;
        } else if (valueHeader.text == "SOUND") {
            settings.Sound = newVal;
        }
    }
}
