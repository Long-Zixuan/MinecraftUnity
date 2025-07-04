using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace UnityMC
{
    [System.Serializable]
    public enum UIType
    {
        Inventory,
        Crafting
    }
    [System.Serializable]
    public class UIAndType
    {
        public UIType type;
        public BaseUILogic uiLogic;
    }
    
    public class PlauerUIManager : MonoBehaviour,IUi
    {
        public UIAndType[] plauerUiLogics;
        
        
        protected Dictionary<UIType, BaseUILogic> plauerUiLogicDic_ = new Dictionary<UIType, BaseUILogic>();
        
        public PlayerController playerController_;

        [FormerlySerializedAs("inventoryUI")] public GameObject inventoryWindow;
        
        public GameObject craftingWindow;

        public bool HadPlauerUIOpen
        {
           get
           {
               foreach (var item in plauerUiLogics)
               {
                   if (item.uiLogic.isOpening)
                   {
                       return true;
                   }
               }
               return false;
           }
        }
        
        private static PlauerUIManager instance_s;
        
        public static PlauerUIManager Instance
        {
            get
            {
                if (instance_s == null)
                {
                    instance_s = FindObjectOfType<PlauerUIManager>();
                }
                return instance_s;
            }
        }
        

        private void Awake()
        {
            if (instance_s != null)
            {
                Debug.LogWarning(gameObject.name+":GameManager实例已存在");
                Destroy(gameObject);
                return;
            }
            foreach (var item in plauerUiLogics)
            {
                plauerUiLogicDic_.Add(item.type, item.uiLogic);
            }
            instance_s = this;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            openAndOffInventoryUI();
        }

        void openAndOffInventoryUI()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (HadPlauerUIOpen)
                {
                    closeOpenedPlayerUI();
                    return;
                }
                else
                {
                    openUI(UIType.Inventory,this);
                }
                /*if (craftingWindow.activeSelf)
                {
                    craftingWindow.SetActive( false);
                    return;
                }
                playerController_.InventoryLogic.onOpen();
                inventoryWindow.SetActive(!inventoryWindow.activeSelf);
                Cursor.visible = inventoryWindow.activeSelf;
                playerController_.CanMove = !inventoryWindow.activeSelf;
                if (!inventoryWindow.activeSelf)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.None;
                }*/
            }
        }

        public void openUI(UIType type,IUi parent)
        {
            GameManager.Instance.Player.CanMove = false;
            Cursor.lockState = CursorLockMode.None;
            plauerUiLogicDic_[type].onOpen(parent);
        }
        
        

        public void closeOpenedPlayerUI()
        {
            GameManager.Instance.Player.CanMove = true;
            Cursor.lockState = CursorLockMode.Locked;
           foreach (var item in plauerUiLogics)
            {
                if (item.uiLogic.isOpening)
                {
                    item.uiLogic.onClose();
                }
            } 
        }
        
        public void onOpen(IUi self)
        {
            print("All UI Closed");
        }

        public void onClose()
        {
            //print("All UI Closed");
        }

    }
}
