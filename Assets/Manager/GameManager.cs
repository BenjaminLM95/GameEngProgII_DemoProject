using UnityEngine;


[DefaultExecutionOrder(-100)]


public class GameManager : MonoBehaviour
{
    public InputManager inputManager;


    public static GameManager Instance { get; private set; }

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

        inputManager = GetComponentInChildren<InputManager>();

    }

    

}
