using UnityEngine;

public class GameState_Loading : IState
{
    GameManager gameManager => GameManager.Instance;
    GameStateManager gameStateManager => GameManager.Instance.GameStateManager;
    UIManager uiManager => GameManager.Instance.UIManager;


    #region Singleton Instance

    // A single, readonly instance of the atate class is created.
    // The 'readonly' keyword ensures this instance cannot be modified after initialization.
    private static readonly GameState_Loading instance = new GameState_Loading();

    // Provides global access to the singleton instance of this state.
    // Uses an expression-bodied property to return the static _instance variable.
    public static GameState_Loading Instance = instance;

    #endregion



    public void EnterState()
    {


    }

    public void FixedUpdateState()
    {
        //Debug.Log("Running Loading FixedUpdate State");
    }

    public void UpdateState()
    {
        // Debug.Log("Running Loading Update State");


    }

    public void LateUpdateState()
    {
        //Debug.Log("Running Loading LateUpdate State");
    }

    public void ExitState()
    {
        //Debug.Log("Exiting Loading State");
    }
}
