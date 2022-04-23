using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Health : MonoBehaviour
{
    [Header("Defense")]
    public float maxHealth = 200f;
    public bool Alive = true;
    public float pushBack = 15;
    public float currentHealth;
    public HealthUpdate updateScript;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isPlayer; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        isPlayer = CheckPlayer();
    }

    public void TakeDamage(float damage, float attackerX) {
        if (isPlayer) {
            updateScript.UpdateBar(currentHealth - damage, maxHealth);
        }

        // subtract damage
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        Debug.Log("health: " + currentHealth + "/" + maxHealth);

        // knocking enemy back
        if (rb.position.x < attackerX) {
            rb.AddForce(Vector2.left*pushBack, ForceMode2D.Impulse);
        } else {
            rb.AddForce(Vector2.right*pushBack, ForceMode2D.Impulse);
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

        // disable collider, scripts, & animators
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D part in colliders) {
            part.enabled = false;
        }

        // freeze body in place
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        if (isPlayer) {
            disablePlayer();
        } else {
            disableEnemy();
        }

        this.enabled = false;
    }

    bool CheckPlayer() {
        if (GetComponent<EnemyTracking>() != null) {
            return false;
        } else {
            return true;
        }
    }

    void disableEnemy() {
        GetComponent<EnemyTracking>().enabled = false;
        GetComponent<EnemyCombat>().enabled = false;
        GetComponent<Enemy>().enabled = false;
        GetComponent<Seeker>().enabled = false;
    }

    void disablePlayer() {
        GetComponent<PlayerCombat>().enabled = false;
        GetComponent<player2DController>().enabled = false;
    }
}
