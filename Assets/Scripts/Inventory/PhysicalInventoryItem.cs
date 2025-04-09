// *****************************************************
// • Script for objects in the world
// • When player walks over it, add item to inventory
// *****************************************************

using UnityEngine;

public class PhysicalInventoryItem : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private InventoryItem thisItem;    

    void OnTriggerEnter2D(Collider2D other)
    {
        // "if player walks over this item..."
        if (other.gameObject.CompareTag("Player") && !other.isTrigger)
        {
            AddItemToInventory();       // Add item to inventory
            Destroy(this.gameObject);   // Destroy the item from the game world
        }
    }

    void AddItemToInventory()
    {
        if (playerInventory && thisItem)
        {
            // "if the item already exists in the player's inventory..."
            if (playerInventory.inventoryList.Contains(thisItem)) {

                // Increase the amount held
                thisItem.amount++;
            }
            // "if it doesn't..."
            else {

                // Add item to player's inventory
                playerInventory.inventoryList.Add(thisItem);
                thisItem.amount++;
            }
        }
    }
}
