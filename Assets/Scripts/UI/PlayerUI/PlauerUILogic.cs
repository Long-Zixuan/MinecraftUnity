using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace UnityMC
{
    public class PlauerUILogic : MonoBehaviour
    {
        public PlayerController playerController_;

        [FormerlySerializedAs("inventoryUI")] public GameObject inventoryWindow;

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
                playerController_.InventoryLogic.updateInventory();
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
