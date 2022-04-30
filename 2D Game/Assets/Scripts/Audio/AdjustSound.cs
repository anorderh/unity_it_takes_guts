using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustSound : MonoBehaviour
{
    [SerializeField]
    private SettingsSO settings;
    private AudioHost[] hosts;

    // Start is called before the first frame update
    void Start()
    {
       hosts = GetComponents<AudioHost>();
       UpdateSound();
    }

    public void UpdateSound() {
        Debug.Log("updated volume");
        foreach(AudioHost host in hosts) {
            host.SetVolume(settings.Sound);
        }
    }
}
