using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    // Manager References

    private InputManager inputManager => GameManager.Instance.inputManager;
    private CharacterController characterController => GetComponent<CharacterController>();

    [SerializeField] private Transform cameraRoot;
    public Transform CameraRoot => cameraRoot;

    [Header("Enable/Disable Controls & Features")]
    public bool moveEnabled = true;
    public bool lookEnabled = true;
    [SerializeField] private bool jumpEnabled = true;
    [SerializeField] private bool sprintEnabled = true;
    [SerializeField] private bool crouchEnabled = true; 
    
    // Input Variables

    public Vector2 moveInput;
    public Vector2 lookInput;


    // Other variables
    [Header("Move Setting")]
    [SerializeField] private float movSpeed;
    [SerializeField] private float crunchMoveSpeed;
    [SerializeField] private float sprintMoveSpeed;
    [SerializeField] private float speedTransitionDuration;
    [SerializeField] private float currentMoveSpeed;
    private bool sprintInput = false; // Is the player holding the sprint input? 
    [SerializeField] private bool crouchInput = false;
    private Vector3 velocity; // Used for vertical movement (jumping/gravity)?

    [Header("Look Setting")]    
    float horizontalLookSensitivity = 30;
    float verticalLookSensitivity = 30;
    public float LowerLookLimit = -60;
    public float UpperLookLimit = 60;
    public bool invertLookY { get; private set; }

    [Header("Jump & Gravity Settings")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private float gravity = 30.0f;
    [SerializeField] private float jumpHeight = 2.0f;
    private float jumpCooldown = 0.2f; // Time before allowing abother jump
    private float jumpCooldownTimer = 0f;
    private float groundCheckRadius = 0.1f;
    private bool jumpRequested = false;

    [Header("Crouch Settings")]
    private float standingHeight;
    private Vector3 standingCenter;
    private float standingCamY;
    private bool isObstructed = false;

    [SerializeField] private float crouchTransitionDuration = 0.2f; // Time in seconds for crouch/stand transition (approximate completion)
    [SerializeField] private float crouchingHeight = 1.0f;
    [SerializeField] private Vector3 crouchingCenter = new Vector3(0, 0.5f, 0);
    [SerializeField] private float crouchingCamY = 0.75f;

    private float targetHeight;
    private Vector3 targetCenter;
    private float targetCamY; // Target Y position for camera root during crouch transition

        public Transform spawnPosition;
       

    private void Awake() 
    {
        movSpeed = 5f;
        // Initialize crouch variables
        standingHeight = characterController.height;
        standingCenter = characterController.center;
        standingCamY = cameraRoot.localPosition.y;

        targetHeight = standingHeight;
        targetCenter = standingCenter;
        targetCamY = cameraRoot.localPosition.y;
    }

    private void Update()
    {
        HandlePlayerMovement();
    }

    private void LateUpdate()
    {
        HandlePlayerLook(); 
    }

    public void HandlePlayerMovement() 
    {
        if (!moveEnabled) return;

        GroundedCheck();
        HandleCrouchTransition(); 
        // Step 1: Get input direction
        Vector3 moveInputDirection = new Vector3(moveInput.x, 0, moveInput.y);
        Vector3 worldMoveDirection = transform.TransformDirection(moveInputDirection);

        // Step 2: Determine movement speed
        float targetSpeed;

        if (sprintInput)
            targetSpeed = sprintMoveSpeed;
        else
            targetSpeed = movSpeed;

        // Step 3: Smoothly interpolate current speed towards targer speed
        float lerpSpeed = 1f - Mathf.Pow(0.01f, Time.deltaTime / speedTransitionDuration);
        currentMoveSpeed = Mathf.Lerp(currentMoveSpeed, targetSpeed, lerpSpeed); 

        // Step 4: Handle horizontal movement
        Vector3 horizontalMovement = worldMoveDirection * currentMoveSpeed;

        // Step 5: Handle jumping and gravity

        ApplyJumpAndGravity();

        // Step 6: Combine horizontal and vertical movement

        Vector3 movement = horizontalMovement;
        movement.y = velocity.y; 

        // Step 7: Apply final movement

        characterController.Move(movement * Time.deltaTime); 


    }

    private void ApplyJumpAndGravity() 
    {
        if (jumpEnabled) 
        {
            if (jumpRequested) 
            {
                velocity.y = Mathf.Sqrt(2f * jumpHeight * gravity);
                jumpRequested = false;

                jumpCooldownTimer = jumpCooldown;
            }
        }

        // Apply gravity based on the player's current state (grounded or in air).
        if (isGrounded && velocity.y < 0)
        {
            // If grounded and moving downwards (due to accumulated gravity from previous frames),
            // snap velocity to a small negative value. This keeps the character firmly on the ground
            // without allowing gravity to build up indefinitely, preventing "bouncing" or
            // incorrect ground detection issues.

            velocity.y = -1f;
        }
        else  // If not grounded (in the air):
        {
            // apply standard gravity
            velocity.y -= gravity * Time.deltaTime;
        }
                

        if (jumpCooldownTimer > 0)
        {
            jumpCooldownTimer -= Time.deltaTime;
        }
    }

    private void HandleCrouchTransition()
    {
        bool shouldCrouch = crouchInput == true;

        // if airborne and was crouching, maintain crouch state (prevents standing up from crouch while walking off a ledge)
        bool wasAlreadyCrouching = characterController.height < (standingHeight - 0.05f);

        if (isGrounded == false && wasAlreadyCrouching)
        {
            shouldCrouch = true; // Maintain crouch state if airborne (walking off ledge while crouching)
        }

        if (shouldCrouch)
        {
            targetHeight = crouchingHeight;
            targetCenter = crouchingCenter;
            targetCamY = crouchingCamY;
            isObstructed = false; // No obstruction when intentionally crouching
        }
        else
        {
            // float maxAllowedHeight = GetMaxAllowedHeight();

            float maxAllowedHeight = 3.0f;

            if (maxAllowedHeight >= standingHeight - 0.05f)
            {
                // No obstruction, allow immediate transition to standing
                targetHeight = standingHeight;
                targetCenter = standingCenter;
                targetCamY = standingCamY;
                isObstructed = false;
            }

            else
            {
                // Obstruction detected, limit height and center
                targetHeight = Mathf.Min(standingHeight, maxAllowedHeight);
                float standRatio = Mathf.Clamp01((targetHeight - crouchingHeight) / (standingHeight - crouchingHeight));
                targetCenter = Vector3.Lerp(crouchingCenter, standingCenter, standRatio);
                targetCamY = Mathf.Lerp(crouchingCamY, standingCamY, standRatio);
                isObstructed = true;
            }
        }

        // Calculate lerp speed based on desired duration
        // This formula ensures the transition approximately reaches 99% of the target in 'crouchTransitionDuration' seconds.
        float lerpSpeed = 1f - Mathf.Pow(0.01f, Time.deltaTime / crouchTransitionDuration);

        // Smoothly transition to targets
        characterController.height = Mathf.Lerp(characterController.height, targetHeight, lerpSpeed);
        characterController.center = Vector3.Lerp(characterController.center, targetCenter, lerpSpeed);

        Vector3 currentCamPos = cameraRoot.localPosition;
        cameraRoot.localPosition = new Vector3(currentCamPos.x, Mathf.Lerp(currentCamPos.y, targetCamY, lerpSpeed), currentCamPos.z);

    }

    public void HandlePlayerLook() 
    {
        if (!lookEnabled) return; 

        float lookX = lookInput.x * horizontalLookSensitivity * Time.deltaTime;
        float lookY = lookInput.y * verticalLookSensitivity * Time.deltaTime;

        if (invertLookY) 
        {
            lookY = -lookY; 
        }

        //Rotate character on Y-axis (left/right look)
        transform.Rotate(Vector3.up * lookX);

        // Tilt cameraRoot on X-axis (up/down look)
        Vector3 currentAngles = cameraRoot.localEulerAngles;
        float newRotationX = currentAngles.x - lookY;

        //Convert to signed angle for proper clamping
        newRotationX = (newRotationX > 180) ? newRotationX - 360 : newRotationX;
        newRotationX = Mathf.Clamp(newRotationX, LowerLookLimit, UpperLookLimit); 

        CameraRoot.localEulerAngles = new Vector3(newRotationX, 0, 0);


    }


    void SetMoveInput(Vector2 inputVector) 
    {
        moveInput = new Vector2(inputVector.x, inputVector.y);
    }

    void SetLookInput(Vector2 inputVector) 
    {
        lookInput = new Vector2(inputVector.x, inputVector.y); 
    }

    void HandleJump(InputAction.CallbackContext context) 
    {
        
        if (context.started)
        {
            if (jumpEnabled && jumpCooldownTimer <= 0f && isGrounded)
            {
                jumpRequested = true;
                jumpCooldownTimer = 0.1f; 
            }

        }


        if (context.performed)
           Debug.Log("Jump performed");
                      

    }

    private void GroundedCheck() 
    {
        isGrounded = characterController.isGrounded;
    }


    public void MovePlayerToSpawnPosition(Transform _spawnPosition) 
    {
        // Moving player to spawn position        
        // turn off character controller
        characterController.enabled = false; 

        transform.position = spawnPosition.position;
        transform.rotation = spawnPosition.rotation;

        //turn on character controller
        characterController.enabled = true; 
    }


    void HandleCrouch(InputAction.CallbackContext context) 
    {
        if (!crouchEnabled) return;

        if (context.started) 
        {
            crouchInput = !crouchInput;
        }

        Debug.Log("Crouch");
    }

    void HandleSprint(InputAction.CallbackContext context) 
    {
        Debug.Log("Sprint");
        // if sprint is not enabled, do nothing and just return
        if (!sprintEnabled) return;

        if (context.started)
            sprintInput = true;

        if (context.canceled)
            sprintInput = false; 
    }


    void OnEnable() 
    {
        inputManager.MoveInputEvent += SetMoveInput;
        inputManager.LookInputEvent += SetLookInput;

        inputManager.JumpInputEvent += HandleJump;
        inputManager.CrouchInputEvent += HandleCrouch;
        inputManager.SprintInputEvent += HandleSprint;
        
    }

    void OnDisable() 
    {
        inputManager.MoveInputEvent -= SetMoveInput;
        inputManager.LookInputEvent -= SetLookInput;

        inputManager.JumpInputEvent -= HandleJump;
        inputManager.CrouchInputEvent -= HandleCrouch;
        inputManager.SprintInputEvent -= HandleSprint; 
    }
}
