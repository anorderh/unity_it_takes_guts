using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanClimb : MonoBehaviour
{
    public Transform RWallCheck;
    public Transform LWallCheck;
    [SerializeField] public LayerMask whatIsGround;

    private Rigidbody2D rb;
    private bool hangFlag;
    private EnemyTracking tracking;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tracking = GetComponent<EnemyTracking>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapBox(RWallCheck.position, new Vector2(0.05f, 0.8f), 0f, whatIsGround) ||
            Physics2D.OverlapBox(LWallCheck.position, new Vector2(0.05f, 0.8f), 0f, whatIsGround) ) {
            tracking.hanging = true;
        } else {
            tracking.hanging = false;
        }
    }

    void FixedUpdate() {
        if (tracking.hanging && rb.velocity.y < 0) {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y/2);
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.DrawCube(RWallCheck.position, new Vector3(0.05f, 0.8f, 0));
        Gizmos.DrawCube(LWallCheck.position, new Vector3(0.05f, 0.8f, 0));
    }
}
