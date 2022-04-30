using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameUpdate : MonoBehaviour
{

    private TMP_Text tmptext;

    // Start is called before the first frame update
    void Start()
    {
        tmptext = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    public void SendMsg(String msg) {
        tmptext.text = msg;
        GetComponent<HideComponent>().RevealText();
    }
}
