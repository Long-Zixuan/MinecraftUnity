using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : InventoryLogic
{
    public InventorySlot res;
    
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
                //print(item.itemName);
                break;
            }
        }
    }

    void Update()
    {
        craft();
    }
    
    
}
