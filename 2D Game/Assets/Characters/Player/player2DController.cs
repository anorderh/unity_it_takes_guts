using UnityEngine;

public class player2DController : MonoBehaviour
{

    public float speed = 1f;
    public int jumpPower = 10;
    public Collider2D standing;
    public Collider2D crouch;


    private bool crouchFlag;
    private float movement;
    private Vector3 m_Velocity = Vector3.zero;
    private Rigidbody2D _rigidbody;
    private Animator animator;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        crouchFlag = false;
    }

    // Update is called once per frame
    private void Update()
    {
        isCrouched();
        if (crouchFlag) {
            movement = Input.GetAxisRaw("Horizontal") * (speed/3);
        } else {
            movement = Input.GetAxisRaw("Horizontal") * speed;
        }
        movement *= Time.fixedDeltaTime;

        standing.enabled = !animator.GetBool("crouched");
        crouch.enabled = !(standing.enabled);


        animator.SetFloat("speed", Mathf.Abs(movement));
        animator.SetFloat("y", _rigidbody.velocity.y);


        if (Mathf.Abs(_rigidbody.velocity.y) < 0.0001f)  {
            if (Input.GetButtonDown("Jump"))
                _rigidbody.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
        
        } 
        
    }

    void isCrouched() {
        if (Input.GetButtonDown("Crouch")) {
            animator.SetBool("crouched", true);
            crouchFlag = true;

        } else if (Input.GetButtonUp("Crouch")) {
            animator.SetBool("crouched", false);
            crouchFlag = false;
        }
    }

    void FixedUpdate () 
    {

        Vector3 targetVelocity = new Vector2(movement, _rigidbody.velocity.y);
        _rigidbody.velocity = Vector3.SmoothDamp(_rigidbody.velocity, targetVelocity, ref m_Velocity, .1f);
        
        if (!Mathf.Approximately(0, movement)) 
            transform.rotation = movement > 0 ? Quaternion.identity : Quaternion.Euler(0, 180, 0);
    }

}
