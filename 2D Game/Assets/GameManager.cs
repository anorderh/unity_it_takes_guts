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

    void Start() {
        update.SendMsg("Eliminate " + enemyCount + " Demon Imps.");
    }

    public void startDeath() {
        GameObject[] deathParts = GameObject.FindGameObjectsWithTag("DeathScreen");

        foreach(GameObject part in deathParts) {
            part.GetComponent<HideComponent>().RevealComponent();
        }
    }

    public void startVictory() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies) {
            Destroy(enemy, 0.1f);
        }

        GameObject[] victoryParts = GameObject.FindGameObjectsWithTag("VictoryScreen");
        foreach(GameObject part in victoryParts) {
            part.GetComponent<HideComponent>().RevealComponent();
        }
    }

    public void Reload() {
        SceneManager.LoadScene("Game");
    }
}
