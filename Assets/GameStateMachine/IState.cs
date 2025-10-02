//Defines the methods required for implementing a game state machine
// Each game state should implement this intereface to ensure consistent behavior

public interface IState 
{
    // When the game state is entered. This method should handle initialization logic specific to the state
    void EnterState();

    void FixedUpdateState();

    void UpdateState();

    void LateUpdateState();

    void ExitState(); 
}
