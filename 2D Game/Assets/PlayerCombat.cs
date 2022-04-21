using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Defense")]
    public float maxHealth = 200f;
    public float currentHealth;

    [Header("Offense")]
    public LayerMask enemyLayers;
    public Transform attackPoint;
    public float attackRange = 1f;
    public int attackDamage = 35;
    public float attackRate;
    public float comboRate;

    public Animator animator;

    private float lastAttack = 0f;
    private bool attackFlag;
    private Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Attack") && animator.GetBool("grounded") && !animator.GetBool("crouched")) {
    
            Attack();
        }
    }

    void TakeDamage(float damage, float enemyX) {
        // subtract damage
        currentHealth -= damage;
        animator.SetTrigger("Hurt");

        // knocking enemy back
        if (rb.position.x < enemyX) {
            rb.AddForce(Vector2.left*15, ForceMode2D.Impulse);
        } else {
            rb.AddForce(Vector2.right*15, ForceMode2D.Impulse);
        }

        // check for death
        if (currentHealth <= 0) {
            playerDie();
        }
    }

    void playerDie() {
        //die animation
        animator.SetBool("isDead", true);

        // disable collider & script
        Collider2D[] playerColliders = GetComponents<Collider2D>();

        foreach (Collider2D part in playerColliders) {
            part.enabled = false;
        }
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        // this.enabled = false;
    }

    void Attack() {

        //animation
        if (Time.time > lastAttack + (animator.GetBool("combo") ? comboRate : attackRate)) {
            if (Time.time > lastAttack + comboRate || animator.GetBool("combo")) {
                animator.SetBool("combo", false);
            } else {
                animator.SetBool("combo", true);
            }
            lastAttack = Time.time;
        } else {
            return;
        }
        animator.SetTrigger("attack");

        //detect enemies
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        Collider2D pastCollider = null;

        //filtering multiple colldiers on 1 enemy
        foreach(Collider2D enemy in enemiesHit) {
            if (pastCollider == null || pastCollider.gameObject != enemy.gameObject) {


                enemy.GetComponent<Enemy>().TakeDamage(attackDamage, rb.position.x);
            }
            pastCollider = enemy;
        }


    }

    void OnDrawGizmosSelected() {
        if (attackPoint == null) {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
