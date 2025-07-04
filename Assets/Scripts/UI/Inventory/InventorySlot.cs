using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField]
    protected int index_;
    
    public int Index
    {
        get
        {
            return index_;
        }
        set
        {
            index_ = value;
        }
    }


    
    [SerializeField]
    protected InventoryLogic inventoryLogic_;
    
    public bool IsInventoryNull
    {
        get { return inventoryLogic_ == null; }
    }

    
    public ItemUI itemUIPre_;

    protected ItemUI ItemUi
    {
        get
        {
//            print("itemUi:"+transform.childCount);
            if (transform.childCount == 0)
            {
                return null;
            }
            return gameObject.transform.GetChild(0).GetComponent<ItemUI>();
        }
    }

    protected InventoryItem Item
    {
        get
        {
            ItemData itemData = inventoryLogic_.getItemData(index_);
            return itemData.Item;
        }
        
    }

    public ItemData Data
    {
        get
        {
            return inventoryLogic_.getItemData(index_);
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
        if (ItemUi != null)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
            //Destroy(ItemUi.gameObject);
        }

        if (item == null)
        {
            return null;
        }
        ItemUI itemUI = Instantiate(itemUIPre_,transform);
        itemUI.Item = item;
        itemUI.ItemCount = count;

        itemUI.updateSelf();
        //updateInventoryData();//递归调用了
        return itemUI;
    }

    public void updateSelf()
    {
        if (transform.childCount > 0)
        {
            gameObject.transform.GetChild(0).GetComponent<ItemUI>().updateSelf();
        }
    }

    public virtual void updateInventoryData(InventoryItem item,int count)
    {
        inventoryLogic_.changeItemCount(index_,new ItemData(item,count));
    }
    
    public virtual void itemJoin(ItemUI itemUI)
    {
        inventoryLogic_.itemJoin(this);
    }

    public virtual void itemLeave(ItemUI itemUI)
    {
        inventoryLogic_.itemLeave(this);
    }
    
}
