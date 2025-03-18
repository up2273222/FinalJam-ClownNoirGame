using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public List<Item> items;

    public void AddItem(Item newItem)
    {
        items.Add(newItem);
    }
}
