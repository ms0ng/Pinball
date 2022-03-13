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
        Prepare prepare = new Prepare("prepare");
        Idle idle = new Idle("idle");

        public SlimeFSM(SlimeComponentData componentData)
        {
            _data = componentData;
            rootMachine = new StateMachine("root", prepare);

        }

        public void InitTransitions()
        {
            TPrepareToIdle prepareToIdle = new TPrepareToIdle("Prepare2Idle", prepare, idle, _data);
        }

        public void Update()
        {
            rootMachine.OnStateUpdate(Time.deltaTime);
        }
    }
}
