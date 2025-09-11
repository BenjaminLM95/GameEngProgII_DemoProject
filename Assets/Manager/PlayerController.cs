using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Manager References

    private InputManager inputManager;

    // Input Variables

    public Vector2 moveInput;
    public Vector2 lookInput;


    private void Awake() 
    {
        inputManager = GameManager.Instance.inputManager; 
    }
       

    void SetMoveInput(Vector2 inputVector) 
    {
        moveInput = new Vector2(inputVector.x, inputVector.y);
    }

    void SetLookInput(Vector2 inputVector) 
    {
        lookInput = new Vector2(inputVector.x, inputVector.y); 
    }

    void JumpStartedInput() 
    {
        Debug.Log("Jump Started");
    }

    void JumpPerformedInput() 
    {
        Debug.Log("Jump Performed");
    }

    void JumpCanceledInput() 
    {
        Debug.Log("Jump Canceled"); 
    }


    void OnEnable() 
    {
        inputManager.MoveInputEvent += SetMoveInput;
        inputManager.LookInputEvent += SetLookInput;

        inputManager.JumpStartedInputEvent += JumpStartedInput;
        inputManager.JumpPerformedInputEvent += JumpPerformedInput;
        inputManager.JumpCanceledInputEvent += JumpCanceledInput;
    }

    void OnDisable() 
    {
        inputManager.MoveInputEvent -= SetMoveInput;
        inputManager.LookInputEvent -= SetLookInput;

        inputManager.JumpStartedInputEvent -= JumpStartedInput;
        inputManager.JumpPerformedInputEvent -= JumpPerformedInput;
        inputManager.JumpCanceledInputEvent -= JumpCanceledInput;
    }
}
