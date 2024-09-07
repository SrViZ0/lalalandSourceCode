using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

//Dont ask why are there like 4 namepaces.
[System.Flags]
public enum BossStates : short  
{
    None = 0,
    CHASE = 1 << 0,
    DASH = 1 << 1,
    SLASH = 1 << 2,
    SHOOT = 1 << 3,
    STUNNED = 1 << 4,
    VULNERABLE = 1 << 5,
    Everything = ~0      // All bits set (bitwise NOT 0)
}

public class BossState
{
    public BossStates states;

    public void AddState(BossStates state )
    {
        states |= state;
    }

    public void ClearState(BossStates state)
    {
        states &= ~state;
    }

    public bool IsState(BossStates state)
    {
        return (states & state) == state;
    }

}


[RequireComponent(typeof(BossActionManager))]
[RequireComponent(typeof(BossHealthManager))]
[RequireComponent(typeof(NavMeshAgent))]
public class BossBehaviourManager : MonoBehaviour
{
    [SerializeField] float behaviourIndex;
    [SerializeField] float aggroRange;
    [SerializeField] float aggrasiveness; // 0 ~ 1. How aggrasive the boss is, more aggrasive = more attacks, stick closer to the player
    float modeTimer;

    public BossState bossState = new BossState();

    public Transform player;
    BossActionManager attackManager;
    BossHealthManager healthManager;

    // Movement Variables
    NavMeshAgent navMeshAgent;
    [SerializeField] float runSpd;
    [SerializeField] float walkSpd;
    float currentSpd;

    [HideInInspector]public Animator animator;



    [Space(15)]

    [HideInInspector]public float debugBehaviourIndex;
    [HideInInspector]public bool debugBehaviour;
    [HideInInspector]public int stateControllerIndex;
    [HideInInspector]public BossStates debugStateSelection;
    [HideInInspector]public float stunDuration;
    private void Awake()
    {
        attackManager = GetComponent<BossActionManager>();
        healthManager = GetComponent<BossHealthManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = runSpd;
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        float v = !debugBehaviour ? behaviourIndex : debugBehaviourIndex;

        BehaviourConstructor(v);

        if (bossState.IsState(BossStates.CHASE) && CheckActiveState()) //Chase logic
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            {
                animator.Play("Run");
            }
            animator.SetBool("Run", true);

            behaviourIndex = Vector3.Distance(this.transform.position, player.transform.position) < aggroRange && behaviourIndex < 35 ? behaviourIndex += Time.deltaTime * 15 : behaviourIndex -= Time.deltaTime;
            ChasePlayer();
        }

        if (bossState.IsState(BossStates.STUNNED)) 
        {
            this.GetComponent<NavMeshAgent>().enabled = false;
        }
        else
        {
            this.GetComponent<NavMeshAgent>().enabled = true;
        }

        if (navMeshAgent.isOnNavMesh && bossState.IsState(BossStates.SHOOT))
        {
            navMeshAgent.speed = 0.3f;
            navMeshAgent.SetDestination(player.position);
        }
        else
        {
            navMeshAgent.speed = currentSpd;
        }   

        if (modeTimer < 0) 
        {
            ReflectAggrasiveness();
        }
        {
            modeTimer -= Time.deltaTime;
        }

