using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public Text itemCountText_;
    
    public Image itemIcon_;
    
    public InventoryItem item_;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void updateSelf(Dictionary<InventoryItem,int> items)
    {
        begin:if(item_ == null)
        {
            itemIcon_.color = new Color(0,0,0,0.0f);
            itemCountText_.enabled = false;
            return;
        }
        if (items[item_] <= 0)
        {
            item_ = null;
            goto begin;
        }
        
        itemCountText_.enabled = true;
        itemIcon_.color = new Color(1,1,1,1.0f);
        itemIcon_.sprite = item_.itemIcon;
        itemCountText_.text = items[item_].ToString();
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    { 
        transform.position = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }
    
    public void OnEndDrag(PointerEventData eventData){}
}
