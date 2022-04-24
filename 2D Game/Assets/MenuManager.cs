using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayGame() {
        SceneManager.LoadScene("Game");
    }

    public void Settings() {
        SceneManager.LoadScene("Settings");
    }

    public void Quit() {
        Application.Quit();
    }
}
