using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemImage;
    public int amount;
    public bool isUsable;
    public bool isUnique;
    public UnityEvent thisEvent;

    // Triggers when the "use" button is pressed in the Inventory Panel
    public void Use() {
        thisEvent.Invoke();
    }

    public void Decrease(int amountToDecrease) {
        amount = amount < 0 ? 0 : amount - amountToDecrease;
    }
}
