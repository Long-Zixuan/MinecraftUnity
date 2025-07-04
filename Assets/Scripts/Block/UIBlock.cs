using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityMC;

public class UIBlock : BaseBlock
{
    [SerializeField]
    protected UIType uiType_;

    public override bool OnToggle()
    {
       PlauerUIManager.Instance.openUI(uiType_,PlauerUIManager.Instance);
       return true;
    }
    
    
}
