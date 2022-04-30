using Assets.AI.GreenSlime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeComponentData : MonsterComponentData
{
    protected SlimeSystem System = SlimeSystem.Instance;
    public void Start() => System.AddComponent(this);
    public void OnDestroy() => System.RemoveComponent(this);

    public Animator mAnimator;
    public SlimeFSM FSM;
    public HPBar HPBar;

    public void GetHurt(float damage) => System.GetHurt(this, damage);
}
