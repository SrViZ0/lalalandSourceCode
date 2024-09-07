using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummiesHealthScript : DeathChecker
{
    private void OnDestroy()
    {
        if (MonoBehaviour.FindObjectOfType<KillQuestScript>() == null) return;
        IQuestObjective balld = FindObjectOfType<KillQuestScript>();
        balld.EnemyEliminated(this.gameObject);
    }
    public override void TriggerDash()
    {
        throw new System.NotImplementedException();
    }
}
