using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [Header("Debug (read only")]
    [SerializeField] private string lastActiveState;
    [SerializeField] private string currentActiveState;

    // Private variable to store state information
    public IState CurrentState => currentState;
    private IState currentState;
    private IState lastState;  //(Kept private for encapsulation)

    // Instantiate GameStates
    public GameState_MainMenu gameState_MainMenu = GameState_MainMenu.Instance;
    public GameState_Gameplay gameState_Gameplay = GameState_Gameplay.Instance;
    public GameState_Pause gameState_Pause = GameState_Pause.Instance; 
    public GameState_Gameover gameState_Gameover = GameState_Gameover.Instance;
    public GameState_BootLoad gameState_BootLoad = GameState_BootLoad.Instance;
    public GameState_Loading gameState_Loading = GameState_Loading.Instance; 
     

    [SerializeField] private LevelManager levelManager; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = gameState_BootLoad;
        currentState.EnterState();
       levelManager = FindFirstObjectByType<LevelManager>();


    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();
    }

    void FixedUpdate() 
    {
        currentState.FixedUpdateState();
    }

    void LateUpdate() 
    {
        currentState.LateUpdateState();
    }

    public void SwitchToState(IState newState) 
    {
        lastState = currentState; // Store the current state as the last state
        lastActiveState = lastState.ToString(); //Update debug info in inspector

        currentState?.ExitState(); // Exit the current state

        currentState = newState; //Switch to the new state
        currentActiveState = currentState.ToString();
        currentState.EnterState();  //Update debug info in inspector
    }

    #region Button Call Methods

    public void Pause() 
    {
        if (currentState != gameState_Gameplay)
            return; 

        if(currentState == gameState_Gameplay) 
        {
            SwitchToState(gameState_Pause);
            Time.timeScale = 0f;
            return; 
        }
    }

    public void Resume() 
    {
        if (currentState != gameState_Pause)
            return;

        if (currentState == gameState_Pause)
        {
            SwitchToState(gameState_Gameplay);
            Time.timeScale = 1f;
            return;
        }
    }

    public void Play() 
    {
        SwitchToState(gameState_Gameplay);
        levelManager.StartGame(); 
    }

    public void Quit() 
    {
        Application.Quit(); 
    }

    public void MainMenu() 
    {
        SwitchToState(gameState_MainMenu);
        levelManager.GoToMainMenu(); 
    }

    public void GameOver() 
    {
        if (currentState != gameState_Gameplay)
            return;

        if(currentState == gameState_Gameplay) 
        { 
            SwitchToState(gameState_Gameover);
        }

        
    }

    #endregion

}
