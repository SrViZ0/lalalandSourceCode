using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PointsEvent
{
    public event Action<string> onMultiplierIncrement;
    public event Action onUnlockSkin;


    public void AddMultiplier(string id)
    {
        if (onMultiplierIncrement != null)
        {
            onMultiplierIncrement(id);
        }
    }

    public void UnlockSkins()
    {
        if (onUnlockSkin != null)
        {
            onUnlockSkin();
        }
    }

}
