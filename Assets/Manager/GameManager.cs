using UnityEngine;


[DefaultExecutionOrder(-100)]


public class GameManager : MonoBehaviour
{
    //public InputManager inputManager;


    public static GameManager Instance { get; private set; }

    [Header("Manager References (Auto-Assigned")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameStateManager gameStateManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private LevelManager levelManager; 

    // public read-only accessors for other scripts to use the manager
    public InputManager InputManager => inputManager;
    public GameStateManager GameStateManager => gameStateManager;
    public PlayerController PlayerController => playerController; 
    public UIManager UIManager => uiManager;
    public LevelManager LevelManager => levelManager;


    private void Awake() 
    {
        #region Singleton

        if(Instance == null) 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject); 
        }

        #endregion

        //Auto-assign manager reference from child object if 
        
        inputManager ??= GetComponentInChildren<InputManager>();
        gameStateManager ??= GetComponentInChildren<GameStateManager>();
        playerController ??= GetComponentInChildren<PlayerController>();
        uiManager ??= GetComponentInChildren<UIManager>();
        levelManager ??= GetComponentInChildren<LevelManager>();
    }

    

}
