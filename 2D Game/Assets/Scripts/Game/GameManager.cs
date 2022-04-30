using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{

    public int enemyCount;
    public int enemiesPresent;
    public GameUpdate update;
    public bool gameOver;
    public static bool Paused = false;
    public GameObject pauseMenu;
    public GameObject player;

    private AudioHost[] hosts;

    [SerializeField] private AudioHost menuClick;
    [SerializeField] private AudioHost loseSFX;
    [SerializeField] private AudioHost winSFX;
    [SerializeField] private AudioHost myBrotherSFX;
    [SerializeField] private ColorSO savedColor;
    [SerializeField] private NameSO savedName;
    [SerializeField] private DifficultySO savedDifficulty;
    [SerializeField] private SettingsSO savedSettings;
    [SerializeField] private SetPauseName pauseName;

    void Start() {
        // getting MAC and difficulty settings
        enemyCount = savedDifficulty.inputEnemies[0];
        enemiesPresent = savedDifficulty.inputEnemies[1];

        // initialize game start
        gameOver = false;
        update.SendMsg("Eliminate " + savedDifficulty.inputEnemies[0] + " Demon Imps.");
        hosts = FindObjectsOfType<AudioHost>();
        pauseName.SetName(savedName.playerName);

        // // init player
        player.GetComponent<SpriteRenderer>().material.color = Color.HSVToRGB(savedColor.Hue, savedColor.Saturation, savedColor.Brightness);

        // init pause menu
        pauseMenu.GetComponentInChildren<SetPauseName>().SetName(savedName.playerName);
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause") && !gameOver) {
            PlayClick();
            if (Paused) {
                Resume();
            } else {
                Pause();
            }
        }

    }

    void FixedUpdate() {
        if (hosts.SequenceEqual(FindObjectsOfType<AudioHost>())) {
            Debug.Log("refreshed audio");
            hosts = FindObjectsOfType<AudioHost>();
            Debug.Log(hosts.Length);
        }
    }

    public void PauseAudio() {
        foreach(AudioHost host in hosts) {
            host.Pause();
        }
    }

    public void ReleaseAudio() {
        foreach(AudioHost host in hosts) {
            host.UnPause();
        }
    }

    public void Resume() {
        ReleaseAudio();
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
    }

    void Pause() {
        PauseAudio();
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
    }

    public void startDeath() {
        if (myBrotherSFX.isPlaying()) {
            myBrotherSFX.Stop();
        }
        
        loseSFX.Play();
        gameOver = true;
        GameObject[] deathParts = GameObject.FindGameObjectsWithTag("DeathScreen");

        foreach(GameObject part in deathParts) {
            part.GetComponent<HideComponent>().RevealComponent();
        }
    }

    public void startVictory() {
        if (myBrotherSFX.isPlaying()) {
            myBrotherSFX.Stop();
        }

        winSFX.Play();
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

    public void StartMyBrother() {
        myBrotherSFX.Play();
        myBrotherSFX.Pause();
    }

    public void StopMyBrother() {
        myBrotherSFX.Stop();
    }

    public void PlayGame() {
        PlayClick();
        Resume();
        SceneManager.LoadScene("Game");
    }

    public void MainMenu() {
        PlayClick();
        Resume();
        SceneManager.LoadScene("Menu");
    }

    public void Quit() {
        PlayClick();
        Application.Quit();
    }

    public void PlayClick() {
        menuClick.UnPause();
        menuClick.Play();
    }
}
