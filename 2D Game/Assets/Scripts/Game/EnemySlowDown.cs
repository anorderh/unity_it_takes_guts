using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlowDown : MonoBehaviour
{

    private Animator animator;
    private Rigidbody2D rb;

    void Start() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("attacking")) {
            rb.AddForce(-(rb.velocity*0.75f));
        }
    }
}
