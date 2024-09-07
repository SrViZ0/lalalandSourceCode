using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscUnlockEvent
{
    public event Action<string> onMiscUnlock;

    public void UnlockSpec(string id)
    {
        if (onMiscUnlock != null)
            onMiscUnlock(id);
    }
}
