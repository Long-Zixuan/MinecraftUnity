using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Crafting : InventoryLogic
{
    public InventorySlot resSlot;
    
    private ItemUI resItem = null;
    
    private Recips recip = new Recips() ;

    new void Awake()
    {
        base.Awake();
    }
    new void Start()
    {
        base.Start();
        getItem();
    }

    protected override void initSlots()
    {
        base.initSlots();
        slots.Add(resSlot);
        resSlot.Index = 9;
    }

    public void getItem()
    {
        for (int i = 0; i < 9; i++)
        {
            /*InventorySlot slot = Grid.transform.GetChild(i).GetComponent<InventorySlot>();

            if (slot == null || slot.ItemUi == null)
            {
                recip.items[i] = null;
                continue;
            }
            recip.items[i] = Grid.transform.GetChild(i).GetComponent<InventorySlot>().ItemUi.Item;*/
            recip.items[i] = getItemData(i).Item;
        }
    }

    public void minsItemInCrafting(int count)
    {
        for (int i = 0; i < 9; i++)
        {
            InventorySlot slot = slots[i];
            if (slot.IsInventoryNull || slot.Data.Item == null)
            {
                continue;
            }
            slot.updateInventoryData(slot.Data.Item, slot.Data.Count - count);
            //changeItemCount(slot.ItemUi.Item, -count);
            //slot.ItemUi.ItemCount -= count;
            //slot.updateSelf();
        }
    }

    public void craft()
    {
        try
        {
            getItem();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        foreach (var item in GameManager.Instance.items)
        {
            if (!item.canCrafting)
            {
                continue;
            }

            if (item.recips.isSame(recip))
            {
                resSlot.updateInventoryData(item, item.recips.count);
                //print(item.itemName);
                return;
            }
        }

        resSlot.updateInventoryData(null, 0);
    }

    void Update()
    {
        //craft();
    }
    
    public override void itemJoin(InventorySlot slot)
    {
        craft();
    }

    public override void itemLeave(InventorySlot slot)
    {
        craft();
    }
    
    
}
