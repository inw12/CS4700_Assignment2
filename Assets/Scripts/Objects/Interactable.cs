// *************************************************************************
// • Raises signal when player is in proximity to an "interactable" object
// *************************************************************************

using UnityEngine;

public class Interactable : MonoBehaviour {

    public Signal context;
    public bool playerInRange;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            context.Raise();
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            context.Raise();
            playerInRange = false;
        }
    }
}