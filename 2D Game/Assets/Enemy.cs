using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    [Range(0,500)] public int maxHealth = 100;
    int currentHealth;

    private EnemyTracking tracking;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        tracking = GetComponent<EnemyTracking>();
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        animator.SetFloat("x", Mathf.Abs(rb.velocity.x));
    }

    public void TakeDamage(int damage, float playerX) {
        // subtract damage
        currentHealth -= damage;
        animator.SetTrigger("Hurt");

        if (rb.position.x < playerX) {
            rb.AddForce(Vector2.left*15, ForceMode2D.Impulse);
        } else {
            rb.AddForce(Vector2.right*15, ForceMode2D.Impulse);
        }

        // check for death
        if (currentHealth <= 0) {
            Die();
        }
    }

    void Die() {
        //die animation
        animator.SetBool("isDead", true);

        // disable collider & script
        Collider2D[] enemyColliders = GetComponents<Collider2D>();

        foreach (Collider2D enemy in enemyColliders) {
            enemy.enabled = false;
        }
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        tracking.followEnabled = false;
        this.enabled = false;
    }
}
