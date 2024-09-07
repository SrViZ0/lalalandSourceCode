using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class BossSpawnScript : MonoBehaviour
{
    [SerializeField] GameObject bossCreaturela;
    GameObject boss;
    [HideInInspector]public static bool bossSpawned;
    [SerializeField] GameObject bossHudUI;

    [SerializeField] List<GameObject> bossStand = new List<GameObject>();

    private void Awake()
    {
        bossSpawned = false;
        bossHudUI.SetActive(false);
        foreach (GameObject part in bossStand)
        {
            part.SetActive(false);
        }
    }
    private void OnEnable()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (BossChecker() || bossSpawned) return;
        if (other.tag == "Player" && BossPartsCollected())
        {
            boss = Instantiate(bossCreaturela);
            boss.SetActive(true);
            foreach (GameObject part in bossStand)
            {
                part.SetActive(false);
            }
            bossSpawned = true;
            bossHudUI.SetActive(true );
            this.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private bool BossChecker()
    {
        return boss; //??? idk why I wrote it liek zis
    }
    private bool BossPartsCollected()
    {
        foreach (GameObject part in bossStand)
        {
            if (!part.gameObject.activeInHierarchy) return false;
        }
        return true;
    }
}
