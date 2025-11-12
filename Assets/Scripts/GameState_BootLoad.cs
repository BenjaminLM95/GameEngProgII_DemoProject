using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState_BootLoad : IState
{
    GameManager gameManager => GameManager.Instance;
    GameStateManager gameStateManager => GameManager.Instance.GameStateManager;
    UIManager uiManager => GameManager.Instance.UIManager;


    #region Singleton Instance

    // A single, readonly instance of the atate class is created.
    // The 'readonly' keyword ensures this instance cannot be modified after initialization.
    private static readonly GameState_BootLoad instance = new GameState_BootLoad();

    // Provides global access to the singleton instance of this state.
    // Uses an expression-bodied property to return the static _instance variable.
    public static GameState_BootLoad Instance = instance;

    #endregion


    public void EnterState()
    {
        //Debug.Log("Entered Boat Load state");
        Cursor.visible = false;

        Time.timeScale = 1f;

        // if BootLoader is the only active scene, redirect to MainMenu
        if(SceneManager.sceneCount == 1 && SceneManager.GetActiveScene().name == "BootLoader") 
        {

            GameManager.Instance.LevelManager.GoToMainMenu();
            gameManager.GameStateManager.SwitchToState(GameState_MainMenu.Instance);
            return;
        }
        else if(SceneManager.sceneCount > 1 && SceneManager.GetActiveScene().name == "MainMenu") 
        {
            gameManager.GameStateManager.SwitchToState(GameState_MainMenu.Instance);
            return;
        }
        // If all the above fails, go to the level scene
        else 
        {
            Debug.Log("Bootloading player!");
            GameManager.Instance.LevelManager.BootloadPlayer();
            gameManager.GameStateManager.SwitchToState(GameState_Gameplay.Instance);
            return;
        }
        
        
    }

    public void FixedUpdateState()
    {
        //Debug.Log("Running BootLoad FixedUpdate State");
    }

    public void UpdateState()
    {
        // Debug.Log("Running BootLoad Update State");

        
    }

    public void LateUpdateState()
    {
        //Debug.Log("Running BootLoad LateUpdate State");
    }

    public void ExitState()
    {
        //Debug.Log("Exiting BootLoad State");
    }
}
