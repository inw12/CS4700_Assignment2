using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Info")]
    public PlayerInventory playerInventory;                     // PlayerInventory object
    [SerializeField] private GameObject inventoryPanel;         // Inventory UI Panel
    [SerializeField] private GameObject inventorySlot;          // item slot
    [SerializeField] private TextMeshProUGUI descriptionText;   // description text
    [SerializeField] private GameObject useButton;              // "use" button
    public InventoryItem item;


    void OnEnable() // triggers whenever objects changes between Enabled/Disabled
    {
        ClearInventorySlots();
        CreateInventorySlots();
        SetTextAndButton("", false);
    } 

    public void SetTextAndButton(string description, bool buttonActive)
    {
        descriptionText.text = description;
        if (buttonActive) {
            useButton.SetActive(true);
        }
        else {
            useButton.SetActive(false);
        }
    }

    void CreateInventorySlots() {
        if (playerInventory)
        {
            for (int i = 0; i < playerInventory.inventoryList.Count; i++)
            {
                if (playerInventory.inventoryList[i].amount  > 0)
                {
                    GameObject temp = Instantiate(inventorySlot, inventoryPanel.transform.position, Quaternion.identity);
                    temp.transform.SetParent(inventoryPanel.transform);
                    InventorySlot newSlot = temp.GetComponent<InventorySlot>();
                    if (newSlot) {
                        newSlot.Setup(playerInventory.inventoryList[i], this);
                    }
                }
            }
        }
    }

    void ClearInventorySlots() {
        for (int i = 0; i < inventoryPanel.transform.childCount; i++) {
            Destroy(inventoryPanel.transform.GetChild(i).gameObject);
        }
    }    

    public void SetupDescriptionAndButton(string description, bool buttonAvailable, InventoryItem item) {
        this.item = item;
        descriptionText.text = description;
        useButton.SetActive(buttonAvailable);
    }

    public void UseButtonPressed() {
        if (item)
        {
            item.Use();

            // Clear inventory slots
            ClearInventorySlots();

            // Refill slots w/ new numbers
            CreateInventorySlots();
            if (item.amount <= 0) {
                SetTextAndButton("", false);
            }
        }
    }
}
