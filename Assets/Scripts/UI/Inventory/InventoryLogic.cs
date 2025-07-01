using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryLogic : MonoBehaviour
{
    public Inventriy inventory_;

    public GameObject Grid;
    // Start is called before the first frame update
    void Start()
    {
        updateInventory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public bool creatNewItem(InventoryItem item)
    {
        for (int i = 0; i < Grid.transform.childCount; i++)
        {
            InventorySlot slot = Grid.transform.GetChild(i).GetComponent<InventorySlot>();
            if (slot.Item == null)
            {
                slot.creatItemUI(item);
                slot.updateSelf(inventory_.Items);
                return true;
            }
        }
        return false;
    }

    

    public void updateInventory()
    {
        for (int i = 0; i < Grid.transform.childCount; i++)
        {
            InventorySlot slot = Grid.transform.GetChild(i).GetComponent<InventorySlot>();
            slot.updateSelf(inventory_.Items);
        }
    }
    
}
