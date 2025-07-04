using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUi
{
    void onOpen(IUi self);
    void onClose();
}
