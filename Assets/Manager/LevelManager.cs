using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameStateManager gameStateManager;

    public GameObject player;
    public PlayerController playerController;
    bool exit = false;
    private string spawnPointName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerController = GameManager.Instance.PlayerController;
        gameStateManager = FindFirstObjectByType<GameStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToMainMenu() 
    {
        SceneManager.LoadScene("MainMenu"); 
    }

    public void StartGame() 
    {
        LoadSceneWithSpawnPoint("Level01", "SpawnPointA");


    }

    public void GoToLevel1() 
    {
        SceneManager.LoadScene("Level01");
    }

    public void GoToLevel2() 
    {
        SceneManager.LoadScene("Level02"); 
    }

    public void LoadSceneWithSpawnPoint(string sceneName, string spawnPoint)
    {
        spawnPointName = spawnPoint;

        SceneManager.sceneLoaded += onSceneLoaded;

        SceneManager.LoadScene(sceneName);
    }

    void onSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene: " + scene.name + "is loaded");
        SetPlayerToSpawn(spawnPointName);
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

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= onSceneLoaded;
    }


}
