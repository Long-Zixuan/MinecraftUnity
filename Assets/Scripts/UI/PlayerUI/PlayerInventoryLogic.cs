using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInventoryLogic : InventoryLogic
{
    public GameObject quickBarGrid;

    public QuickBarLogic quickBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    protected override ItemUI findItemUI(InventoryItem item)
    {
        for (int i = 0; i < quickBarGrid.transform.childCount; i++)
        {
            InventorySlot slot = quickBarGrid.transform.GetChild(i).GetComponent<InventorySlot>();
            if (slot == null || slot.ItemUi == null)
            {
                continue;
            }
            if (slot.ItemUi.item_ == item)
            {
                return slot.ItemUi;
            }
        }
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

 
    
    protected override bool creatNewItem(InventoryItem item,int count)
    {
        for (int i = 0; i < quickBarGrid.transform.childCount; i++)
        {
            InventorySlot slot = quickBarGrid.transform.GetChild(i).GetComponent<InventorySlot>();
            if (slot.ItemUi == null)
            {
                slot.creatItemUI(item,count);
                slot.updateSelf();
                return true;
            }
        }
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

    
    public override void updateInventory()
    {
        for (int i = 0; i < quickBarGrid.transform.childCount; i++)
        {
            InventorySlot slot = quickBarGrid.transform.GetChild(i).GetComponent<InventorySlot>();
            slot.updateSelf();
        }
        print("updateInventory");
        quickBar.updateSelf(quickBarGrid);
        for (int i = 0; i < Grid.transform.childCount; i++)
        {
            InventorySlot slot = Grid.transform.GetChild(i).GetComponent<InventorySlot>();
            slot.updateSelf();
        }
    }
}
