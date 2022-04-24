using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    // private float animationGap = 0.5f;
    // private float animationStamp = 0f;

    public int enemyCount;
    public GameUpdate update;
    public bool gameOver;
    public static bool Paused = false;
    public GameObject pauseMenu;
    public GameObject player;

    void Start() {
        gameOver = false;
        update.SendMsg("Eliminate " + enemyCount + " Demon Imps.");
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause") && !gameOver) {
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

    public void startDeath() {
        gameOver = true;
        GameObject[] deathParts = GameObject.FindGameObjectsWithTag("DeathScreen");

        foreach(GameObject part in deathParts) {
            part.GetComponent<HideComponent>().RevealComponent();
        }
    }

    public void startVictory() {
        gameOver = true;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies) {
            Destroy(enemy, 0.1f);
        }

        GameObject[] victoryParts = GameObject.FindGameObjectsWithTag("VictoryScreen");
        foreach(GameObject part in victoryParts) {
            part.GetComponent<HideComponent>().RevealComponent();
        }
    }

    public void PlayGame() {
        Resume();
        SceneManager.LoadScene("Game");
    }

    public void MainMenu() {
        Resume();
        SceneManager.LoadScene("Menu");
    }

    public void Quit() {
        Application.Quit();
    }
}
