using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TextChange: MonoBehaviour
{
    private TMP_Text tmptext;

    private Color32 origColor;

    void Start() {
        tmptext = GetComponentInChildren<TMP_Text>();
        origColor = tmptext.color;
    }

    public void Push() {
        GetComponent<Animator>().SetTrigger("pushed");
    }

    public void ChangeColor() {
        tmptext.color = new Color32(128, 128, 128, 255);
    }

    public void RevertColor() {
        tmptext.color = origColor;
    }
}
