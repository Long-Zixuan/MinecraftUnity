using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public ItemUI itemUIPre_;

    public ItemUI Item
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

    public void creatItemUI(InventoryItem item)
    {
        itemUIPre_ = Instantiate(itemUIPre_,transform);
        itemUIPre_.item_ = item;
    }

    public void updateSelf(Dictionary<InventoryItem,int> items)
    {
        if (transform.childCount > 0)
        {
            gameObject.transform.GetChild(0).GetComponent<ItemUI>().updateSelf(items);
        }
    }
    
}
