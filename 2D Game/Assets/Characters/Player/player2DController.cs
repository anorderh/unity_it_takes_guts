using UnityEngine;

public class player2DController : MonoBehaviour
{

    [Range(0,5)] [SerializeField] public float speed = 1f;
    [Range(0,50)]  [SerializeField] public int jumpPower = 10;
    [SerializeField] public LayerMask whatIsGround;
    public Collider2D standing;
    public Transform ceilingCheck;
    public Transform groundCheck;


    private float ceilingRadius = 0.2f;
    private bool crouchFlag;
    private bool groundFlag;
    private float movement;
    private Vector3 m_Velocity = Vector3.zero;
    private Rigidbody2D _rigidbody;
    private Animator animator;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        crouchFlag = false;
        groundFlag = true;
    }

    // Update is called once per frame
    private void Update()
    {
        isCrouched();
        isGrounded();

        if (crouchFlag) {
            movement = Input.GetAxisRaw("Horizontal") * (speed/3);
        } else {
            movement = Input.GetAxisRaw("Horizontal") * speed;
        }
        movement *= Time.fixedDeltaTime;

        standing.enabled = !animator.GetBool("crouched");


        animator.SetFloat("speed", Mathf.Abs(movement));
        animator.SetFloat("y", _rigidbody.velocity.y);
        if (crouchFlag != animator.GetBool("crouched")) {
            animator.SetBool("crouched", crouchFlag);
        }
        if (groundFlag != animator.GetBool("grounded")) {
            animator.SetBool("grounded", groundFlag);
        }


        if (groundFlag)  {
            if (Input.GetButtonDown("Jump"))
                _rigidbody.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
        
        } 
        
    }

    void isCrouched() {
        if (Input.GetButtonDown("Crouch") || Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, whatIsGround)) {
            crouchFlag = true;

        } else if (Input.GetButtonUp("Crouch")) {
            crouchFlag = false;
        }
    }

    void isGrounded() {
        if (Physics2D.OverlapCircle(groundCheck.position, ceilingRadius, whatIsGround)) {
            groundFlag = true;
        } else {
            groundFlag = false;
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
