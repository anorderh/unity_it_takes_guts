using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyTracking : MonoBehaviour
{
     [Header("Pathfinding")]
    public float activateDistance = 50f;
    public float pathUpdateSeconds = 0.5f;
    [SerializeField] public LayerMask whatIsGround;
    public Transform groundPoint;
    public Transform ceilingPoint;

    [Header("Physics")]
    public float speed = 200f;
    public float speedCap = 5f;
    public float nextWaypointDistance = 3f;
    public float jumpNodeHeightRequirement = 0.2f;
    public float highJumpHeightRequirement = 0.8f;
    public float jumpModifier = 0.3f;
    public float jumpCheckOffset = 0.1f;
    public float jumpPause = 0.1f;

    [Header("Custom Behavior")]

    public bool followEnabled = true;
    public bool jumpEnabled = true;
    public bool directionLookEnabled = true;

    private Transform target;
    private Path path;
    private float jumpTimestamp;
    private float angle;
    private float tempHeightReq;
    private int currentWaypoint = 0;
    public bool isGrounded;
    public bool ceilingAbove;
    public bool hanging;
    private Vector2 direction;
    private Vector2 force = Vector2.zero;
    Seeker seeker;
    Rigidbody2D rb;
    private Collider2D[] enemyColliders;
    private Vector3 startOffset;
    private Animator animator;
    private EnemyAudioControl ac;

    void Start() {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target =  GameObject.FindWithTag("Player").transform;
        ac = GetComponentInChildren<EnemyAudioControl>();

        // speed cap
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, speedCap);
        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    void FixedUpdate() {
        AdjustForSlope();

        if (TargetInDistance() && followEnabled) {
            PathFollow();
        }
    }

    void UpdatePath() {
        if (followEnabled && TargetInDistance() && seeker.IsDone()) {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void PathFollow() {
        // no path present
        if (path == null) {
            return;
        }

        // current fulfilled waypoint is latest generated, reached path's end
        if (currentWaypoint >= path.vectorPath.Count) {
            return;
        }

        // checking if onGround
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, 0.1f, whatIsGround);
        ceilingAbove = Physics2D.OverlapCircle(ceilingPoint.position, 0.1f, whatIsGround);

        // direction && calculating force towards target
        direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        force = direction * speed * Time.deltaTime;

        // if hanging, always jump no matter relative heights
        if (hanging) {
            tempHeightReq = 0.1f;
        } else {
            tempHeightReq = jumpNodeHeightRequirement;
        }

        // check Jump
        if (jumpEnabled && (isGrounded || hanging) && !ceilingAbove && (Time.time > jumpTimestamp)) {
            if ((direction.y > tempHeightReq && Mathf.Abs(rb.velocity.x) < 3)) {
                // slow down when jumping
                rb.velocity = new Vector2(1, rb.velocity.y);


                rb.AddForce(Vector2.up * speed * (direction.y > highJumpHeightRequirement ? jumpModifier*1f : jumpModifier));
                jumpTimestamp = Time.time + jumpPause;

            }
        }

        // adjusting force if grav. low for ramps, and applying
        force = rb.gravityScale > 1 ? force : force*0.8f;
        if (animator.GetBool("attacking")) {
            rb.AddForce(-(force/2));
        }
        rb.AddForce(force);


        // finding distance to curWaypoint, to check if next WayPoint should be pursued
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance) {
            currentWaypoint++;
        }

        if (directionLookEnabled) {
            if (rb.position.x < target.transform.position.x)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (rb.position.x > target.transform.position.x)
            {
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }        
        }
    }

    bool TargetInDistance() {
        // checking if curPosition & target position are close enough to "aggro"
        return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
    }

    void OnPathComplete(Path p) {
        if (!p.error) {
            path = p;
            currentWaypoint = 0;
        }
    }

    Collider2D GetMainCollider() {
        enemyColliders = GetComponents<Collider2D>();

        foreach (Collider2D enemy in enemyColliders) {
            if (enemy is BoxCollider2D) {
                return enemy;
            }
        }
        return null;
    }

    void AdjustForSlope() {
        if (isGrounded && Mathf.Abs(rb.velocity.y) > 0) {
            rb.gravityScale = 0.4f;
        } else {
            rb.gravityScale = 3;
        }
    }

    // bool StuckOnLedge() {
    //     if (direction.y > 0 && Mathf.Approximately(direction.x, 0)) {
    //         Debug.Log("stuck on ledge");
    //         return true;
    //     } else {
    //         return false;
    //     }
    // }

    void OnDrawGizmosSelected() {
        if (groundPoint == null) {
            return;
        }

        Gizmos.DrawWireSphere(groundPoint.position, 0.1f);
        Gizmos.DrawWireSphere(ceilingPoint.position, 0.1f);
    }
}
