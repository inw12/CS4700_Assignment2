// *************************************************************************
// • Applies to ALL world entities that produce text when interacted with
// *************************************************************************

using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    public GameObject dialogueBox;
    public GameObject playerUI;
    public Text dialogueText;
    public string dialogue;
    public bool playerInRange;

    public virtual void Update()
    {
        if (Input.GetButtonDown("Interact") && playerInRange)
        {
            if (dialogueBox.activeInHierarchy)
            {
                dialogueBox.SetActive(false);   // disable dialogue box  
                playerUI.SetActive(true);       // re-enable Player UI
            }
            else
            {
                playerUI.SetActive(false);      // disable Player UI
                dialogueBox.SetActive(true);    // enable dialogue box
                dialogueText.text = dialogue;   // display dialogue text
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            dialogueBox.SetActive(false);
        }
    }
}
