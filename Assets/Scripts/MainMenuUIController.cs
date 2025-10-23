using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuUIController : MonoBehaviour
{
    private UIDocument mainMenuUIdoc => GetComponent<UIDocument>();

    GameManager gameManager => GameManager.Instance; 

    UIManager UIManager => GameManager.Instance.UIManager;

    LevelManager levelManager => GameManager.Instance.LevelManager;

    InputManager inputManager => GameManager.Instance.InputManager;

    GameStateManager gameStateManager => GameManager.Instance.GameStateManager;

    Button playButton;
    Button optionButton;
    Button quitButton;

    private Button[] menuButtons;
    private int focusedIndex = 0;
       

    private void OnEnable()
    {
        playButton = mainMenuUIdoc.rootVisualElement.Q<Button>("PlayButton");
        optionButton = mainMenuUIdoc.rootVisualElement.Q<Button>("OptionButton");
        quitButton = mainMenuUIdoc.rootVisualElement.Q<Button>("QuitButton");

        playButton.clicked += OnPlayButtonClicked;
        optionButton.clicked += OnPauseButtonClicked;
        quitButton.clicked += OnQuitButtonClicked;
    }

    public void OnPlayButtonClicked() 
    {
        Debug.Log("Play");
        gameStateManager.Play();
    }

    public void OnPauseButtonClicked() 
    {
        Debug.Log("Option");
    }

    public void OnQuitButtonClicked() 
    {
        gameStateManager.Quit();
    }
}
