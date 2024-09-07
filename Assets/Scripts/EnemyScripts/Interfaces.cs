using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IQuestObjective
{
    void EnemyEliminated(GameObject enemy);
}
public interface ICollisonsBullet
{ 
    void DamageTarget(GameObject target, GameObject source);
}

public interface ICollisonAmmo
{
    void PickUpAmmo(GameObject ammoPickUp);
}

public interface IStunTarget
{
    void StunTarget(GameObject target, GameObject source);
}

public interface IAddPoints
{
    void AddPoints(int points);
}

public interface IAddMultiplier
{
    void AddMutipliers(float mutiplier);
}

