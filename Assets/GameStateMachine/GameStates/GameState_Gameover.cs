using UnityEngine;
using UnityEngine.InputSystem;

public class GameState_Gameover : IState
{
    GameManager gameManager => GameManager.Instance;
    GameStateManager gameStateManager => GameManager.Instance.GameStateManager;
    UIManager uiManager => GameManager.Instance.UIManager;


    #region Singleton Instance

    // A single, readonly instance of the atate class is created.
    // The 'readonly' keyword ensures this instance cannot be modified after initialization.
    private static readonly GameState_Gameover instance = new GameState_Gameover();

    // Provides global access to the singleton instance of this state.
    // Uses an expression-bodied property to return the static _instance variable.
    public static GameState_Gameover Instance = instance;

    #endregion

    public void EnterState()
    {
        //Debug.Log("Entered Game Over state");
        //uiManager.EnableGameover();

    }

    public void FixedUpdateState()
    {
        //Debug.Log("Running Game Over FixedUpdate State");
    }

    public void UpdateState()
    {
        Debug.Log("Running Game Over Update State");
        if (Keyboard.current[Key.G].wasPressedThisFrame)
        {
            gameStateManager.SwitchToState(GameState_Gameplay.Instance);
            Cursor.visible = false;
        }

    }

    public void LateUpdateState()
    {
        //Debug.Log("Running Game Over LateUpdate State");
    }

    public void ExitState()
    {
        //Debug.Log("Exiting Game Over State");
    }
}
