using UnityEngine;

public class GameState_Gameplay : IState
{
    GameManager gameManager => gameManager.Instance;
    GameStateManager gameStateManager => gameManager.Instance.GameStateManager;
    PlayerController playerController => gameManager.Instance.PlayerController; 

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
    }

    public void FixedUpdateState() 
    {
        Debug.Log("Running Gameplay FixedUpdate State"); 
    }

    public void UpdateState() 
    {
        Debug.Log("Running Gameplay Update State");

        playerController.HandlePlayerMovement(); 

        if (Keyboard.current[Key.P].wasPressThisFrame) 
        {
            gameStateManager.SwitchToState(GameState_MainMenu.Instance); 
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
