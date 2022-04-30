using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MSFrame;
using MSFrame.FSM;

namespace Assets.AI.GreenSlime
{
    public class SlimeFSM
    {
        public SlimeComponentData _data;

        private StateMachine rootMachine;
        private StateMachine aliveMachine;

        //States:
        //Prepare prepare = new Prepare("prepare");
        Idle idle = new Idle("idle");
        State sDying = new State("sDying");

        public SlimeFSM(SlimeComponentData componentData)
        {
            _data = componentData;
            rootMachine = new StateMachine("root", idle);
            InitTransitions();
        }

        public void InitTransitions()
        {
            //Transition tPrepareToIdle = new Transition("tShoutToIdle", idle);
            //tPrepareToIdle.OnCheck += () =>
            //{
            //    if (_data.mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            //        return true;
            //    return false;
            //};
            //prepare.AddTransition(tPrepareToIdle);

            Transition tAnyToDying = new Transition("tA2D", sDying);
            tAnyToDying.OnCheck += () =>
            {
                if (_data.HP <= 0)
                {
                    return true;
                }
                return false;
            };
            idle.AddTransition(tAnyToDying);



            sDying.OnEnter += (prev) =>
            {
                Debug.Log("Enemy Slime Died!");
                _data.mAnimator.SetTrigger("Die");
            };
            sDying.OnUpdate += (dt) =>
            {

                if (_data.mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Dying") && _data.mAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
                {
                    GameObject.Destroy(_data.gameObject);
                }
            };
        }

        public void Update()
        {
            rootMachine.OnStateUpdate(Time.deltaTime);
        }
    }
}
