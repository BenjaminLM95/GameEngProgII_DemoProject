using UnityEngine;

public class GizmosSpawnPoint : MonoBehaviour
{
   private void OnDrawGizmos() 
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.position, 1f);
    }
}
