using MSFrame.FSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.AI.GreenSlime
{
    class Prepare : State
    {
        public Prepare(string name) : base(name)
        {

        }

        public override void OnStateEnter(IState prevState)
        {
            base.OnStateEnter(prevState);

        }

        public override void OnStateExit(IState next)
        {
            base.OnStateExit(next);
        }

        public override void OnStateUpdate(float deltaTime)
        {
            base.OnStateUpdate(deltaTime);
        }
    }

    class Idle : State
    {
        public Idle(string name) : base(name)
        {

        }
        public override void OnStateEnter(IState prevState)
        {
            base.OnStateEnter(prevState);
            Debug.Log("Enter Idle");
        }

        public override void OnStateExit(IState next)
        {
            base.OnStateExit(next);
            Debug.Log("Exit Idle");
        }

        public override void OnStateFixedUpdate()
        {
            base.OnStateFixedUpdate();
        }

        private float timeCount = 0;
        public override void OnStateUpdate(float deltaTime)
        {
            base.OnStateUpdate(deltaTime);
            timeCount += deltaTime;
            if (timeCount >= 1)
            {
                timeCount = 0;
                Debug.Log("Update Idle...");
            }
        }
    }

    class TPrepareToIdle : Transition
    {
        SlimeComponentData data;
        public TPrepareToIdle(string name, Prepare prepare, Idle idle, SlimeComponentData data) : base(name, prepare, idle)
        {
            OnCheck += () => CheckCondition();
            prepare.AddTransition(this);
            this.data = data;
        }
        public bool CheckCondition()
        {
            return !data.mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle");
        }
    }
}
