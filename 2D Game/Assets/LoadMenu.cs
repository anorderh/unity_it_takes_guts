using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{

    [SerializeField]
    private AudioSource clickSFX;

    public void Return() {
        PlayClick();
        SceneManager.LoadScene("Menu");
    }

    public void PlayClick() {
        clickSFX.Play();
    }
}
