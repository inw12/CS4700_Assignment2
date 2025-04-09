using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Breakable")) {
            other.GetComponent<Pot>().destroy();
        }
    }
}
