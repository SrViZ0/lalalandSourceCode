using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public enum DamageType
{
    TRU_DMG,    //Stuff like DOT goes here
    PLR_DMG,    //Damage Originating form the player E.G. bullet shot
    LT_ENV_DMG, //Falling blocks etc goes here
    HV_ENV_DMG  //Toilet bowl dmg, Vaccum dmg etc goes here

}
[RequireComponent(typeof(UID_Generator))]
public abstract class DamageScript : MonoBehaviour
{
    public DamageType damageType;
    public float damage;
    [Space(3)]
        
    public float stunDuration;
    [Space(7)]
    [Tooltip("Toggel damage over time")]
    public bool dmgOvrTime;
    public float dotDmg;

    private void Update()
    {
        if (dmgOvrTime) damage = dotDmg * Time.deltaTime;
    }
    public void DamageTarget(ICollisonsBullet hitTgt, GameObject target, GameObject source)
    {
        hitTgt.DamageTarget(target, source);
        Debug.Log(source);
    }
    public void StunTarget(IStunTarget hitTgt, GameObject target, GameObject source)
    {
        hitTgt.StunTarget(target, source);
    }
}