using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public LayerMask damagableLayers;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int attackDamage = 35;
    public float attackRate = 10f;

    private Animator animator;
    private Rigidbody2D rb;
    private Health health;
    private float attackTime;
    // private bool attacking = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        attackTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (health.Alive) {
            if (Physics2D.OverlapCircle(attackPoint.position, attackRange, damagableLayers) && (Time.time > attackTime)) {
                    animator.SetTrigger("attack");
            }
        } else {
            this.enabled = false;
        }
    }

    public void Attack() {
        Collider2D[] thingsHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, damagableLayers);

        if (thingsHit.Length > 0) {
            Collider2D pastCollider = null;

            foreach(Collider2D thing in thingsHit) {
                // check if object has Health
                if (thing.gameObject.GetComponent<Health>() != null) {
                    // check if Collider gameObject is not same as last Collider
                    if (pastCollider == null || (thing.gameObject != pastCollider.gameObject)) {
                        thing.gameObject.GetComponent<Health>().TakeDamage(attackDamage, rb.position.x);
                    }
                }
                pastCollider = thing;
            }
        }

    }

    void OnDrawGizmosSelected() {
        if (attackPoint == null) {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
