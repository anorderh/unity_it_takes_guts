using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundDisplay : MonoBehaviour
{
    public Slider slider;

    [SerializeField]
    private SettingsSO settings;
    [SerializeField]
    private AdjustSound adjust;
    [SerializeField]
    private TMP_Text tmptext;
    private float curSound;


    void Start() {
        slider.value = curSound = settings.Sound;
        tmptext = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        tmptext.text = slider.value.ToString("0.00");
        if (slider.value != curSound) {
            curSound = slider.value;
            settings.Sound = curSound;
            adjust.UpdateSound();
            Debug.Log("requested update");
        }
    }
}
