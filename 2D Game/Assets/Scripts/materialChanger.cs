using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slowDown : MonoBehaviour
{
    [SerializeField] public int slowMass;
    [SerializeField] public int normalMass;

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
            rb.mass = slowMass;
        } else {
            rb.mass = normalMass;
        }
    }
}
