using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRig;
    private float speedX;
    private float playerSpeed = 2f;
    public static bool canMove = true;
    private float coyoteTimeCounter;

    private float jumpBufferCounter;

    private bool isJumping;
    public bool isGrounded;
    public bool gravityjump = false;
    private float jumpheight = 7.5f;
    private bool hasJumped = false;
    private bool wasGroundedLastFrame = false;
    private float impactSpeed = 0f;
    private bool isStunned = false;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius = 0.2f;
    [SerializeField] private float jumpBufferTime = 0.1f;
    [SerializeField] private float coyoteTime = 0.1f;
    [SerializeField] private float druidJumpHeight;
    [SerializeField] private float variableJumpMultiplier = 0.5f;
    [SerializeField] private LayerMask groundLayer;
    public static bool canjump = true;
    public Vector2 lastGroundPosition = Vector2.zero;
    private void Start()
    {
        playerRig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (canMove)
        {
            speedX = Input.GetAxisRaw("Horizontal");
            playerRig.linearVelocityX = speedX * playerSpeed;
        }
        else playerRig.linearVelocityX = 0;

        bool touchingGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, LayerMask.GetMask("Ground"));
        isGrounded = touchingGround;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (!isGrounded && playerRig.linearVelocityY > 0.1f)
        {
            jumpBufferCounter = 0f;
        }

        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f && !hasJumped)
        {
            Debug.Log("JUMP!");
            playerRig.linearVelocityY = jumpheight;
            isJumping = true;
            hasJumped = true;
            jumpBufferCounter = 0f;
        }

        // ---- VARIABLE JUMP HEIGHT ----

        if (Input.GetKeyUp(KeyCode.Space) && isJumping)
        {
            if (playerRig.linearVelocityY > 0f)
            {
                playerRig.linearVelocityY *= variableJumpMultiplier;
            }
            isJumping = false;
        }

        // ---- RESET ON LAND ----
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
            canjump = true;
            hasJumped = false;

            RaycastHit2D groundPosCheck = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Ground"));
            if (groundPosCheck) lastGroundPosition = transform.position;
        }
        else
        {
            Debug.Log("Reset JumpOnGround");
            coyoteTimeCounter -= Time.deltaTime;
            canjump = false;
        }

        wasGroundedLastFrame = isGrounded;

        // ---- FASTER JUMP FALL ----
       
            if (canjump == false)
            {
                if (gravityjump)
                {
                    playerRig.gravityScale += 2; //add gravityScale to gravity when falling so it feels less floaty when jumping
                    gravityjump = false;
                }
            }
            else
            {
                if (!gravityjump)
                {
                    gravityjump = true;
                    playerRig.gravityScale = 1f; //set back to normal gravity
                }
            }
        
    }
}