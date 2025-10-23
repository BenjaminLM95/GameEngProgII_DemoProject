using UnityEngine;
using UnityEngine.InputSystem;

public class GameState_MainMenu : IState
{
    GameManager gameManager => GameManager.Instance;
    GameStateManager gameStateManager => GameManager.Instance.GameStateManager;
    UIManager uiManager => GameManager.Instance.UIManager;

    #region Singleton Instance

    // A single, readonly instance of the atate class is created.
    // The 'readonly' keyword ensures this instance cannot be modified after initialization.
    private static readonly GameState_MainMenu instance = new GameState_MainMenu();

    // Provides global access to the singleton instance of this state.
    // Uses an expression-bodied property to return the static _instance variable.
    public static GameState_MainMenu Instance = instance; 

    #endregion


    public void EnterState() 
    {
        //Debug.Log("Entered Main Menu state");
        uiManager.EnableMainMenu(); 
    }

    public void FixedUpdateState() 
    {
        //Debug.Log("Running Main Menu FixedUpdate State");
    }

    public void UpdateState() 
    {
       // Debug.Log("Running Main Menu Update State");

        if (Keyboard.current[Key.G].wasPressedThisFrame)
        {
            gameStateManager.SwitchToState(GameState_Gameplay.Instance);
            Cursor.visible = false;
        }
    }

    public void LateUpdateState() 
    {
        //Debug.Log("Running Main Menu LateUpdate State");
    }

    public void ExitState() 
    {
        //Debug.Log("Exiting Main Menu State");
    }
}
