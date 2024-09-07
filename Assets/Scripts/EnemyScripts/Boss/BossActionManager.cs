using UnityEngine;

public class BossActionManager : MonoBehaviour
{
    BossBehaviourManager behavMgmnt;

    [SerializeField] float slashDuration;
    [Tooltip("Minium Shooting range")]
    [SerializeField] float minShootDistance;
    [SerializeField] float dashDistance;

    [SerializeField] GameObject SlashHitbox;
    [SerializeField] GameObject SlashProjectile;

    [SerializeField] GameObject aftImg;

    [SerializeField] Transform shootPoint;
    [SerializeField] Transform spwnPoint;

    private float timer;

    [Header("Dash Data")]
    [SerializeField] GameObject tgt;
    [SerializeField] float dashDuration;
    [Tooltip("0 ~ 1, 1 is 100%")]
    [SerializeField] float dashStunChance;
    [SerializeField] float stunDuration;

    [Tooltip("size of Ara around the player")]
    [SerializeField] float dashRange;
    private Vector3 dashTargetPos;
    private Vector3 dashStartPos;
    private bool rolled = false;


    private void Awake()
    {
        behavMgmnt = GetComponent<BossBehaviourManager>();
    }
    private void Update()
    {

        if (behavMgmnt.bossState.IsState(BossStates.DASH)) // If dash
        {
            timer += Time.deltaTime;

            if (!rolled) dashTargetPos = Dash();

            rolled = true;

            //enter dash anim
            if (timer > dashDuration)
            {
                //exit dash anim
                this.transform.position = dashTargetPos;
                behavMgmnt.bossState.ClearState(BossStates.DASH);
                float rng = Random.Range(0, 1);

                if (rng <= dashStunChance)
                {
                    behavMgmnt.StunBoss(stunDuration);
                }

                timer = 0;
                rolled = false;
            }
        }

        if (behavMgmnt.bossState.IsState(BossStates.SHOOT))
        {
            ActionResetter(BossStates.SHOOT);
        }

        if (behavMgmnt.bossState.IsState(BossStates.SLASH))
        {
            ActionResetter(BossStates.SLASH);
        }
    }

    public float Attack()
    {
        if (Vector3.Distance(this.transform.position, behavMgmnt.player.transform.position) > minShootDistance) //Conditions for attacks
        {
            behavMgmnt.bossState.AddState(Shoot());//Call Shoot and update state\
            behavMgmnt.animator.Play("Slash 1");
            return behavMgmnt.RNGGenny(5, 15); //Early return so following code wont run
        }
        else
        {
            behavMgmnt.bossState.AddState(Slash());// Call Slash and update state
            behavMgmnt.animator.Play("Slash 0");
            aftImg.SetActive(true);
        }

        Debug.Log("");

        ///
        /// Implement IF we have animation Variants. ↓↓↓
        ///

        //float rng = Random.Range(0, 1);
        //string animName = rng < 0.5 ? "Slash 1" : "Slash 2"; //Play anim variant 1 or 2, Name sensitive
        //behavMgmnt.animator.Play(animName);




        return behavMgmnt.RNGGenny(10, 25);
    }
    public Vector3 Dash()
    {

        for (int i = 0; i < 32; i++)
        {
            Vector3 tgtPos = behavMgmnt.player.transform.position + new Vector3(Random.Range(-dashRange, dashRange), 0, Random.Range(-dashRange, dashRange)); //3 is offset make sure coord isnt IN the player
            Destroy(Instantiate(tgt, tgtPos, tgt.transform.rotation), 15f);
            if (behavMgmnt.IsOnNavMesh(tgtPos))// check if postion is valid
            {
                return tgtPos;
            }
        }
        //Play Dash anim
        behavMgmnt.animator.Play("Dash");
        return spwnPoint.transform.position;
    }
    public BossStates Slash()
    {
        behavMgmnt.bossState.AddState(BossStates.SLASH);
        int r = Random.Range(1, 3);
        for (int i = 0; i < r; i++)
        {
            Destroy(Instantiate(SlashHitbox, shootPoint.transform.position, this.transform.rotation, this.transform), slashDuration);
        }
        timer = slashDuration;
        return BossStates.SLASH;
    }
    public BossStates Shoot()
    {
        behavMgmnt.bossState.AddState(BossStates.SHOOT);
        int r = Random.Range(1, 2);
        for (int i = 0; i < r; i++)
        {
            Destroy(Instantiate(SlashProjectile, shootPoint.transform.position, this.transform.rotation), 3f);
        }   
        timer = slashDuration;
        return BossStates.SHOOT;
    }

    private void ActionResetter(BossStates state)
    {
        //Todo- if check anim finish
        //if anim !finsih retun

        timer -= Time.deltaTime;
        if (timer < 0) 
        {   
            behavMgmnt.bossState.ClearState(state);
        }
    }
}
