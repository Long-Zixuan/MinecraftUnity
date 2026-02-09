using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityMC;

public class GameManager : MonoBehaviour
{
    public World world;
    public GameObject worldPrefabs;
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
        if (world == null)
        {
            world = findWorldObj();
            world.transform.position = Vector3.zero;
        }

        if (world == null)
        {
            world = creatWorldObj();
        }
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

    private World findWorldObj()
    {
        return GameObject.Find("World").GetComponent<World>();
    }

    private World creatWorldObj()
    {
        GameObject worldObj = Instantiate(worldPrefabs,Vector3.zero,Quaternion.identity);
        World w = worldObj.AddComponent<World>();
        return w;
    }
    
}
