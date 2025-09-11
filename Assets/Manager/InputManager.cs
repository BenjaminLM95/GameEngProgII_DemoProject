using UnityEngine;
using UnityEngine.InputSystem;
using System; 


public class InputManager : MonoBehaviour, Inputs.IPlayerActions
{
    private Inputs inputs; 


    void Awake() 
    {
        try
        {
            inputs = new Inputs();
            inputs.Player.SetCallbacks(this);
            inputs.Player.Enable();
        }
        catch(Exception exception)
        {
            Debug.LogError("Error " + exception.Message);
        }
    }

    #region Inputs Events

    // Event that are triggered when input activity is detected

    public event Action<Vector2> MoveInputEvent;
    public event Action<Vector2> LookInputEvent;

    public event Action JumpStartedInputEvent;
    public event Action JumpPerformedInputEvent;
    public event Action JumpCanceledInputEvent; 


    #endregion

    #region Input Callbacks

    // 


    #endregion

    public void OnMove(InputAction.CallbackContext context) 
    {
        MoveInputEvent?.Invoke(context.ReadValue<Vector2>()); 
    }

    public void OnLook(InputAction.CallbackContext context) 
    {
        LookInputEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context) 
    {
        if (context.started) 
        {
            JumpStartedInputEvent?.Invoke(); 
        }
        else if (context.performed) 
        {
            JumpPerformedInputEvent?.Invoke();
        }
        else if (context.canceled) 
        {
            JumpCanceledInputEvent?.Invoke();
        }
    }


    void OnEnable() 
    {
        if (inputs != null) 
        {
            inputs.Player.Enable();
        }
    }

    void Destroy() 
    {
        if(inputs != null) 
        {
            inputs.Player.Disable();
        }
    }

}
