using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UIElements;


[System.Serializable]
public class Recips
{
    public InventoryItem[] items = new InventoryItem[9];
    public int count;

    public bool isSame(Recips other)
    {
        if (other == null)
        {
            return false;
        }
        for (int i = 0; i < 9; i++)
        {
           // MonoBehaviour.print(other.items.Length+":"+items.Length);
            try
            {
                if (other.items[i] == null && items[i] == null)
                {
                    continue;
                }

                if (other.items[i] == null || items[i] == null)
                {
                    return false;
                }

                if (items[i] != other.items[i])
                {
                    return false;
                }
            }
            catch(Exception e){}
        }
        return true;
    }
}

[CreateAssetMenu(fileName = "New MC Inventory Item", menuName = "MCInventory/Item")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public GameObject itemPrefab;
    public Recips recips;

    public bool canCrafting = true;
    //public int itemCount;
    public bool toggleable = false;
    public GameObject togglePrefab;
}
