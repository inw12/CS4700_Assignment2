using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    private bool playerInRange;
    private Animator anim;
    [SerializeField] private InventoryItem item;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI text;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact") && playerInRange)
        {
            // Update Animation
            anim.SetBool("isOpen", true);

            // Update Player Inventory
            if (playerInventory)
            {
                // "if the item already exists in the player's inventory..."
                if (playerInventory.inventoryList.Contains(item)) {
                    // Increase the amount held
                    item.amount++;      
                }
                else {
                    // Add item to player's inventory
                    playerInventory.inventoryList.Add(item);    
                    item.amount++;
                }
            }

            // UI Pop-Up for item received
            if (dialogueBox)
            {
                dialogueBox.SetActive(true);
                image.sprite = item.itemImage;
                text.text = item.itemName;
                StartCoroutine(NotificationCo());
            }
        }
    } 

    IEnumerator NotificationCo()
    {
        yield return new WaitForSeconds(2f);
        dialogueBox.SetActive(false);
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
        }
    }

}