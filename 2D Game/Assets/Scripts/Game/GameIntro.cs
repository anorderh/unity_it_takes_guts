using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameIntro : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioHost[] intros = GetComponents<AudioHost>();

        intros[Random.Range(0, 3)].Play();
    }
}
