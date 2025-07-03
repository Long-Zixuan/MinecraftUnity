using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickBarLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateSelf(GameObject qucickBarGrid)
    {
        for (int i = 0; i < qucickBarGrid.transform.childCount; i++)
        {
            InventorySlot othSlot = qucickBarGrid.transform.GetChild(i).GetComponent<InventorySlot>();
            InventorySlot selfSlot = transform.GetChild(i).GetComponent<InventorySlot>();

            if (selfSlot.transform.childCount > 0)
            {
                Destroy(selfSlot.transform.GetChild(0).gameObject);
            }

            if (othSlot.ItemUi == null)
            {
                return;
            }
            selfSlot.creatItemUI(othSlot.ItemUi.item_, othSlot.ItemUi.ItemCount);
        }
    }
    
    
}
