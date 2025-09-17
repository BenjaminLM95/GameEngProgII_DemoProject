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

    public event Action<InputAction.CallbackContext> JumpInputEvent;
    public event Action<InputAction.CallbackContext> SprintInputEvent;
    public event Action<InputAction.CallbackContext> CrouchInputEvent; 
    
    


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
        JumpInputEvent?.Invoke(context); 

    }

    public void OnCrouch(InputAction.CallbackContext context) 
    {
        CrouchInputEvent?.Invoke(context); 
    }

    public void OnSprint(InputAction.CallbackContext context) 
    {
        SprintInputEvent?.Invoke(context); 
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
