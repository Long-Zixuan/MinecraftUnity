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

    void Start()
    {
        getItem();
    }

    public void getItem()
    {
        for (int i = 0; i < 9; i++)
        {
            InventorySlot slot = Grid.transform.GetChild(i).GetComponent<InventorySlot>();

            if (slot == null || slot.ItemUi == null)
            {
                recip.items[i] = null;
                continue;
            }
            recip.items[i] = Grid.transform.GetChild(i).GetComponent<InventorySlot>().ItemUi.item_;
        }
    }

    public void minsItemInCrafting(int count)
    {
        for (int i = 0; i < 9; i++)
        {
            InventorySlot slot = Grid.transform.GetChild(i).GetComponent<InventorySlot>();
            if (slot == null || slot.ItemUi == null)
            {
                continue;
            }
            changeItemCount(slot.ItemUi.item_, -count);
            slot.ItemUi.ItemCount -= count;
            slot.updateSelf();
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
                resItem = resSlot.creatItemUI(item, item.recips.count);
                //print(item.itemName);
                return;
            }
        }

        if (resSlot.transform.childCount > 0)
        {
            Destroy(resSlot.transform.GetChild(0).gameObject);
        }
    }

    void Update()
    {
        //craft();
    }
    
    public override void itemJoin(ItemUI itemUI)
    {
        if (itemUI == resItem)
        {
            //Destroy(itemUI.gameObject);
            return;
        }
        craft();
    }

    public override void itemLeave(ItemUI itemUI)
    {
        if (itemUI == resItem)
        {
            print("res itemLeave");
            minsItemInCrafting(itemUI.ItemCount / itemUI.item_.recips.count);
            //return;
        }
        craft();
    }
    
    
}
