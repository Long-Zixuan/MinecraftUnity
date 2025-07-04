using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUILogic : MonoBehaviour,IUi
{
    public GameObject window;
    public bool isOpening = false;

    protected IUi parentUi_;
    public virtual void onOpen(IUi parent)
    {
        if (parent != null)
        {
            parentUi_ = parent;
        }
        window.SetActive( true);
        isOpening = true;
    }
    
    public virtual void onClose()
    {
        parentUi_.onOpen(null);
        window.SetActive( false);
        isOpening = false;
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
