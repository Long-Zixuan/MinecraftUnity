using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityMC;

public class CraftingUILogic : MonoBehaviour
{
    void Awake()
    {
        GetComponent<PlayerInventoryLogic>().updateInventory();
        Cursor.lockState = CursorLockMode.None;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
