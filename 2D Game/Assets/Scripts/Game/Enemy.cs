using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public float speedCap = 5f;

    private EnemyTracking tracking;
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        tracking = GetComponent<EnemyTracking>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        animator.SetFloat("x", Mathf.Abs(rb.velocity.x));
        animator.SetBool("grounded", tracking.isGrounded);

        
    }

    void FixedUpdate() {
        // if too fast, slow down (falling ignored)
        if (Mathf.Abs(rb.velocity.x) > 6f) {
            rb.velocity = new Vector2((rb.velocity.x > 0 ? 5 : -5), rb.velocity.y);
        }
        if (rb.velocity.y > 15f) {
            rb.velocity = new Vector2(rb.velocity.x, 8);
        }  
    }

}
