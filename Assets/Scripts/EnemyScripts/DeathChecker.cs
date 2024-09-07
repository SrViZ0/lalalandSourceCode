using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public abstract class DeathChecker : MonoBehaviour, ICollisonsBullet, IStunTarget
{
    [Header("Properties")]
    [SerializeField] private GameObject ammoDrop;

    //public UnityEvent chasePlayer;
    /*[HideInInspector]*/ public int health;
    public int maxHealth;

    [HideInInspector] public int shieldPoints;
    public int maxShieldPoints;

    [HideInInspector] Animator animator;
    public float stunTimer;

    [HideInInspector] public GameObject deathCause;

    [Space(5)]
    [Header("Points")]
    public int pointsGiven;
    public int multiGiven;
    float accumulatedDmg;

    [Space(3)]
    [Header("Audio")]
    public AudioClip hitMarker;
    public AudioSource hitSource;
    [Space(3)]

    [Header("Materials")]
    public Material enemyMaterial;
    public Color originalColor;
    [Space(3)]

    [Header("VFX")]
    public GameObject hitVFX;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void Start()
    {
        health = maxHealth;
        Debug.Log(gameObject.name + health);
        shieldPoints = maxShieldPoints;
    }



    public void CheckHealth() //Check if this enemy is still alive
    {
        if (health <= 0)
        {
            PointsSystemMaster inst = GameObject.Find("PointsManager").GetComponent<PointsSystemMaster>();
            AddPoints(inst,pointsGiven);
            AddMultipliers(inst, multiGiven);
            Quaternion euler = Quaternion.Euler(-90, 0 , Random.Range(-90,90));
            if (ammoDrop != null) Instantiate(ammoDrop, gameObject.transform.position, euler);
            //animator.Play("Death");
            Destroy(gameObject/*,animator.GetCurrentAnimatorClipInfo(0).Length*/);
        }
    }

    public void DamageTarget(GameObject target, GameObject source)
    {
        int dmg;
        target = this.gameObject;
        DamageScript dmgSource = source.GetComponent<DamageScript>();
        dmg = Mathf.RoundToInt(dmgSource.damage);
        if (dmgSource.damage < 1)
        {
            accumulatedDmg += dmgSource.damage;
            if (accumulatedDmg > 1) 
            { 
                dmg = 1;
                accumulatedDmg--;
            }
        }

        if (shieldPoints > 0 && dmgSource.damageType == DamageType.HV_ENV_DMG)
        {
            dmg = 0;
            TriggerDash();
        }

        health -= dmg; //real

        if (dmgSource.damageType == DamageType.LT_ENV_DMG) shieldPoints--;


        deathCause = source;
        Debug.Log(source);
        Debug.Log(deathCause + "killed Fella");

        CheckHealth();
    }


    public void StunTarget(GameObject target, GameObject source)
    {
        target = this.gameObject;
        target.GetComponent<DeathChecker>().stunTimer = source.GetComponent<DamageScript>().stunDuration;
        //Debug.Log(bullet.GetComponent<BulletBehave>().stunDuration);
    }

    public void AddPoints(IAddPoints ap ,int points)
    {
        ap.AddPoints(points);
    }

    public void AddMultipliers(IAddMultiplier am, float multi)
    {
        am.AddMutipliers(multi);
    }

    public abstract void TriggerDash();

    //public void ReduceHealth()
    //{
    //    health -= 1;
    //}

    //public void GetHit()
    //{
    //    Instantiate(hitVFX, this.gameObject.transform);
    //    Debug.Log("Hurt");
    //    hitSource.PlayOneShot(hitMarker);
    //    chasePlayer.Invoke();
    //    ReduceHealth();
    //    //enemyMaterial.color = Color.red;
    //}
    //public void ResetColor()
    //{
    //    enemyMaterial.color = originalColor;
    //}
    //public void Damaged()
    //{
    //    GetHit();
    //    CheckHealth();
    //    //Debug.Log("Hit");
    //    //Invoke("ResetColor", 0.2f);
    //}

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag.Equals(externalObjectTags))
        //{
        //    Debug.Log("hit");
        //    Damaged();
        //}
        //if (externalObjectTags.Any(CompareTag))
        //{
        //    Debug.Log("hit");
        //    Damaged();
        //}


        //if (other.gameObject.tag == ("Bullet"))
        //{
        //    Destroy(other.gameObject);
        //    Damaged();
        //}
    }
}
