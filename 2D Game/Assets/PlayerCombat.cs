using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;
    public LayerMask enemyLayers;
    public Transform attackPoint;
    public float attackRange = 1f;
    public int attackDamage = 35;
    public float attackRate;
    public float comboRate;

    private float lastAttack = 0f;
    private bool attackFlag;
    private Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Attack") && animator.GetBool("grounded") && !animator.GetBool("crouched")) {
            if (!animator.GetBool("attacking")) {
                animator.SetBool("attacking", true);
            }
        
            Attack();
        }

        if (Time.time > lastAttack + comboRate) {
            animator.SetBool("attacking", false);
        }
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
