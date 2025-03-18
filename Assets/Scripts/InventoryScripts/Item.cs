using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private string ItemName;
    private Sprite Icon;
    private InventorySystem inventorySystem;

    private void Start()
    {
        inventorySystem = GameObject.Find("Inventory").GetComponent<InventorySystem>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            inventorySystem.AddItem(ItemName, Icon);
            Destroy(gameObject);
        }
    }
}
