using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [Header("Debug (read only")]
    [SerializeField] private string lastActiveState;
    [SerializeField] private string currentActiveState;

    // Private variable to store state information
    private IState currentState;
    private IState lastState;  //(Kept private for encapsulation)

    // Instantiate GameStates
    public GameState_MainMenu gameState_MainMenu = GameState_MainMenu.Instance;
    public GameState_Gameplay gameState_Gameplay = GameState_Gameplay.Instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = gameState_Gameplay;
        currentState.EnterState(); 


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
}
