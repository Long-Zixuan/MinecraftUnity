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

    public int ItemCount
    {
        get => Convert.ToInt32(itemCountText_.text);
        set => itemCountText_.text = value.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void updateSelf()
    {
        begin:if(item_ == null)
        {
            Destroy(gameObject);
            return;
        }
        if (Convert.ToInt32(itemCountText_.text) <= 0)
        {
            item_ = null;
            goto begin;
        }
        itemCountText_.enabled = true;
        itemIcon_.color = new Color(1,1,1,1.0f);
        itemIcon_.sprite = item_.itemIcon;
    }

    private Transform originParent_;

    private bool isSingle_ = false;

    private GameObject newItemUI_ = null;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        originParent_ = transform.parent;
        transform.SetParent(transform.parent.parent);
        if (Input.GetButton("Fire2") && ItemCount > 1)
        {
            isSingle_ = true;
            newItemUI_ = Instantiate(this.gameObject, originParent_.position, Quaternion.identity,originParent_);
            newItemUI_.GetComponent<ItemUI>().inventoryLogic_ = inventoryLogic_;
            newItemUI_.GetComponent<ItemUI>().item_ = item_;
            newItemUI_.GetComponent<ItemUI>().ItemCount = ItemCount - 1;
            newItemUI_.GetComponent<ItemUI>().updateSelf();
            ItemCount = 1;
        }

       
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        inventoryLogic_.itemLeave( this);
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
            GameObject player = GameObject.Find("Player");
            for (int i = 0; i < Convert.ToInt32(itemCountText_.text); i++)
            {
                Instantiate(item_.itemPrefab,player.transform.position,Quaternion.identity);
            }
            inventoryLogic_.changeItemCount(item_,-Convert.ToInt32(itemCountText_.text));
            Destroy(gameObject);
            return;
        }
        else if (eventData.pointerCurrentRaycast.gameObject.tag == "item")
        {

            if (eventData.pointerCurrentRaycast.gameObject.GetComponent<ItemUI>().item_ == item_)
            {
                eventData.pointerCurrentRaycast.gameObject.GetComponent<ItemUI>().ItemCount += ItemCount;
                Destroy(gameObject);
                return;
            }
            if (isSingle_)
            {
                moveFail();
                return;
            }
            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent);
            transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
            eventData.pointerCurrentRaycast.gameObject.transform.SetParent(originParent_);
            eventData.pointerCurrentRaycast.gameObject.transform.position = originParent_.transform.position;
            if (eventData.pointerCurrentRaycast.gameObject.GetComponent<ItemUI>().inventoryLogic_ !=
                inventoryLogic_)
            {
                inventoryLogic_.changeItemCount(item_, -Convert.ToInt32(itemCountText_.text));
                eventData.pointerCurrentRaycast.gameObject.GetComponent<ItemUI>().inventoryLogic_
                    .changeItemCount(item_, Convert.ToInt32(itemCountText_.text));
                inventoryLogic_ = eventData.pointerCurrentRaycast.gameObject.GetComponent<ItemUI>().inventoryLogic_;
            }
            inventoryLogic_.itemJoin( this);

        }

        else if (eventData.pointerCurrentRaycast.gameObject.tag == "itemNumber")
        {

            GameObject itemObj = eventData.pointerCurrentRaycast.gameObject.transform.parent.gameObject;
            if (itemObj.GetComponent<ItemUI>().item_ == item_)
            {
                itemObj.GetComponent<ItemUI>().ItemCount += ItemCount;
                Destroy(gameObject);
                return;
            }
            if (isSingle_)
            {
                moveFail();
                return;
            }
            transform.SetParent(itemObj.transform.parent);
            transform.position = itemObj.transform.position;
            itemObj.transform.SetParent(originParent_);
            itemObj.transform.position = originParent_.transform.position;
            if (itemObj.GetComponent<ItemUI>().inventoryLogic_ != inventoryLogic_)
            {
                inventoryLogic_.changeItemCount(item_, -Convert.ToInt32(itemCountText_.text));
                itemObj.GetComponent<ItemUI>().inventoryLogic_
                    .changeItemCount(item_, Convert.ToInt32(itemCountText_.text));
                inventoryLogic_ = itemObj.GetComponent<ItemUI>().inventoryLogic_;
            }
            inventoryLogic_.itemJoin(this);

        }

        else if (eventData.pointerCurrentRaycast.gameObject.tag == "BagSlots")
        {

            isSingle_ = false;
            GameObject slot = eventData.pointerCurrentRaycast.gameObject;
            transform.SetParent(slot.transform);
            transform.position = slot.transform.position;
            if (slot.GetComponent<InventorySlot>().inventoryLogic_ != inventoryLogic_)
            {
                inventoryLogic_.changeItemCount(item_, -Convert.ToInt32(itemCountText_.text));
                slot.GetComponent<InventorySlot>().inventoryLogic_
                    .changeItemCount(item_, Convert.ToInt32(itemCountText_.text));
                inventoryLogic_ = slot.GetComponent<InventorySlot>().inventoryLogic_;
            }
            inventoryLogic_.itemJoin(this);

        }
        else
        {
            if (!isSingle_)
            {
                transform.SetParent(originParent_);
                transform.position = originParent_.transform.position;
            }
            else
            {
                moveFail();
            }
        }
        
        
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    void moveFail()
    {
        if (newItemUI_ == null)
        {
            /*newItemUI_ = Instantiate(this.gameObject, originParent_.position, Quaternion.identity,originParent_);
            newItemUI_.GetComponent<ItemUI>().inventoryLogic_ = inventoryLogic_;
            newItemUI_.GetComponent<ItemUI>().item_ = item_;
            newItemUI_.GetComponent<ItemUI>().ItemCount = 1;
            newItemUI_.GetComponent<ItemUI>().updateSelf();*/
        }
        else
        {
            newItemUI_.GetComponent<ItemUI>().ItemCount += 1;
        }
        Destroy(gameObject);
    }
    
}
