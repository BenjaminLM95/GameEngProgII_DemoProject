using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public string sceneName;
    public string spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.LevelManager.LoadSceneWithSpawnPoint(sceneName, spawnPoint);
        }
    }
}
