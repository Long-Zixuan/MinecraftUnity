using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryLogic : MonoBehaviour
{
    public Inventriy inventory_;

    public GameObject Grid;
    // Start is called before the first frame update
    void Start()
    {
        updateInventory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private bool creatNewItem(InventoryItem item,int count)
    {
        for (int i = 0; i < Grid.transform.childCount; i++)
        {
            InventorySlot slot = Grid.transform.GetChild(i).GetComponent<InventorySlot>();
            if (slot.ItemUi == null)
            {
                slot.creatItemUI(item,count);
                slot.updateSelf();
                return true;
            }
        }
        return false;
    }

    
    private ItemUI findItemUI(InventoryItem item)
    {
        for (int i = 0; i < Grid.transform.childCount; i++)
        {
            InventorySlot slot = Grid.transform.GetChild(i).GetComponent<InventorySlot>();
            if (slot == null || slot.ItemUi == null)
            {
                continue;
            }
            if (slot.ItemUi.item_ == item)
            {
                return slot.ItemUi;
            }
        }
        return null;
    }

    public bool changeItemCount(InventoryItem item, int count,bool creatItemUI = false)
    {
        print("changeItemCount,item:"+item.itemName+",count:"+count);
        if (!creatItemUI)
        {
            inventory_.changeItemCount(item, count);
            return true;
        }
        ItemUI itemUI = findItemUI(item);
        if (itemUI == null)
        {
            if (creatNewItem(item,count))
            {
                inventory_.changeItemCount(item, count);
                return true;
            }
            else
            {
                return false;
            }
        }
        itemUI.ItemCount += count;
        inventory_.changeItemCount(item, count);
        return true;
    }

    
    public void updateInventory()
    {
        for (int i = 0; i < Grid.transform.childCount; i++)
        {
            InventorySlot slot = Grid.transform.GetChild(i).GetComponent<InventorySlot>();
            slot.updateSelf();
        }
    }
    
}
