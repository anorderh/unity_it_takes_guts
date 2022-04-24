using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCenter : MonoBehaviour
{
    public static bool Paused = false;
    public GameManager manager;
    public GameObject pauseMenu;


    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause") && !manager.gameOver) {
            if (Paused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Resume() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
    }

    void Pause() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
    }
}
