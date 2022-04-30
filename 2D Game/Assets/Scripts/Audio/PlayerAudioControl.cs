using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioControl : MonoBehaviour
{

    public Animator animator;
    public Health health;
    [SerializeField] private AudioHost runSFX;
    [SerializeField] private AudioHost crouchSFX;
    [SerializeField] private AudioHost jumpSFX;
    [SerializeField] private AudioHost swingSFX;
    [SerializeField] private AudioHost swingSFX2;
    [SerializeField] private AudioHost hitSFX;
    [SerializeField] private AudioHost hitSFX2;
    [SerializeField] private AudioHost hurtSFX;
    [SerializeField] private AudioHost deadSFX;
    [SerializeField] private AudioHost rollSFX;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        health = GetComponentInParent<Health>();
        runSFX.Play();
        crouchSFX.Play();
    }

    void FixedUpdate() {
        if (animator.GetBool("grounded") && Mathf.Abs(animator.GetFloat("speed")) > 0.1
            && !animator.GetBool("attacking") && !animator.GetBool("rolling")) {
                if (animator.GetBool("crouched")) {
                    StartCrouchWalking();
                } else {
                    StartRunning();
                }
            } else {
                NotMoving();
            }
    }

    public void PlayJump() {
        jumpSFX.Play();
    }

    public void PlaySwing() {
        if (animator.GetBool("combo")) {
            swingSFX.Play();
        } else {
            swingSFX2.Play();
        }
    }

    public void PlayHit() {
        if (animator.GetBool("combo")) {
            hitSFX.Play();
        } else {
            hitSFX2.Play();
        }
    }

    
    public void StartRunning() {
        StopCrouchWalking();
        runSFX.UnPause();
    }

    public void StartCrouchWalking() {
        StopRunning();
        crouchSFX.UnPause();
    }

    public void StopRunning() {
        runSFX.Pause();
    }

    public void StopCrouchWalking() {
        crouchSFX.Pause();
    }

    public void NotMoving() {
        StopRunning();
        StopCrouchWalking();
    }

    public void PlayHurt() {
        hurtSFX.Play();
    }

    public void PlayDead() {
        deadSFX.Play();
    }

    public void PlayRoll() {
        rollSFX.Play();
    }
}
