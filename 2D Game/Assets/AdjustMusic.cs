using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustMusic : MonoBehaviour
{
    [SerializeField]
    private SettingsSO settings;
    private AudioSource[] sources;

    // Start is called before the first frame update
    void Start()
    {
       sources = GetComponents<AudioSource>();
       UpdateMusic();
    }

    public void UpdateMusic() {
        foreach(AudioSource src in sources) {
            src.volume = src.volume*settings.Music;
        }
    }
}
