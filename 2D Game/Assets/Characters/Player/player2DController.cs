using UnityEngine;

public class player2DController : MonoBehaviour
{

    [Range(0,5)] [SerializeField] public float speed = 1f;
    [Range(0,50)]  [SerializeField] public int jumpPower = 10;
    [SerializeField] public LayerMask whatIsGround;
    public Collider2D standing;
    public Transform ceilingCheck;
    public Transform groundCheck;
    public Transform RWallCheck;
    public Transform LWallCheck;


    private RaycastHit2D Hit2D;
    private float ceilingRadius = 0.2f;
    private bool crouchFlag;
    private bool ceilingFlag;
    private bool groundFlag;
    private bool hangFlag;
    private float movement;
    private Vector3 m_Velocity = Vector3.zero;
    private Rigidbody2D _rigidbody;
    private Animator animator;
    private float canJump;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        crouchFlag = false;
        groundFlag = true;
        ceilingFlag = false;
        hangFlag = false;
    }

    // Update is called once per frame
    private void Update()
    {
        // flags
        ceilingAbove();
        isCrouched();
        isGrounded();
        isHanging();

        

        // movement speed
        if (crouchFlag) {
            movement = Input.GetAxisRaw("Horizontal") * (speed/3);
        } else if (hangFlag) {
            movement = Input.GetAxisRaw("Horizontal") * (speed/5);
        } else {
            movement = Input.GetAxisRaw("Horizontal") * speed;
        }
        movement *= Time.fixedDeltaTime;

        // disabling Stand collider if crouch detected
        standing.enabled = !animator.GetBool("crouched");

        // updating animations
        animator.SetFloat("speed", Mathf.Abs(movement));
        animator.SetFloat("y", _rigidbody.velocity.y);
        if (crouchFlag != animator.GetBool("crouched")) {
            animator.SetBool("crouched", crouchFlag);
        }
        if (groundFlag != animator.GetBool("grounded")) {
            animator.SetBool("grounded", groundFlag);
        }
        if (ceilingFlag != animator.GetBool("ceilingAbove")) {
            animator.SetBool("ceilingAbove", ceilingFlag);
        }
        if (hangFlag != animator.GetBool("hanging")) {
            animator.SetBool("hanging", hangFlag);
        }

        // checking & implementing jump
        if ((groundFlag || hangFlag) && Input.GetButtonDown("Jump"))  {
            tryJump();
        
        } 
        
    }

    void ceilingAbove() {
        if (Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, whatIsGround)) {
            ceilingFlag = true;
        } else {
            ceilingFlag = false;
        }
    }

    void isCrouched() {
        if (Input.GetButton("Crouch") || ceilingFlag) {
            crouchFlag = true;

        } else {
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

    void isHanging() {
        if (Physics2D.OverlapCircle(RWallCheck.position, 0.05f, whatIsGround) ||
            Physics2D.OverlapCircle(LWallCheck.position, 0.05f, whatIsGround) ) {
            hangFlag = true;
        } else {
            hangFlag = false;
        }
    }

    void tryJump() {
        if (Time.time > canJump) {
                _rigidbody.AddForce(new Vector2(0, (hangFlag ? jumpPower + 3 : jumpPower)), ForceMode2D.Impulse);
                canJump = Time.time + 0.6f;
        }
    }

    void FixedUpdate () 
    {
        Vector3 targetVelocity = new Vector2(movement, _rigidbody.velocity.y);
        _rigidbody.velocity = Vector3.SmoothDamp(_rigidbody.velocity, targetVelocity, ref m_Velocity, .1f);

        if (hangFlag && _rigidbody.velocity.y < 0) {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y/2);
        } 

        if (!Mathf.Approximately(0, movement)) 
            transform.rotation = movement > 0 ? Quaternion.identity : Quaternion.Euler(0, 180, 0);
    }

}
