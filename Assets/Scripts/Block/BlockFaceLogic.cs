using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFaceLogic : MonoBehaviour
{
    private Renderer renderer_;
    // Start is called before the first frame update
    void Start()
    {
        renderer_ = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Block"))
        {
            renderer_.enabled = false;
            print("BlockFaceLogic OnTriggerEnter");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Block"))
        {
            renderer_.enabled = false;
            print("BlockFaceLogic OnTriggerStay");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Block"))
        {
            renderer_.enabled = true;
            print("BlockFaceLogic OnTriggerExit");
        }
    }
}
