using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackChecker : MonoBehaviour
{
    public CapsuleCollider boxCollider;

    public float prepTime;
    public float prepSpeed;
    public float attackDuration;

    public float attackSpeed;

    public static bool canAttack;
    public bool attackTracker;

    void Start()
    {
        boxCollider = GetComponent<CapsuleCollider>();
        canAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        attackTracker = canAttack;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //PrepAttack(prepSpeed);
        }
    }
    //public void PrepAttack(float prepSpeed)
    //{
    //    canAttack = true;
    //    EnemyBehave.navAgent.speed = prepSpeed;
    //    Debug.Log("prepare");
    //    StartCoroutine(LaunchAttack(attackSpeed));
    //}
    //public IEnumerator LaunchAttack(float attackDuration)
    //{
    //    EnemyBehave.navAgent.speed = attackSpeed;
    //    yield return new WaitForSeconds(attackDuration);
    //    DisableAttack();
    //}
    //public void DisableAttack()
    //{
    //    canAttack = false;
    //}
}
