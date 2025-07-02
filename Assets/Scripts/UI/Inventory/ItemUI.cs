using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public InventoryLogic inventoryLogic_;
    
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
            Destroy(gameObject);
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

    private Transform originParent_;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        originParent_ = transform.parent;
        transform.SetParent(transform.parent.parent);
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
    
    
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        //print(eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == null)
        {
            //还差一个创建掉落物的逻辑
            inventoryLogic_.minusItemCount(item_,Convert.ToInt32(itemCountText_.text));
            Destroy(gameObject);
            return;
        }
        else if (eventData.pointerCurrentRaycast.gameObject.tag == "item")
        {
            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent);
            transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
            eventData.pointerCurrentRaycast.gameObject.transform.SetParent(originParent_);
            eventData.pointerCurrentRaycast.gameObject.transform.position = originParent_.transform.position;
            if (eventData.pointerCurrentRaycast.gameObject.GetComponent<ItemUI>().inventoryLogic_ != inventoryLogic_)
            {
                inventoryLogic_.minusItemCount(item_,Convert.ToInt32(itemCountText_.text));
                eventData.pointerCurrentRaycast.gameObject.GetComponent<ItemUI>().inventoryLogic_.plusItemCount(item_, Convert.ToInt32(itemCountText_.text));
            }
        }

        else if (eventData.pointerCurrentRaycast.gameObject.tag == "itemNumber")
        {
            GameObject item = eventData.pointerCurrentRaycast.gameObject.transform.parent.gameObject;
            transform.SetParent(item.transform.parent);
            transform.position = item.transform.position;
            item.transform.SetParent(originParent_);
            item.transform.position = originParent_.transform.position;
            if (item.GetComponent<ItemUI>().inventoryLogic_ != inventoryLogic_)
            {
                inventoryLogic_.minusItemCount(item_,Convert.ToInt32(itemCountText_.text));
                item.GetComponent<ItemUI>().inventoryLogic_.plusItemCount(item_, Convert.ToInt32(itemCountText_.text));
            }
        }

        else if (eventData.pointerCurrentRaycast.gameObject.tag == "BagSlots")
        {
            GameObject slot = eventData.pointerCurrentRaycast.gameObject;
            transform.SetParent(slot.transform);
            transform.position = slot.transform.position;
            if (slot.GetComponent<InventorySlot>().inventoryLogic_ != inventoryLogic_)
            {
                inventoryLogic_.minusItemCount(item_,Convert.ToInt32(itemCountText_.text));
                slot.GetComponent<InventorySlot>().creatItemUI(item_);
            }
        }
        else
        {
            transform.SetParent(originParent_);
            transform.position = originParent_.transform.position;
        }
        
        
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
