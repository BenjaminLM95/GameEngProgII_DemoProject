using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameStateManager gameStateManager;
    [SerializeField] private UIManager uiManager; 

    public GameObject player;
    public PlayerController playerController;
    bool exit = false;
    private string spawnPointName;
    private bool isLoading; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerController = GameManager.Instance.PlayerController;
        gameStateManager = FindFirstObjectByType<GameStateManager>();
        uiManager = FindFirstObjectByType<UIManager>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToMainMenu() 
    {
        LoadScene("MainMenu"); 
    }

    public void StartGame() 
    {
        LoadSceneWithSpawnPoint("Level01", "SpawnPointA");


    }

    public void GoToLevel1() 
    {
        LoadScene("Level01");
    }

    public void GoToLevel2() 
    {
        LoadScene("Level02"); 
    }
    public void LoadScene(string sceneName) 
    {
        StartCoroutine(LoadSceneAsync(sceneName)); 
    }


    public void LoadSceneWithSpawnPoint(string sceneName, string spawnPoint)
    {
        spawnPointName = spawnPoint;

        SceneManager.sceneLoaded += onSceneLoaded;

        LoadScene(sceneName);
    }

    void onSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene: " + scene.name + "is loaded");

        int sceneIndex = scene.buildIndex; 

        if(sceneIndex > 1) 
        {
            gameStateManager.SwitchToState(GameState_Gameplay.Instance);
            SetPlayerToSpawn(spawnPointName);
        }
        else if(sceneIndex == 1) 
        {
            gameStateManager.SwitchToState(GameState_MainMenu.Instance);
            SetPlayerToSpawn("SpawnPointM"); 
        }
        else 
        {
            gameStateManager.SwitchToState(GameState_BootLoad.Instance); 
        }

            
    }

    public void BootloadPlayer()
    {
        playerController.MovePlayerToSpawnPosition();
    }


    public void SetPlayerToSpawn(string spawnPointName)
    {
        GameObject spawnPointObject = GameObject.Find(spawnPointName);
        if (spawnPointObject != null)
        {
            if (player != null)
            {
                // Set the player position to the spawn object
                Transform spawnPointTransform = spawnPointObject.transform;
                player.transform.position = spawnPointTransform.position;
                player.transform.eulerAngles = spawnPointTransform.eulerAngles;
            }
            else
            {
                Debug.LogError("Player not found in the scene!");
            }
        }
        else
        {
            Debug.LogError($"Spawn point with {spawnPointName} not found in the scene!");
        }
    }

    IEnumerator LoadSceneAsync(string sceneName) 
    {
        gameStateManager.SwitchToState(gameStateManager.gameState_Loading);

        Debug.Log("LoadSceneAsync started for scene name: " + sceneName);

        isLoading = true;

        // Wait one frame to ensure UI is properly initialized
        yield return null;

        SceneManager.sceneLoaded += onSceneLoaded;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Prevent scene activation until we're ready
        asyncLoad.allowSceneActivation = false;

        float artificialProgress = 0f;
        float minUpdateInterval = 0.005f; // Time between updates in seconds
        float maxUpdateInterval = 0.5f; // Time between updates in seconds
        float minProgressIncrement = 0.005f; // Minimum progress increase per update
        float maxProgressIncrement = 0.05f; // Maximum progress increase per update
        float progressCompletedDelayDuration = 1.0f; // Delay after reaching 100% before completing

        while (!asyncLoad.isDone)
        {
            // Progress goes from 0 to 0.9
            float realProgress = asyncLoad.progress;

            // Gradually increase artificial progress
            artificialProgress = Mathf.MoveTowards(
                artificialProgress,
                realProgress,
                Random.Range(minProgressIncrement, maxProgressIncrement)
            );

            if (realProgress >= 0.9f && artificialProgress >= 0.9f)
            {
                // Set progress to 100% before the hold
                artificialProgress = 1.0f;                
                uiManager.LoadingUIController.UpdateProgressBar(artificialProgress);

                Debug.Log("Loading completed, holding for display...");

                // Hold at 100% for desired duration
                yield return new WaitForSeconds(progressCompletedDelayDuration);

                Debug.Log("Hold complete, activating scene...");

                // Now allow the scene to activate
                asyncLoad.allowSceneActivation = true;
            }
            else
            {
                // Normalize progress to 0-1 range
                artificialProgress = Mathf.Clamp01(artificialProgress / 0.9f);
            }

            uiManager.LoadingUIController.UpdateProgressBar(artificialProgress);

            // Wait for the specified interval before next update
            yield return new WaitForSeconds(Random.Range(minUpdateInterval, maxUpdateInterval));
        }

        isLoading = false;

    }


    private void OnDisable()
    {
        SceneManager.sceneLoaded -= onSceneLoaded;
    }


}
