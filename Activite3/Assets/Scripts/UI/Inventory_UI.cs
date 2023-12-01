using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Player player;
    public List<Slot_UI> slots = new List<Slot_UI>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        // Vérifie si inventoryPanel est non nul
        if (inventoryPanel != null)
        {
            bool isActive = inventoryPanel.activeSelf;
            inventoryPanel.SetActive(!isActive);

            if (!isActive)
            {
                Setup();
            }
        }
        else
        {
            Debug.LogError("Inventory Panel n'est pas assigné dans l'inspecteur!");
        }
    }

    void Setup()
    {
        // Vérifie si player et player.inventory sont non nuls
        if (player != null && player.inventory != null && slots.Count == player.inventory.slots.Count)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (player.inventory.slots[i].type != Collectable.CollectableType.NONE)
                {
                    slots[i].SetItem(player.inventory.slots[i]);
                }
                else
                {
                    slots[i].SetEmpty();
                }
            }
        }
        else
        {
            Debug.LogError("Player, Player Inventory ou Slots ne sont pas correctement initialisés!");
        }
    }
}
