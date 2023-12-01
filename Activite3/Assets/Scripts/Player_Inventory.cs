using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    public Inventory inventory;

    private void Awake()
    {
        inventory = new Inventory(12);
    }
}
