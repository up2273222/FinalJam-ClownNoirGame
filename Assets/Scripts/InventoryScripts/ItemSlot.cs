using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    //===DATA===//
    public string ItemName;
    public Sprite ItemIcon;
    public bool full;

    //===SLOT===//
    [SerializeField]
    private TMP_Text name;
    [SerializeField]
    private Image icon;

    public void AddItem(string Name, Sprite ItemSprite)
    {
        ItemName = Name;
        ItemIcon = ItemSprite;

        name.text = ItemName;
        icon.sprite = ItemIcon;
    }
}
