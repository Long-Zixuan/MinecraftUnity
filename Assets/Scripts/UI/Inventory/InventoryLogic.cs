using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryLogic : MonoBehaviour
{
    public Inventriy inventory_;

    public GameObject Grid;

    protected List<InventorySlot> slots = new List<InventorySlot>();

    public List<InventorySlot> Slots
    {
       get { return slots; } 
    }


    protected void Awake()
    {
        
        initSlots();
        /*for (int i = 0; i < inventory_.items_.Count; i++)
        {
            print("items");
            print(inventory_.items_[i]);
        }*/
    }

    // Start is called before the first frame update
    protected void Start()
    {
        /*for (int i = 0; i < Grid.transform.childCount; i++)
        {
            slots.Add(Grid.transform.GetChild(i).GetComponent<InventorySlot>());
            Grid.transform.GetChild(i).GetComponent<InventorySlot>().Index = i;
        }*/
        updateInventory();
    }

    protected virtual void initSlots()
    {
        if (slots.Count != 0)
        {
            return;
        }
        for (int i = 0; i < Grid.transform.childCount; i++)
        {
            if (Grid.transform.GetChild(i).GetComponent<InventorySlot>() == null)
            {
                continue;
            }
            slots.Add(Grid.transform.GetChild(i).GetComponent<InventorySlot>());
            Grid.transform.GetChild(i).GetComponent<InventorySlot>().Index = i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
   /* protected virtual bool creatNewItem(InventoryItem item,int count)
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
    }*/

    
    /*protected virtual ItemUI findItemUI(InventoryItem item)
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
    }*/

    /*public bool changeItemCount(InventoryItem item, int count,bool creatItemUI = false)
    {
        print("changeItemCount"+gameObject.name+"item:"+item.itemName+",count:"+count);
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
        updateInventory();
        return true;
    }*/

    public bool addItem(ItemData data)
    {
        for (int i = 0; i < inventory_.items_.Count; i++)
        {
            if (inventory_.items_[i].Item == data.Item && inventory_.items_[i].Count < 64)
            {
                inventory_.changeItemCount(i,new ItemData(data.Item,data.Count + inventory_.items_[i].Count));
                updateInventory();
                return true;
            }
        }

        for (int i = 0; i < inventory_.items_.Count; i++)
        {
            if (inventory_.items_[i].Item == null)
            {
                inventory_.changeItemCount(i,data);
                updateInventory();
                return true;
            }
        }

        return false;
    }

    public void changeItemCount(int index, ItemData data)
    {
        inventory_.changeItemCount(index, data);
        updateInventory();
    }
    
    public ItemData getItemData(int index)
    {
        return inventory_.items_[index];
    }
    

    
    public virtual void updateInventory()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (inventory_.items_[i].Count <= 0)
            {
                inventory_.items_[i].Item = null;
            }
            slots[i].creatItemUI(inventory_.items_[i].Item, inventory_.items_[i].Count);
        }
    }
    
    public virtual void itemJoin(InventorySlot slot)
    {
        updateInventory();
    }

    public virtual void itemLeave(InventorySlot slot)
    {
        updateInventory();
    }

}
