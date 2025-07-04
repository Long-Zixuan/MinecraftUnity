using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    //public InventoryLogic inventoryLogic_;
    [SerializeField]
    private Text itemCountText_;
    [SerializeField]
    private Image itemIcon_;
    
    [SerializeField]
    private InventoryItem item_;

    public InventoryItem Item
    {
       get => item_;
       set => item_ = value;
    }

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
    
    private InventorySlot originSlot_;

    private bool isSingle_ = false;

    private ItemUI newItemUI_ = null;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        originParent_ = transform.parent;
        originSlot_ = originParent_.GetComponent<InventorySlot>();
        
        transform.SetParent(transform.parent.parent);
       
        
        if (Input.GetButton("Fire2") && ItemCount > 1)
        {
            isSingle_ = true;
            //newItemUI_ = originSlot_.creatItemUI(item_, ItemCount - 1);
            originSlot_.updateInventoryData(item_,ItemCount - 1);
            ItemCount = 1;
        }
        else
        {
            originSlot_.updateInventoryData(null,0);
        }
       
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        //inventoryLogic_.itemLeave( this);
        originSlot_.itemLeave( this);
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
            //inventoryLogic_.changeItemCount(item_,-Convert.ToInt32(itemCountText_.text));
            //originSlot_.updateInventoryData();
            Destroy(gameObject);
            return;
        }
        else if (eventData.pointerCurrentRaycast.gameObject.tag == "item")
        {
            InventorySlot otherSlot =
                eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<InventorySlot>();;
            if (otherSlot.Data.Item == item_)
            {
                //otherSlot.creatItemUI(Item, ItemCount+otherSlot.ItemUi.ItemCount);
                otherSlot.updateInventoryData(Item, ItemCount+otherSlot.Data.Count);
                Destroy(gameObject);
                return;
            }
            if (isSingle_)
            {
                moveFail();
                return;
            }
            
            //ItemUI otherItemUi = otherSlot.ItemUi;
            InventoryItem otherItem = otherSlot.Data.Item;
            int otherItemCount = otherSlot.Data.Count;

            //otherSlot.creatItemUI(Item, ItemCount);
            otherSlot.updateInventoryData(Item, ItemCount);

            //originSlot_.creatItemUI(otherItem, otherItemCount);
            originSlot_.updateInventoryData(otherItem, otherItemCount);
            
            //inventoryLogic_.itemJoin( this);
            otherSlot.itemJoin(this);
            
            Destroy(gameObject);
        }

        else if (eventData.pointerCurrentRaycast.gameObject.tag == "itemNumber")
        {

            InventorySlot otherSlot =
                eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<InventorySlot>();;
            if (otherSlot.Data.Item == item_)
            {
                //otherSlot.creatItemUI(Item, ItemCount+otherSlot.ItemUi.ItemCount);
                otherSlot.updateInventoryData(Item, ItemCount+otherSlot.Data.Count);
                Destroy(gameObject);
                return;
            }
            if (isSingle_)
            {
                moveFail();
                return;
            }
            
            //ItemUI otherItemUi = otherSlot.ItemUi;
            InventoryItem otherItem = otherSlot.Data.Item;
            int otherItemCount = otherSlot.Data.Count;

            //otherSlot.creatItemUI(Item, ItemCount);
            otherSlot.updateInventoryData(Item, ItemCount);

            //originSlot_.creatItemUI(otherItem, otherItemCount);
            originSlot_.updateInventoryData(otherItem, otherItemCount);
            
            //inventoryLogic_.itemJoin( this);
            otherSlot.itemJoin(this);
            
            Destroy(gameObject);
        }

        else if (eventData.pointerCurrentRaycast.gameObject.tag == "BagSlots")
        {

            //isSingle_ = false;
            InventorySlot slot = eventData.pointerCurrentRaycast.gameObject.GetComponent<InventorySlot>();
            //slot.creatItemUI(Item, ItemCount);
            slot.updateInventoryData(Item, ItemCount);
            //originSlot_.updateInventoryData();
            slot.itemJoin(this);
            Destroy(gameObject);
        }
        else
        {
            if (!isSingle_)
            {
               // originSlot_.creatItemUI(Item, ItemCount);
                originSlot_.updateInventoryData(Item, ItemCount);
                Destroy(gameObject);
                //transform.SetParent(originParent_);
                //transform.position = originParent_.transform.position;
            }
            else
            {
                moveFail();
            }
        }
        //GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    void moveFail()
    {
        //originSlot_.creatItemUI(Item, ItemCount + newItemUI_.ItemCount);
        originSlot_.updateInventoryData(Item, ItemCount + newItemUI_.ItemCount);
        //if (newItemUI_ == null)
        //{
            /*newItemUI_ = Instantiate(this.gameObject, originParent_.position, Quaternion.identity,originParent_);
            newItemUI_.GetComponent<ItemUI>().inventoryLogic_ = inventoryLogic_;
            newItemUI_.GetComponent<ItemUI>().item_ = item_;
            newItemUI_.GetComponent<ItemUI>().ItemCount = 1;
            newItemUI_.GetComponent<ItemUI>().updateSelf();*/
        //}
        //else
        //{
        //    newItemUI_.GetComponent<ItemUI>().ItemCount += 1;
        //}
        Destroy(gameObject);
    }
    
}
