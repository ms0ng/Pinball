using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MSFrame.FSM;

public class TigerMasterFSM
{
    public TigerMasterData data;
    public bool Pause;

    State sShout = new State("sShout");
    State sIdle = new State("sIdle");
    State sBreakdown = new State("sBreakdown");
    State sDying = new State("sDying");
    State sFirePunch = new State("sFirePunch");
    State sScratch = new State("sScratch");


    StateMachine rootMachine;
    StateMachine sAlive;

    void InitTransitions()
    {
        //Shout
        Transition tShoutToIdle = new Transition("tShoutToIdle", sIdle);
        tShoutToIdle.OnCheck += () =>
        {
            if (data.animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                return true;
            return false;
        };
        sShout.AddTransition(tShoutToIdle);

        //FirePunch
        Transition tIdleToFirePunch = new Transition("tIdleToFirePunch", sFirePunch);
        Transition tFirePunchToIdle = new Transition("tFirePunchToIdle", sIdle);
        tIdleToFirePunch.OnCheck += () =>
        {
            if (data.NextAttack.Equals("FirePunch") && data.NextAttackCountdown <= 0) return true;
            return false;
        };
        tFirePunchToIdle.OnCheck += () =>
        {
            if (data.animator.GetNextAnimatorStateInfo(0).IsName("FirePunch")) return false;

            //if (data.animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            //    return true;
            return true;
        };
        sIdle.AddTransition(tIdleToFirePunch);
        sFirePunch.AddTransition(tFirePunchToIdle);

        //Scratch
        Transition tIdleToScratch = new Transition("tIdleToScratch", sScratch);
        Transition tScratchToIdle = new Transition("tScratchToIdle", sIdle);
        tIdleToScratch.OnCheck += () =>
        {
            if (data.NextAttack.Equals("Scratch") && data.NextAttackCountdown <= 0) return true;
            return false;
        };
        tScratchToIdle.OnCheck += () =>
        {
            if (data.animator.GetNextAnimatorStateInfo(0).IsName("Scratch")) return false;

            //if (data.animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            //    return true;
            return true;
        };
        sIdle.AddTransition(tIdleToScratch);
        sScratch.AddTransition(tScratchToIdle);

        //Breakdown&Dying
        Transition tBreakdownRecover = new Transition("tBreakdownToIdle", sAlive);
        Transition tAnyToBreakdown = new Transition("tA2B", sBreakdown);
        Transition tAnyToDying = new Transition("tA2D", sDying);

        tBreakdownRecover.OnCheck += () =>
        {
            if (data.animator.GetCurrentAnimatorStateInfo(0).IsName("Breakdown") && data.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
            {
                return true;
            }
            return false;
        };
        tAnyToBreakdown.OnCheck += () =>
        {
            if (data.BreakdownDamage > data.BreakdownThreshold)
            {
                return true;
            }
            return false;
        };
        tAnyToDying.OnCheck += () =>
        {
            if (data.HP <= 0)
            {
                return true;
            }
            return false;
        };

        sBreakdown.AddTransition(tBreakdownRecover);
        sBreakdown.AddTransition(tAnyToDying);
        sAlive.AddTransition(tAnyToBreakdown);
        sAlive.AddTransition(tAnyToDying);
    }

    private void InitStateMachine()
    {
        sAlive = new StateMachine("MainMachine", sShout);
        rootMachine = new StateMachine("rootMachine", sAlive);
    }

    private void InitStates()
    {
        rootMachine.OnUpdate += (dt) =>
        {
            data.currentState = sAlive.CurrentStae.Name;
        };

        sIdle.OnEnter += (prev) =>
        {
            data.NextAttack = Random.Range(1, 100) % 2 == 0 ? "Scratch" : "FirePunch";
            data.NextAttackCountdown = Random.Range(1, 5);
        };
        //float tCount = 0;
        sIdle.OnUpdate += (float dt) =>
        {
            //tCount += dt;
            data.NextAttackCountdown -= dt;
            //if (tCount > 1)
            //{
            //    //Debug.Log($"HP: {data.HP}");
            //    Debug.Log("Idleing");
            //    tCount = 0;
            //}
        };

        sScratch.OnEnter += (prev) =>
          {
              data.animator.SetTrigger("Scratch");
          };

        sFirePunch.OnExit += (prev) =>
          {
              data.animator.SetTrigger("FirePunch");
          };

        sBreakdown.OnEnter += (IState prev) =>
        {
            data.BreakdownTimes++;
            data.BreakdownThreshold *= 1.5f;
            data.BreakdownDamage = 0;
            data.animator.SetTrigger("Breakdown");
            Debug.Log($"{sBreakdown.Name} On Enter");
        };
        sBreakdown.OnExit += (next) =>
        {
            data.animator.SetTrigger("BreakdownRecover");
        };

        sDying.OnEnter += (prev) =>
        {
            Debug.Log("Enemy Died!");
            data.animator.SetTrigger("Die");
        };
        sDying.OnUpdate += (dt) =>
        {

            if (data.animator.GetCurrentAnimatorStateInfo(0).IsName("Dying") && data.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
            {
                Pause = true;
                GameObject.Destroy(data.gameObject);
            }
        };

        //sAlive.OnUpdate += (dt) =>
        //{
        //    Debug.Log("MainMachining");
        //};
    }

    public void Start(TigerMasterData data)
    {
        this.data = data;
        InitStateMachine();
        InitTransitions();
        InitStates();
    }

    public void Update(float deltaTime)
    {
        rootMachine.OnStateUpdate(deltaTime);
    }

    public void FixedUpdate()
    {
        sAlive.OnStateFixedUpdate();
    }

    public void Attack()
    {

    }
}
