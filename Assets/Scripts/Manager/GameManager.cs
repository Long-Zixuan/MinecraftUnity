using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public InventoryItem[] items;
    
    static private GameManager instance_s;

    public static GameManager Instance
    {
        get
        {
            return instance_s;
        }
    }
    void Awake()
    {
        if (instance_s != null)
        {
            Destroy(this.gameObject);
            Debug.LogWarning(gameObject.name+":GameManager实例已存在");
            return;
        }
        instance_s = this;
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
