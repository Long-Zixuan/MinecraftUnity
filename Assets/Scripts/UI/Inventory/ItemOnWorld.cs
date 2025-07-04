using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityMC;

public class ItemOnWorld : MonoBehaviour
{
    [SerializeField]
    protected InventoryItem item_;
    protected InventoryLogic inventoryLogic_;
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
            inventoryLogic_ = other.gameObject.GetComponent<PlayerController>().InventoryLogic;
            if (inventoryLogic_.addItem(new ItemData(item_, 1)))
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("OnCollisionEnter Player");
            inventoryLogic_ = other.gameObject.GetComponent<PlayerController>().InventoryLogic;
            if (inventoryLogic_.addItem(new ItemData(item_, 1)))
            {
                Destroy(gameObject);
            }
        }
    }
}
