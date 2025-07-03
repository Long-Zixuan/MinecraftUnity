using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public InventoryLogic inventoryLogic_;
    
    public ItemUI itemUIPre_;

    public ItemUI ItemUi
    {
        get
        {
            if (transform.childCount == 0)
            {
                return null;
            }
            return gameObject.transform.GetChild(0).GetComponent<ItemUI>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ItemUI creatItemUI(InventoryItem item,int count)
    {
        ItemUI itemUI = Instantiate(itemUIPre_,transform);
        itemUI.item_ = item;
        itemUI.ItemCount = count;
        itemUI.inventoryLogic_ = inventoryLogic_;
        itemUI.updateSelf();
        return itemUI;
    }

    public void updateSelf()
    {
        if (transform.childCount > 0)
        {
            gameObject.transform.GetChild(0).GetComponent<ItemUI>().updateSelf();
        }
    }
    
}
