using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{

    InputSystem_Actions inputActions;
    Rigidbody2D rb;

    public float jumpForce = 5f; // Adjusts current jump force
    [SerializeField] private float jumpCutMultiplier = 0.5f; //Multiplier to reduce jump height when jump is released early 

    [SerializeField] private float fallMultiplier = 2.5f; // Multiplier to increase fall speed for better jump feel
    [SerializeField] private float lowJumpMultiplier = 2f; //Multiplier to increase fall speed when jump is released early for better jump feel
    [SerializeField] private float maxFallSpeed = -10f; // Maximum fall speed to prevent excessive falling

    bool isJumpPressed = false;
    bool isJumpReleased = false;
    bool isJumpHeld = false;

    [SerializeField] private LayerMask groundLayer; // Layer to check for ground
    [SerializeField] private Transform groundCheck; //Empty GameObject Position at the player's feet
    [SerializeField] private float groundCheckRadius = 0.2f; // Radius for ground check

    [SerializeField] private float coyoteTime = 0.2f; // Time after leaving the ground during which a jump can still be initiated
    [SerializeField] private float coyoteTimeCounter; 
    bool isJumping = false;

    [SerializeField] private float jumpBuffertime = 0.2f; // Time Before Landing during which a jump input can be buffered
    [SerializeField] private float jumpBufferCounter; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        inputActions = new InputSystem_Actions();
        if (!TryGetComponent(out rb)){
            Debug.LogError("rigidbody2D component is missing player object.Disabling PlayerJump scriptm ");
        }
        
    }
     void OnEnable()
    {
        inputActions.Player.Jump.Enable();
        inputActions.Player.Jump.performed += OnJumpPerformed;
        inputActions.Player.Jump.canceled += OnJumpCanceled;
    }
   
    void OnDisable( )
    {
        inputActions.Player.Jump.performed -= OnJumpPerformed;
        inputActions.Player.Jump.canceled -= OnJumpCanceled;
        inputActions.Player.Jump.Disable();
    }
    private void Update()
    {
        bool isGrounded = IsGrounded();

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime; // Reset coyote time when grounded
            isJumping = false; // reset jump input when grounded
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime; // Decrease coyote time when grounded
        }
        if (jumpBufferCounter> 0f && coyoteTimeCounter > 0f && !isJumping)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x,jumpForce); //adjust jump force as needed
            isJumping = true; //Set Jump input as active
            coyoteTimeCounter = 0f; // Reset coyote time counter after jumping
            jumpBufferCounter = 0f; // Reset jump buhher counter after jumping
        }
        if (isJumpPressed)
        {
            jumpBufferCounter = jumpBuffertime; //Reset jump buffer counter when jump is pressed
            isJumpPressed = false; // Reset jump iput after buffering 
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime; // Decrease jump buffer counter when not pressed
        }
        if (isJumpReleased && rb.linearVelocity.y> 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * jumpCutMultiplier); //reduced upward velocity for variable jump height
            isJumpReleased = false; // reset jump release input
        }
        isJumpReleased =false; // Reset jump release input at the end of the frame to prevent unintended behaviour
    }

    private void FixedUpdate()
    {
        //Apply fall multiplier for better jump feel 
        if (rb.linearVelocity.y < 0f)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;   
        }
        else if (rb.linearVelocity.y > 0f  && isJumpHeld)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
        //clamp fall speed to prevent execessive falling
        if (rb.linearVelocity.y < maxFallSpeed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, maxFallSpeed);
        }
    }

    private void OnJumpPerformed (InputAction.CallbackContext context)
    {
        // Implement Jump logic here
        if (context.performed)
        {
            isJumpPressed = true;
            isJumpHeld = true;
        }
    }
    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        // Implement Jump logic here
        if (context.canceled)
        {
            isJumpReleased = true;
            isJumpHeld = false;
        }
    }

    private bool IsGrounded()
    {
        //check if the player is touching the ground using a circle overlap
        return Physics2D.OverlapCircle(groundCheck.position,groundCheckRadius,groundLayer);
    }

   
}
