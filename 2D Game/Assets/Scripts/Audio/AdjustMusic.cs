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
     }
}
