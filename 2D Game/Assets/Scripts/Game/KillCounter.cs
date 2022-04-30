using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class KillCounter : MonoBehaviour
{
    public GameManager manager;
    private TMP_Text TMPtext;
    private int kills = 0;

    void Start() {
        manager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        TMPtext = GetComponent<TMP_Text>();
        TMPtext.text = "" + kills;
    }

    public void IncrementKill() {
        kills++;
        TMPtext.text = "" + kills;

        if (manager.enemyCount <= kills) {
            manager.startVictory();
        }
    }
}
