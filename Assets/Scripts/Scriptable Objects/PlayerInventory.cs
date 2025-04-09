// ************************************************
// • A list of inventory items held by the player
// ************************************************

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Player Inventory")]
public class PlayerInventory : ScriptableObject
{
    public List<InventoryItem> inventoryList = new();
}
