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

    // public read-only accessors for other scripts to use the manager
    public InputManager InputManager => inputManager;
    public GameStateManager GameStateManager => gameStateManager;
    public playerController => playerController; 


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
        playerController ??= GetComponentInChildren<playerController>(); 
    }

    

}
