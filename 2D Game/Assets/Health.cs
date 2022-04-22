using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Defense")]
    public float maxHealth = 200f;
    public float currentHealth;
    public bool Alive = true;

    private Rigidbody2D rb;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage, float attackerX) {
        // subtract damage
        currentHealth -= damage;
        animator.SetTrigger("Hurt");

        // knocking enemy back
        if (rb.position.x < attackerX) {
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
        Alive = false;
        //die animation
        animator.SetBool("isDead", true);

        // disable collider & script
        Collider2D[] colliders = GetComponents<Collider2D>();

        foreach (Collider2D part in colliders) {
            part.enabled = false;
        }
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        if (GetComponent<EnemyTracking>() != null) {
            GetComponent<EnemyTracking>().followEnabled = false;
        }
        if (GetComponent<EnemyCombat>() != null) {
            GetComponent<EnemyCombat>().enabled = false;
        }
        this.enabled = false;
    }
}
