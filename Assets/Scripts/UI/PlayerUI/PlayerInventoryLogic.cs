using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInventoryLogic : InventoryLogic
{
    public GameObject quickBarGrid;

    public QuickBarLogic quickBar;

    new void Awake()
    {
        base.Awake();
    }
    
    // Start is called before the first frame update
    new void Start()
    {
        
    }

    protected override void initSlots()
    {
        if (slots.Count != 0)
        {
            return;
        }
        if (Grid.transform.GetChild(0).GetComponent<InventorySlot>() == null)
        {
            print("InventorySlot is null");
            return;
        }
        for (int i = 0; i < quickBarGrid.transform.childCount; i++)
        {
            slots.Add(quickBarGrid.transform.GetChild(i).GetComponent<InventorySlot>());
            quickBarGrid.transform.GetChild(i).GetComponent<InventorySlot>().Index = i;
        }
        int count = quickBarGrid.transform.childCount;
        for (int i = 0; i < Grid.transform.childCount; i++)
        {
            slots.Add(Grid.transform.GetChild(i).GetComponent<InventorySlot>());
            Grid.transform.GetChild(i).GetComponent<InventorySlot>().Index = i + count;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /*protected override ItemUI findItemUI(InventoryItem item)
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
    }*/

 
    
    /*protected override bool creatNewItem(InventoryItem item,int count)
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
    }*/

    
    public override void updateInventory()
    {
        base.updateInventory();
        quickBar.updateSelf(quickBarGrid);
       
    }
}
