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
    
    public class PlauerUIManager : MonoBehaviour
    {
        public PlayerController playerController_;

        [FormerlySerializedAs("inventoryUI")] public GameObject inventoryWindow;
        
        public GameObject craftingWindow;

        public bool HadUIOpen
        {
           get => inventoryWindow.activeSelf || craftingWindow.activeSelf; 
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
                if (craftingWindow.activeSelf)
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
                }
            }
        }

    }
}
