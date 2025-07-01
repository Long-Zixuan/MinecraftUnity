using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityMC;

public class ItemOnWorld : MonoBehaviour
{
    [SerializeField]
    protected InventoryItem item_;
    protected Inventriy inventory_;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inventory_ = other.gameObject.GetComponent<PlayerController>().Inventory;
            if (inventory_.addItem(item_))
            {
                Destroy(gameObject);
            }
        }
    }
}
