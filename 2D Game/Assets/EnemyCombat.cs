using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public LayerMask damagableLayers;
    public Transform attackPoint;
    public float attackRange = 1f;
    public int attackDamage = 35;
    public float attackRate;
    public float comboRate;

    public Animator animator;

    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapCircle(attackPoint.position, attackRange, damagableLayers)) {

        }
        
    }

    void DetectPlayer() {
        Collider2D[] thingsHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, damagableLayers);

        if (thingsHit.Length > 0) {

            foreach(Collider2D thing in thingsHit) {
                if (thing.gameObject.GetComponent<player2DController>() != null) {
                    animator.SetBool("combo", !animator.GetBool("combo"));
                    animator.SetTrigger("attack");

                    // damage method
                }
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
