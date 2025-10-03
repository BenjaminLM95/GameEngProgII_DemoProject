using UnityEngine;
using UnityEngine.InputSystem; 

public class GameState_Gameplay : IState
{
    GameManager gameManager => GameManager.Instance;
    GameStateManager gameStateManager => GameManager.Instance.GameStateManager;
    PlayerController playerController => GameManager.Instance.PlayerController;
    UIManager uiManager => GameManager.Instance.UIManager;
   
    

    #region Singleton Instance

    // A single, readonly instance of the atate class is created.
    // The 'readonly' keyword ensures this instance cannot be modified after initialization.
    private static readonly GameState_Gameplay instance = new GameState_Gameplay();

    // Provides global access to the singleton instance of this state.
    // Uses an expression-bodied property to return the static _instance variable.
    public static GameState_Gameplay Instance = instance;

    #endregion

    public void EnterState() 
    {
        Debug.Log("Entered Gameplay state");
        uiManager.EnableGameplay(); 
    }

    public void FixedUpdateState() 
    {
        Debug.Log("Running Gameplay FixedUpdate State"); 
    }

    public void UpdateState() 
    {
        Debug.Log("Running Gameplay Update State");

        playerController.HandlePlayerMovement(); 

        if (Keyboard.current[Key.M].wasPressedThisFrame) 
        {
            gameStateManager.SwitchToState(GameState_MainMenu.Instance);
            Cursor.visible = true;
        }

        if (Keyboard.current[Key.Escape].wasPressedThisFrame)
        {
            gameStateManager.Pause();
            Cursor.visible = true;
            
        }

        if (Keyboard.current[Key.K].wasPressedThisFrame) 
        {
            gameStateManager.GameOver(); 
        }

    }

    public void LateUpdateState() 
    {
        playerController.HandlePlayerLook();

        Debug.Log("Running Gameplay LateUpdate State");
    }

    public void ExitState() 
    {
        Debug.Log("Exiting Gameplay State"); 
    }

    
}
