using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SetPauseName : MonoBehaviour
{
    [SerializeField]
    private TMP_Text tmptext;

    public void SetName(string name) {
        tmptext.text = "<-- " + name;
    }
}
