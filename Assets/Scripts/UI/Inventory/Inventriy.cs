using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu(fileName = "New MC Inventory", menuName = "MCInventory/Inventory")]
public class Inventriy : ScriptableObject
{
    protected Dictionary<InventoryItem,int> items_ = new Dictionary<InventoryItem, int>();
    //public Dictionary<InventoryItem,int> Items { get { return items_; } }

    public string inLogicName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public bool addItem(InventoryItem item)
    {
       
        if (items_.ContainsKey(item))
        {
            items_[item]++;
        }
        else
        {
            items_.Add(item, 1);
            return GameObject.Find(inLogicName).GetComponent<InventoryLogic>().creatNewItem(item);
        }
        GameObject.Find(inLogicName).GetComponent<InventoryLogic>().updateInventory();
        Debug.Log("Adding item:"+item.itemName+":"+items_[ item]);
       return true;
    }*/

    public void changeItemCount(InventoryItem item, int count)
    {
        if (items_.ContainsKey(item))
        {
            items_[item] += count;
        }
        else
        {
            items_.Add(item, count);
        }
    }
    
    
}
