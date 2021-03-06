using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    public Image On;
    public Image Off;
    public bool status = false;
    
    private GameManager manager;

    void Start() {
        manager =  GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        On.enabled = false;
    }

    public void PushToggle() {
        if (!status) {
            manager.StartMyBrother();

            status = true;
            Off.enabled = false;
            On.enabled = true;
        } else {
            manager.StopMyBrother();

            status = false;
            Off.enabled = true;
            On.enabled = false;
        }
    }
}
