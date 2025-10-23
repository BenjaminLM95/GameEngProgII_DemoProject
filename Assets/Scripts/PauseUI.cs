using UnityEngine;
using UnityEngine.UIElements;

public class PauseUI : MonoBehaviour
{
    private UIDocument mainMenuUIdoc => GetComponent<UIDocument>();

    GameManager gameManager => GameManager.Instance;

    UIManager UIManager => GameManager.Instance.UIManager;

    LevelManager levelManager => GameManager.Instance.LevelManager;

    InputManager inputManager => GameManager.Instance.InputManager;

    GameStateManager gameStateManager => GameManager.Instance.GameStateManager;

    Button resumeButton;
    Button mainMenuButton;

    private void OnEnable()
    {
        resumeButton = mainMenuUIdoc.rootVisualElement.Q<Button>("ResumeBtn");
        mainMenuButton = mainMenuUIdoc.rootVisualElement.Q<Button>("MainMenuBtn");
        

        resumeButton.clicked += OnResumeButtonClicked;
        mainMenuButton.clicked += OnMainMenuButtonClicked;
        
    }

    void OnResumeButtonClicked() 
    {
        gameStateManager.Resume();
    }

    void OnMainMenuButtonClicked() 
    {
        gameStateManager.MainMenu();    
    }
}
