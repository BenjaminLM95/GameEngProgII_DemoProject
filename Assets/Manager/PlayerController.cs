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
    private bool crouchInput = false; 

    [Header("Look Setting")]    
    float horizontalLookSensitivity = 30;
    float verticalLookSensitivity = 30;
    public float LowerLookLimit = -60;
    public float UpperLookLimit = 60; 
    

    public bool invertLookY { get; private set; }

    


    private void Awake() 
    {
        movSpeed = 5f;
        //InvertedCamera = false; 
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

        // Step 6: Combine horizontal and vertical movement

        Vector3 movement = horizontalMovement;

        // Step 7: Apply final movement

        characterController.Move(movement * Time.deltaTime); 


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
              

        if(context.started)
            Debug.Log("Jump Started");        
       

        if (context.performed)
            Debug.Log("Jump performed");

    }

    void HandleCrouch(InputAction.CallbackContext context) 
    {
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