        //Debugging stuff
        ModifyState(debugStateSelection);
    }

    public void BehaviourConstructor(float behaviour)
    {
        Mathf.Clamp(behaviour, 0f, 100f);

        if (!CheckActiveState()) return; // check if should construct actions

        bossState.ClearState(BossStates.CHASE);

        if (behaviour > 89)
        {
            bossState.AddState(BossStates.DASH);
            behaviourIndex = RNGGenny(20, 40);
        }
        else if (behaviour > 34)
        {
            behaviourIndex += attackManager.Attack();

            //if (behaviourIndex > 60) behaviourIndex -= (aggrasiveness * 10); //ToDo- nerf this. too OP boss perpeturally attacks
        }
        else if (behaviour <21)
        {
            bossState.AddState(BossStates.DASH);
            behaviourIndex = RNGGenny(20, 40);
        }
        else // val between 3 - 35
        {
            bossState.AddState(BossStates.CHASE);
        }
    }


    private void ReflectAggrasiveness()
    {
        //ToDo - roll speed type with aggrasiveness
        aggrasiveness = healthManager.health / healthManager.maxHealth + Random.Range(-0.3f, 0.3f);
        Mathf.Clamp01(aggrasiveness);

        float rng = Random.Range(0, 1);
        Mathf.Clamp(rng, 0.01f, 1);
        navMeshAgent.speed = rng > aggrasiveness ? runSpd : walkSpd;
        currentSpd = navMeshAgent.speed;

        modeTimer = Mathf.RoundToInt(Random.Range(5, 15));
    }

    public int RNGGenny(float a, float b)
    {
        return Random.Range(a, b).ConvertTo<int>();
    }

    private bool CheckActiveState()
    {
        // Early return if bossState is null
        if (bossState == null)
        {
            return false;
        }

        // Return true if none of the specified states are active
        return !bossState.IsState(BossStates.SHOOT) &&
               !bossState.IsState(BossStates.SLASH) &&
               !bossState.IsState(BossStates.DASH)  &&
               !bossState.IsState(BossStates.STUNNED);
    }

    private void ModifyState(BossStates statesInput)
    {
        switch (stateControllerIndex) 
        {
            case 1: bossState.AddState(statesInput);
                Debug.Log("States Added :)");
                break;

            case -1: bossState.ClearState(statesInput);
                Debug.Log("States Removed :)");
                break;

            case 0: return;
        }
        stateControllerIndex = 0;
    }

    public void ModifyBI(float input) //reflector since BI is priv
    {
        behaviourIndex += input;
    }

    private void ChasePlayer()
    {
        if (navMeshAgent.isOnNavMesh)
        {
            navMeshAgent.SetDestination(player.position);
        }
    }

    public bool IsOnNavMesh(Vector3 coords) //Checks if coords is hitting the navmesh
    {   
        return NavMesh.SamplePosition(coords, out NavMeshHit hit, 10f, NavMesh.AllAreas); //TODO - Placeholder name change later
       //return NavMesh.SamplePosition(coords, out NavMeshHit hit, 10f, NavMesh.GetAreaFromName("Walkable")); //Correct Solution?
    }

    public void StunBoss(float duration) //Manually stun boss
    {
        healthManager.stunTimer += duration;
    }
}



#if UNITY_EDITOR
[CustomEditor(typeof(BossBehaviourManager))]
public class  UTEdt : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        float stunTimer = 0;
        GUILayout.Space(20);

        GUIStyle style1 = new GUIStyle(EditorStyles.label)
        {
            fontSize = 14,
            fontStyle = FontStyle.Bold,
        };

        EditorGUILayout.LabelField("Debugging Tools", style1);
        GUILayout.Space(5);
        BossBehaviourManager script = (BossBehaviourManager)target;
        Rect behvIndx = EditorGUILayout.GetControlRect();
        script.debugBehaviourIndex = EditorGUI.IntField( behvIndx, new GUIContent("Behaviour Index", "Overrides behaviour index") ,Mathf.Clamp(script.debugBehaviourIndex.ConvertTo<int>(),0,100));

        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Load In Index")) 
        {
            script.debugBehaviour = true;
        }
        if (GUILayout.Button("Unload Index"))
        {
            script.debugBehaviour = false;
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(15);

        script.debugStateSelection = (BossStates)EditorGUILayout.EnumFlagsField(new GUIContent("State", "States to add manually"), script.debugStateSelection);

        GUILayout.Space(5);
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Add State(s)"))
        {
            script.stateControllerIndex++;
        }
        if (GUILayout.Button("Remove State(s)"))
        {
            script.stateControllerIndex--;
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(15);

        Rect stun = EditorGUILayout.GetControlRect();
        stunTimer = EditorGUI.FloatField(stun, new GUIContent("Stun Duration", "Stun Duration"),stunTimer);
        if (GUILayout.Button("Stun Boss"))
        {
            script.StunBoss(stunTimer);
        }
    }
}
#endif

