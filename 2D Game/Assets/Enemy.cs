using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    [Range(0,500)] public int maxHealth = 100;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage) {
        // subtract damage
        currentHealth -= damage;
        animator.SetTrigger("Hurt");

        Debug.Log("Enemy health: " + currentHealth);

        // check for death
        if (currentHealth <= 0) {
            Die();
        }
    }

    void Die() {
        //die animation
        animator.SetBool("isDead", true);

        // disable colliders & script
        Collider2D[] colliders = GetComponents<Collider2D>();

        foreach (Collider2D collider in colliders) {
            collider.enabled = false;
        }
        this.enabled = false;
    }
}
