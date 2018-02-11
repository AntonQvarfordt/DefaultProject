using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


/// <summary>
/// This is the AI Controller,the script where the Finite State Machine will be create
/// the variables in this script are almost public,in oerder to let the state in the finite state machine
/// to access to them,like the "Black Board" Concept in unreal engine.
/// </summary>
public class AIController : Controller
{

    public Transform aiTankMainBody;

    public Transform aiTankTrack;

    public Transform aiTankNozzle;

    public Transform openFirePoint;

    public Transform explosionEffect;

    public Transform[] patrolWayPointArray;

    public NavMeshAgent NavMeshAgent { set; get; }

    public Animator Animator { set; get; }

    //The instance of the FiniteStateMachine
    public FiniteStateMachine FiniteStateMachine { set; get; }

    public GameObject[] PlayerTanksInScene { set; get; }

    //要攻击的目标
    public GameObject target { set; get; }

    public Renderer MainBodyRender{set;get;}

    public Renderer TrackRender { set; get; }

    public Renderer NozzleRender { set; get; }

    public ParticleSystem[] ParticleSystemArray { set; get; }

    /// <summary>
    /// the life system which record the life value
    /// </summary>
    public LifeSystem LifeSystem { set; get; }

    private float m_FSMUpdateTimer;

    /// <summary>
    /// The frequency we update the FSM
    /// </summary>
    public float updateInveral = 0.2f;

    protected override void Awake()
    {
        base.Awake();

        this.NavMeshAgent = GetComponent<NavMeshAgent>();

        this.Animator = GetComponent<Animator>();

        this.TrackRender = aiTankTrack.GetComponent<Renderer>();

        this.MainBodyRender = aiTankMainBody.GetComponent<Renderer>();

        this.NozzleRender = aiTankNozzle.GetComponent<Renderer>();

        this.ParticleSystemArray = explosionEffect.GetComponentsInChildren<ParticleSystem>();

        //Initialize FSM process
        FSMInitialize();

        m_FSMUpdateTimer = Time.time + updateInveral;

        LifeSystem = GetComponent<LifeSystem>();    
    }

    void Start()
    {
        //regist DrawViewEvent function to the showAIViewEvent in the UIManager
        UIManager.GetInstance().showAIViewEvent += DrawViewEvent;
    }

    //if we draw the AI view when game is running
    public void DrawViewEvent(bool value)
    {
        this.drawView = value;
    }

    /// <summary>
    /// The initialize process of FSM
    /// </summary>
    private void FSMInitialize()
    {
        //create the finite state machine
        FiniteStateMachine = new FiniteStateMachine();

        //create states and add them to the finite state machine we just created
        FiniteStateMachine.AddState(new PatrolState("PatrolState", this));
        FiniteStateMachine.AddState(new ChaseState("ChaseState", this));
        FiniteStateMachine.AddState(new AttackState("AttackState", this));
        FiniteStateMachine.AddState(new DieState("DieState", this));

        //according to the transition conditions, creating the transtion between states.
        FiniteStateMachine.CreateFSMStateToAnotherFSMStateTransition("PatrolState", "ChaseState", new IFSMTransitionCondition[1] { new PatrolToChaseCondition(this) });
        FiniteStateMachine.CreateFSMStateToAnotherFSMStateTransition("PatrolState", "AttackState", new IFSMTransitionCondition[1] { new PatrolToAttackCondition(this) });

        FiniteStateMachine.CreateFSMStateToAnotherFSMStateTransition("ChaseState", "AttackState", new IFSMTransitionCondition[1] { new ChaseToAttackCondition(this) });
        FiniteStateMachine.CreateFSMStateToAnotherFSMStateTransition("ChaseState", "PatrolState", new IFSMTransitionCondition[1] { new ChaseToPatrolCondition(this) });

        FiniteStateMachine.CreateFSMStateToAnotherFSMStateTransition("AttackState", "ChaseState", new IFSMTransitionCondition[1] { new AttackToChaseCondition(this) });
        FiniteStateMachine.CreateFSMStateToAnotherFSMStateTransition("AttackState", "PatrolState", new IFSMTransitionCondition[1] { new AttackToPatrolCondition(this) });

        FiniteStateMachine.CreateAnyFSMStateToFSMStateTransition("DieState", new IFSMTransitionCondition[1] { new AnyToDieCondition(this) });

        //set default state
        FiniteStateMachine.SetDefaultState("PatrolState");
        //set the end state
        FiniteStateMachine.SetEndState("DieState", new IFSMTransitionCondition[1] { new DieToExitCondition(this) });

        //initialize the fsm before we running it
        FiniteStateMachine.OnInitialize();
    }


    void Update()
    {
        //The frequency we update the FSM
        if (Time.time > m_FSMUpdateTimer)
        {
            m_FSMUpdateTimer = Time.time + updateInveral;

            //find all tanks which tag are "PlayerTank"
            PlayerTanksInScene = GameObject.FindGameObjectsWithTag("PlayerTank");

            //Update FSM
            FiniteStateMachine.OnUpdate();
        }

        // when tank is moving,offset tank track texture
        AnimatingTankTrack();

        //show this tank current life value
        UIManager.GetInstance().UpdateAIHP_AIState_DisplayInfo(LifeSystem.CurrentLifeValue.ToString(), FiniteStateMachine.OutputCurrentStateName());
    }

    /// <summary>
    /// when tank is moving,offset tank track texture
    /// </summary>
    private void AnimatingTankTrack()
    {
        float navMeshAgentSpeedNormalized = NavMeshAgent.velocity.magnitude / NavMeshAgent.speed;

        float textureOffSetSpeedFactor = 0.05f;

        Animator.SetFloat("moveSpeed", navMeshAgentSpeedNormalized);


        Material material = TrackRender.materials[0];

        float mainTextureOffsetX = material.mainTextureOffset.x - navMeshAgentSpeedNormalized * textureOffSetSpeedFactor;

        mainTextureOffsetX = Mathf.Repeat(mainTextureOffsetX, 1.0f);

        material.mainTextureOffset = new Vector2(mainTextureOffsetX, 0.0f);
    }

}
