using UnityEngine;

public class player2DController : MonoBehaviour
{

    public float speed = 2;
    public float jumpPower = 5;
    public float movement;

    private Rigidbody2D _rigidbody;
    private Animator animator;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!inAir() || !isCrouched()) {
            movement = Input.GetAxis("Horizontal");

            if (Input.GetButtonDown("Jump"))
                _rigidbody.AddForce(new Vector2(0, jumpPower));
        
        } else {
            movement = 0;
        }

        animator.SetFloat("speed", Mathf.Abs(movement));
        
    }

    bool inAir() {
        if (Mathf.Abs(_rigidbody.velocity.y) < 0.001f) {
            animator.SetBool("inAir", false);
            return false;
        } else {
            animator.SetBool("inAir", true);
            return true;
        }
    }

    bool isCrouched() {
        if (Input.GetButtonDown("Crouch")) {
            animator.SetBool("crouched", true);
            return true;
        } else {
            animator.SetBool("crouched", false);
            return false;
        }
    }

    void FixedUpdate () 
    {
        // change position
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * speed;
        // change direction
        if (!Mathf.Approximately(0, movement)) 
            transform.rotation = movement > 0 ? Quaternion.identity : Quaternion.Euler(0, 180, 0);
    }
}
