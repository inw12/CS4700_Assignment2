// **********************
// • Primary quest NPC
// **********************

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum QuestStatus
{
    available, inProgress, complete
}

public class StaticNPC : MonoBehaviour
{
    // Universal variables for interactable objects w/ text
    public bool playerInRange;
    public string dialogue;
    public string altDialogue;
    public GameObject dialogueBox;
    public GameObject playerUI;
    public GameObject commandToolTip;
    public Text dialogueText;

    // NPC-specific variables for quests
    private QuestStatus questStatus;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private InventoryItem questItem;

    // Notification Bubbles
    [SerializeField] private GameObject questAvailable;
    [SerializeField] private GameObject questOngoing;
    [SerializeField] private GameObject questCompleted;

    public GameObject victoryScreen;
    

    void Start() {
        questStatus = QuestStatus.available;
    }

    void Update()
    {
        // Default dialogue while quest is incomplete
        if (questStatus != QuestStatus.complete) {
            GetDialogue();  
        }

        // Change quest status IF player obtains quest item
        if (playerInventory.inventoryList.Contains(questItem) && questStatus != QuestStatus.complete) {
            questStatus = QuestStatus.complete;
        }

        // Update quest status bubble
        UpdateBubble();

        // Alternate dialogue/scenario when quest is complete
        if (Input.GetButtonDown("Interact") && playerInRange && questStatus == QuestStatus.complete)
        {
            GetAltDialogue();  
            StartCoroutine(QuestCompleteCo());
        }   
    }


    // Displays dialogue box & determines which string of dialogue to return
    private void GetDialogue()
    {
        // "if Player talks w/ NPC..."
        if (Input.GetButtonDown("Interact") && playerInRange)
        {
            // set quest as 'inProgress' if haven't already...
            if (questStatus == QuestStatus.available) {
                questStatus = QuestStatus.inProgress;
            }

            // display dialogue box
            if (dialogueBox.activeInHierarchy)
            {
                dialogueBox.SetActive(false);   // disable dialogue box  
                playerUI.SetActive(true);       // re-enable Player UI
                commandToolTip.SetActive(true);
            }
            else
            {
                playerUI.SetActive(false);      // disable Player UI
                commandToolTip.SetActive(false);
                dialogueBox.SetActive(true);    // enable dialogue box
                dialogueText.text = dialogue;   // display dialogue text
            }
        }
        // "if player moves out of talking range..."
        if (!playerInRange)
        {
            dialogueBox.SetActive(false);   // disable dialogue box  
            playerUI.SetActive(true);       // re-enable Player UI
            commandToolTip.SetActive(true);
        }
    }

    private void GetAltDialogue()
    {
        // "if Player talks w/ NPC..."
        if (dialogueBox.activeInHierarchy)
        {
            dialogueBox.SetActive(false);   // disable dialogue box  
            playerUI.SetActive(true);       // re-enable Player UI
            commandToolTip.SetActive(true);
        }
        else
        {
            playerUI.SetActive(false);      // disable Player UI
            commandToolTip.SetActive(false);
            dialogueBox.SetActive(true);    // enable dialogue box
            dialogueText.text = altDialogue;   // display dialogue text
        }
        // "if player moves out of talking range..."
        if (!playerInRange)
        {
            dialogueBox.SetActive(false);   // disable dialogue box  
            playerUI.SetActive(true);       // re-enable Player UI
            commandToolTip.SetActive(true);
        }
    }

    private IEnumerator QuestCompleteCo()
    {
        yield return new WaitForSeconds(2.5f);
        dialogueBox.SetActive(false);
        victoryScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    // Updates the text bubble depending on the quest status
    private void UpdateBubble()
    {
        if (questStatus == QuestStatus.available)
        {
            questAvailable.SetActive(true);
            questOngoing.SetActive(false);
            questCompleted.SetActive(false);
        }
        else if (questStatus == QuestStatus.inProgress)
        {
            questAvailable.SetActive(false);
            questOngoing.SetActive(true);
            questCompleted.SetActive(false);
        }
        else
        {
            questAvailable.SetActive(false);
            questOngoing.SetActive(false);
            questCompleted.SetActive(true);
        }
    }

    // Detects when player is in range...
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            playerInRange = true;
        }
    }

    // Detects when player is out of range...
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            playerInRange = false;
            dialogueBox.SetActive(false);
        }
    }

}