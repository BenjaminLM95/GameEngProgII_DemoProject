using UnityEngine;

public class GameoverTrigger : MonoBehaviour
{

    public GameStateManager gameStateManager; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Contact: " + other.name);
            gameStateManager.GameOver();
        }
        
    }
}
