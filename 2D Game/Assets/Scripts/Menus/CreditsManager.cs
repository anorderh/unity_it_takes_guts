using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    [SerializeField]
    private SettingsSO savedSettings;

    // Start is called before the first frame update
    void Start()
    {
        UpdateRes();
    }

    public void UpdateRes() {
        Screen.SetResolution(savedSettings.Resolution[0], savedSettings.Resolution[1], false);
    }
}
