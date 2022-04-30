using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHost : MonoBehaviour
{
    [SerializeField]
    public AudioSource source;
    private float origVolume;

    // Start is called before the first frame update
    void Awake() {
        origVolume = source.volume;
    }

    public void SetVolume(float scale) {
        source.volume = origVolume*scale;
    }

    public void Play() {
        source.Play();
    }

    public void Stop() {
        source.Stop();
    }

    public void Pause() {
        source.Pause();
    }

    public void UnPause() {
        source.UnPause();
    }

    public bool isPlaying() {
        return source.isPlaying;
    }

}
