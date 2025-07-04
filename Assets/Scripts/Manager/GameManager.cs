using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityMC;

public class GameManager : MonoBehaviour
{
    public InventoryItem[] items;

    [SerializeField]
    private PlayerController player;

    public PlayerController Player
    {
        get
        {
            return player;
        }
    }
    
    public PlauerUIManager playerUIManager;

    public bool CanPlayerMove
    {
        get
        {
            return playerUIManager.HadPlauerUIOpen == false;
        }
    }
    
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

    public void onUIOpen()
    {
        
    }

    public void onUIClose()
    {
        
    }
    
    
}
