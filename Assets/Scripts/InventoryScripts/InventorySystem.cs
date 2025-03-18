using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public ItemSlot[] slots;

    public void AddItem(string name, Sprite icon)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i].full == false)
            {
                slots[i].AddItem(name, icon);
                slots[i].full = true;
            }
        }
    }
}
