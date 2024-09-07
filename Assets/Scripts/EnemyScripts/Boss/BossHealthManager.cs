using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossHealthManager : DeathChecker
{
    BossBehaviourManager behavMgmnt;
    float lastStun;
    private void Awake()
    {
        behavMgmnt = GetComponent<BossBehaviourManager>();
    }
    void Update()
    {
        CheckStun();
    }
    public void CheckStun()
    {
        if (stunTimer > lastStun) 
        {
            behavMgmnt.animator.Play("Stun");
        }
        if (stunTimer > 0)
        {
            behavMgmnt.bossState.AddState(BossStates.STUNNED);
            stunTimer -= Time.deltaTime * 2; //Half stun duration

            if (stunTimer == 0.8f)
            {
                behavMgmnt.animator.Play("UnStun");
            }

        }
        else
        {
            behavMgmnt.bossState.ClearState(BossStates.STUNNED);
        }
        lastStun = stunTimer;
        
    }

    public override void TriggerDash()
    {
        Debug.Log("TriggerDash() (boss) is being called");
        behavMgmnt.ModifyBI(999);
        throw new System.NotImplementedException();
    }
}
