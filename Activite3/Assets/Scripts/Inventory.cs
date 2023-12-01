using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    [System.Serializable]
    public class Slot
    {
        public Collectable.CollectableType type;
        public int count;
        public int maxAllowed;
        public Sprite icon;

        public Slot()
        {
            type = Collectable.CollectableType.NONE;
            count = 0;
            maxAllowed = 99;
        }

        public bool CanAddItem()
        {
            return count < maxAllowed;
        }

        public void AddItem(Collectable item)
        {
            if (type == Collectable.CollectableType.NONE)
            {
                type = item.type;
                icon = item.icon;
            }
            count++;
        }
    }

    public List<Slot> slots = new List<Slot>();

    public Inventory(int numSlots)
    {
        for (int i = 0; i < numSlots; i++)
        {
            slots.Add(new Slot());
        }
    }

    public void Add(Collectable item)
    {
        // Chercher d'abord un slot avec le même type et qui n'est pas plein
        foreach (Slot slot in slots)
        {
            if (slot.type == item.type && slot.CanAddItem())
            {
                slot.AddItem(item);
                return;
            }
        }

        // Si aucun slot existant n'est trouvé, chercher un slot vide
        foreach (Slot slot in slots)
        {
            if (slot.type == Collectable.CollectableType.NONE)
            {
                slot.AddItem(item);
                return;
            }
        }

        // Gérer le cas où il n'y a plus de place dans l'inventaire
        Debug.LogWarning("Pas assez de place dans l'inventaire pour ajouter cet objet.");
    }
}
