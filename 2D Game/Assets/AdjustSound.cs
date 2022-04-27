using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustSound : MonoBehaviour
{
    [SerializeField]
    private SettingsSO settings;
    private AudioSource[] sources;

    // Start is called before the first frame update
    void Start()
    {
       sources = GetComponents<AudioSource>();
       UpdateSound();
    }

    public void UpdateSound() {
        foreach(AudioSource src in sources) {
            src.volume = src.volume*settings.Sound;
        }
    }
}
