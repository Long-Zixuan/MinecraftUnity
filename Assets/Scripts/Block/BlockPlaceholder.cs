using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityMC;

public class BlockPlaceholder : MonoBehaviour
{
    public BaseBlock block;

    public BaseBlock blockIns;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(GameManager.Instance.Player.transform.position, transform.position) <= 10)
        {
            if (blockIns == null)
            {
                blockIns = Instantiate(block, transform.position, Quaternion.identity, transform);
            }
        }
        else
        {
            if (blockIns != null)
            {
                Destroy(blockIns.gameObject);
            }
        }
    }
}
