using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("UI Elements to Change")]
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemAmountText;

    [Header("Item Variables")]
    public InventoryItem item;
    public InventoryManager manager;


    public void Setup(InventoryItem item, InventoryManager manager)
    {
        this.item = item;
        this.manager = manager;
        
        // "if this item exists..."
        if (item) {
            itemImage.sprite = item.itemImage;              // load item image
            itemAmountText.text = "" + item.amount;     // load item amount
        }
    }

    public void ClickedOn()
    {
        // "if this item exists..."
        if (item)
        {
            manager.SetupDescriptionAndButton(item.itemDescription, item.isUsable, item);
        }
    }
}
