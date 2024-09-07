using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEvent
{

    public event Action<string> onItemUnlock;
    public event Action<string> onSkinApply;


    public void UnlockItem(string id)
    {
        if (onItemUnlock != null)
        {
            onItemUnlock(id);
        }
    }
    public void ApplySkin(string id) //Not used
    {
        if (onSkinApply != null)
        {
            onSkinApply(id);
        }
    }
}
