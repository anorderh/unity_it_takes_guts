using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    public GameObject OnSprite;
    public GameObject OffSprite;
    public bool status = true;
    public AudioSource myBrotherMusic;
    
    private GameManager manager;

    void Start() {
        manager =  GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        myBrotherMusic = GetComponent<AudioSource>();
        PushToggle();
    }

    public void PushToggle() {
        if (!status) {
            myBrotherMusic.Play();
            myBrotherMusic.Pause();
            manager.sources.Add(myBrotherMusic);

            status = true;
            OffSprite.SetActive(false);
            OnSprite.SetActive(true);
        } else {
            myBrotherMusic.Stop();

            status = false;
            OffSprite.SetActive(true);
            OnSprite.SetActive(false);
        }
    }
}
