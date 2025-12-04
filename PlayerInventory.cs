/**
sing System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<string> items = new List<string>();

    public bool HasItem(string id)
    {
        return items.Contains(id);
    }

    public void AddItem(string id)
    {
        if (!items.Contains(id))
            items.Add(id);
    }
}
*/


using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventory Data")]
    public List<string> items = new List<string>();
    public int maxSlots = 3;        

    [Header("Inventory UI")]
    public GameObject inventoryPanel;          
    public Button inventoryToggleButton;       
    public TextMeshProUGUI[] slotTexts;        

    private void Start()
    {
        
        if (inventoryPanel != null)
            inventoryPanel.SetActive(false);

        
        if (inventoryToggleButton != null)
            inventoryToggleButton.onClick.AddListener(ToggleInventory);

        RefreshUI();
    }

    public bool HasItem(string id)
    {
        return items.Contains(id);
    }

    public void AddItem(string id)
    {
        
        if (items.Count >= maxSlots)
        {
            Debug.Log("Inventory full, cannot pick up " + id);
            return;
        }

        items.Add(id);
        RefreshUI();
    }

    private void ToggleInventory()
    {
        if (inventoryPanel == null) return;

        bool isActive = inventoryPanel.activeSelf;
        inventoryPanel.SetActive(!isActive);

        
        if (!isActive)
            RefreshUI();
    }

    private void RefreshUI()
    {
        if (slotTexts == null || slotTexts.Length == 0) return;

        "
        for (int i = 0; i < slotTexts.Length; i++)
        {
            if (slotTexts[i] == null) continue;

            if (i < items.Count)
                slotTexts[i].text = items[i];   
            else
                slotTexts[i].text = "Empty";
        }
    }
}
