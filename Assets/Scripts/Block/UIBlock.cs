using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UIBlock : BaseBlock
{
    [FormerlySerializedAs("uiName")] [SerializeField]
    protected string uiTag;

    public override bool OnToggle()
    {
        //Toggle the UI
       GameObject ui = GameObject.FindWithTag(uiTag);
       if (ui == null)
       {
           print("UI not found");
           return false;
       }
       ui.SetActive(!ui.activeSelf);
       return true;
    }
    
    
}
