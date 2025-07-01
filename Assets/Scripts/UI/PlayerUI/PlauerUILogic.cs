using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlauerUILogic : MonoBehaviour
{
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
            inventoryWindow.SetActive(!inventoryWindow.activeSelf);
        }
    }
    
}
