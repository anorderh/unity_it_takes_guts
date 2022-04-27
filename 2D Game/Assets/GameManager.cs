using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{

    // private float animationGap = 0.5f;
    // private float animationStamp = 0f;

    public int enemyCount;
    public int enemiesPresent;
    public GameUpdate update;
    public bool gameOver;
    public static bool Paused = false;
    public GameObject pauseMenu;
    public GameObject player;

    private List<AudioSource> sources;

    [SerializeField] private AudioSource menuClick;
    [SerializeField] private AudioSource loseSFX;
    [SerializeField] private AudioSource winSFX;
    [SerializeField] private AudioSource myBrotherSFX;
    [SerializeField] private ColorSO savedColor;
    [SerializeField] private NameSO savedName;


    void Start() {
        // initialize game start
        gameOver = false;
        update.SendMsg("Eliminate " + enemyCount + " Demon Imps.");
        sources = new List<AudioSource>(FindObjectsOfType<AudioSource>());

        // // init player
        // savedColor.hue = 0.5f;
        player.GetComponent<SpriteRenderer>().material.color = Color.HSVToRGB(savedColor.Hue, savedColor.Saturation, savedColor.Brightness);

        // init pause menu
        pauseMenu.GetComponentInChildren<SetPauseName>().SetName(savedName.playerName);
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause") && !gameOver) {
            PlayClick();
            if (Paused) {
                ReleaseAudio();
                Resume();
            } else {
                StopAudio();
                Pause();
            }
        }

    }

    void FixedUpdate() {
        if (!sources.ToArray().SequenceEqual(FindObjectsOfType<AudioSource>())) {
            Debug.Log("refreshed audio");
            sources = new List<AudioSource>(FindObjectsOfType<AudioSource>());
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

    void StopAudio() {
        foreach(AudioSource src in sources) {
            src.Pause();
        }
    }

    void ReleaseAudio() {
        foreach(AudioSource src in sources) {
            src.UnPause();
        }
    }

    public void startDeath() {
        if (myBrotherSFX.isPlaying) {
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
        if (myBrotherSFX.isPlaying) {
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
