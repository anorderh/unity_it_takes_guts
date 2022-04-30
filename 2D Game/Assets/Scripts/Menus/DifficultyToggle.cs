using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DifficultyToggle : MonoBehaviour
{
    public Image Easy;
    public Image Medium;
    public Image Hard;
    public int choice = 0;
    [SerializeField] private DifficultySO difficulty;
    [SerializeField] private TMP_Text tmptext;

    private int[] EasySettings = {10, 3};
    private int[] MedSettings = {25, 5};
    private int[] HardSettings = {50, 10};
    private Image[] icons;
    private int[][] settings;
    private string[] names = {"EASY", "MEDIUM", "HARD"};

    void Start() {
        icons = new Image[]{Easy, Medium, Hard};
        settings = new int[][]{EasySettings, MedSettings, HardSettings};
        SetDifficulty();
    }

    public void UpdateDifficulty() {
        if (choice + 1 == settings.Length) {
            choice = 0;
        } else {
            choice++;
        }

        SetDifficulty();
    }

    void SetDifficulty() {
        for(int i = 0; i < icons.Length; i++) {
            if (choice == i) {
                icons[i].enabled = true;
                difficulty.inputEnemies = settings[i];
                tmptext.text = names[i];
            } else {
                icons[i].enabled = false;
            }
        }
    }
}
