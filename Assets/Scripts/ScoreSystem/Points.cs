using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points
{
    public PointsInfoSO info;

    private bool unlocked = false;
    private bool active = false;
    private float multiplier;

    public Points(PointsInfoSO pointsInfo)
    {
        this.info = pointsInfo;
        unlocked = false;
        active = false;
        multiplier = 0;
    }

    public Points(PointsInfoSO pointsInfo, bool unlocked, bool active ,float multiplier)
    {
        this.info = pointsInfo;
        this.unlocked = unlocked;
        this.active = active;
        this.multiplier = multiplier;
    }
}
