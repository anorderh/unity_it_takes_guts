using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyTracking : MonoBehaviour
{
     [Header("Pathfinding")]
    public Transform target;
    public float activateDistance = 50f;
    public float pathUpdateSeconds = 0.5f;
    [SerializeField] public LayerMask whatIsGround;
    public Transform groundPoint;

    [Header("Physics")]
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float jumpNodeHeightRequirement = 0.2f;
    public float jumpModifier = 0.3f;
    public float jumpCheckOffset = 0.1f;

    [Header("Custom Behavior")]
    public bool followEnabled = true;
    public bool jumpEnabled = true;
    public bool directionLookEnabled = true;

    private Path path;
    private float angle;
    private int currentWaypoint = 0;
    public bool isGrounded;
    private Vector2 force = Vector2.zero;
    Seeker seeker;
    Rigidbody2D rb;
    private Collider2D[] enemyColliders;
    private Vector3 startOffset;

    void Start() {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    void FixedUpdate() {
        if (DetectSlope()) {
            rb.gravityScale = 0.4f;
        } else {
            rb.gravityScale = 3;
        }

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

        // direction && calculating force towards target
        Vector2 direction = ((Vector2)target.transform.position - rb.position).normalized;
        force = direction * speed * Time.deltaTime;

        // check Jump
        if (jumpEnabled && isGrounded) {
            if (direction.y > jumpNodeHeightRequirement) {
                rb.AddForce(Vector2.up * speed * jumpModifier);
            }
        }

        force = rb.gravityScale > 1 ? force : force*0.8f;
        if (Mathf.Abs(rb.velocity.x) < 5f) {
            rb.AddForce(force);
        }

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

    bool DetectSlope() {
        if (isGrounded && Mathf.Abs(rb.velocity.y) > 0) {
            return true;
        } else {
            return false;
        }
    }

    void OnDrawGizmosSelected() {
        if (groundPoint == null) {
            return;
        }

        Gizmos.DrawWireSphere(groundPoint.position, 0.1f);
    }
 
}
