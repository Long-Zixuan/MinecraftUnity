using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;


[CreateAssetMenu(fileName = "New MC Inventory", menuName = "MCInventory/Inventory")]
public class Inventriy : ScriptableObject
{
    public List<ItemData> items_;
    public void changeItemCount(int index, ItemData data)
    {
        items_[index] = data;
    }
}


[System.Serializable]
public class ItemData
{
    [SerializeField] 
    private InventoryItem item_;
    [SerializeField] 
    private int count_;
    
    public InventoryItem Item
    {
        get { return item_; }
        set { item_ = value; }
    }
    public int Count
    {
        get { return count_; }
    }
    public ItemData(InventoryItem item, int count)
    {
        item_ = item;
        count_ = count;
    }
}
