using UnityEngine;
using UnityEngine.InputSystem;

public class GameState_Pause : IState
{

    GameManager gameManager => GameManager.Instance;
    GameStateManager gameStateManager => GameManager.Instance.GameStateManager;
    UIManager uiManager => GameManager.Instance.UIManager;

    #region Singleton Instance

    // A single, readonly instance of the atate class is created.
    // The 'readonly' keyword ensures this instance cannot be modified after initialization.
    private static readonly GameState_Pause instance = new GameState_Pause();

    // Provides global access to the singleton instance of this state.
    // Uses an expression-bodied property to return the static _instance variable.
    public static GameState_Pause Instance = instance;

    #endregion

    public void EnterState()
    {
        //Debug.Log("Entered Pause state");
        uiManager.EnablePause(); 
    }

    public void FixedUpdateState()
    {
        //Debug.Log("Running Pause FixedUpdate State");
    }

    public void UpdateState()
    {
        //Debug.Log("Running Pause Update State");

        if (Keyboard.current[Key.Escape].wasPressedThisFrame)
        {
            gameStateManager.Resume(); 
            Time.timeScale = 1f;
            Cursor.visible = false;
        }
    }

    public void LateUpdateState()
    {
        //Debug.Log("Running Pause LateUpdate State");
    }

    public void ExitState()
    {
        //Debug.Log("Exiting Pause State");
    }
}
