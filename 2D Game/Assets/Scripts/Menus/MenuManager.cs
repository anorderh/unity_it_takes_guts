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
    [SerializeField]
    private AudioHost clickSFX;

    [SerializeField] private ColorSO savedColor;
    [SerializeField] private NameSO savedName;
    [SerializeField] private DifficultySO savedDifficulty;
    [SerializeField] private SettingsSO savedSettings;

    // Start is called before the first frame update
    void Start()
    {   
        ResetPlayerData();
        // UpdateRes();
        ChangeMenuState(true);
    }

    public void ResetPlayerData() {
        savedColor.Reset();
        savedName.Reset();
        savedDifficulty.Reset();
    }

    public void PlayGame() {
        SceneManager.LoadScene("Game");
    }

    public void Quit() {
        savedSettings.Reset();
        Application.Quit();
    }

    public void SetWidth(int newWidth) {
        savedSettings.width = newWidth;
    }

    public void LoadCredits() {
        SceneManager.LoadScene("Credits");
    }

    public void SetHeight(int newHeight) {
        savedSettings.height = newHeight;
    }

    public void UpdateRes() {
        Screen.SetResolution(savedSettings.Resolution[0], savedSettings.Resolution[1], false);
    }

    public void PlayClick() {
        clickSFX.Play();
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
