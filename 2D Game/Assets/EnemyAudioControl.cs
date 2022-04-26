using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioControl : MonoBehaviour
{
    public Animator animator;
    public Health health;
    [SerializeField] private AudioSource runSFX;
    [SerializeField] private AudioSource swingSFX;
    [SerializeField] private AudioSource hurtSFX;
    [SerializeField] private AudioSource deadSFX;
    [SerializeField] private AudioSource HighHealthSFX;
    [SerializeField] private AudioSource MedHealthSFX;
    [SerializeField] private AudioSource LowHealthSFX;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        health = GetComponentInParent<Health>();
        runSFX.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("grounded") && Mathf.Abs(animator.GetFloat("x")) > 0.1
            && !animator.GetBool("attacking") && Time.timeScale != 0f) {
                StartRunning();
            } else {
                StopRunning();
            }
    }

    void StartRunning() {
        runSFX.UnPause();
    }

    void StopRunning() {
        runSFX.Pause();
    }

    public void PlaySwing() {
        swingSFX.Play();
    }

    public void PlayHurt() {
        hurtSFX.Play();
    }

    public void PlayDead() {
        deadSFX.Play();
    }

    public void PlayMood(float cur, float max) {
        if (cur < max*0.5) {
            LowHealthSFX.Play();
        } else if (cur < max*0.75) {
            MedHealthSFX.Play();
        } else {
            HighHealthSFX.Play();
        }
    }

}
