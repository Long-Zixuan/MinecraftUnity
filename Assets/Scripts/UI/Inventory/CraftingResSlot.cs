using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingResSlot : InventorySlot
{
    public override void updateInventoryData(InventoryItem item, int count)
    {
        Crafting crafting = (Crafting)inventoryLogic_;
        if (Data.Item != null)
        {
            crafting.minsItemInCrafting(Data.Count/Item.recips.count);
        }
        base.updateInventoryData(item, count);
    }
}
