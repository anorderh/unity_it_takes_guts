using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private Animator menuAnimator;
    [SerializeField]
    private Animator selectAnimator;
    [SerializeField]
    private Animator settingsAnimator;

    private int width = 1600;
    private int height = 900;

    // Start is called before the first frame update
    void Start()
    {
        UpdateRes();
        ChangeMenuState(true);
    }

    public void PlayGame() {
        SceneManager.LoadScene("Game");
    }

    public void Quit() {
        Application.Quit();
    }

    public void SetWidth(int newWidth) {
        width = newWidth;
    }

    public void SetHeight(int newHeight) {
        height = newHeight;
    }

    public void UpdateRes() {
        Screen.SetResolution(width, height, false);
    }

    public void ChangeMenuState(bool state) {
        if (state) {
            menuAnimator.gameObject.SetActive(state);
            menuAnimator.SetBool("open", state);
        } else {
            menuAnimator.SetBool("open", state);
            menuAnimator.gameObject.SetActive(state);
        }
    }

    public void ChangeSelectState(bool state) {
        if (state) {
            selectAnimator.gameObject.SetActive(state);
            selectAnimator.SetBool("open", state);
        } else {
            selectAnimator.SetBool("open", state);
            selectAnimator.gameObject.SetActive(state);
        }
    }

    public void ChangeSettingsState(bool state) {
        if (state) {
            settingsAnimator.gameObject.SetActive(state);
            settingsAnimator.SetBool("open", state);
        } else {
            settingsAnimator.SetBool("open", state);
            settingsAnimator.gameObject.SetActive(state);
        }
    }

}
