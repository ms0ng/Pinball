using Assets.AI.GreenSlime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeComponentData : MonoBehaviour
{
    protected SlimeSystem System = SlimeSystem.Instance;
    public void Start() => System.AddComponent(this);
    public void OnDestroy() => System.RemoveComponent(this);


    [Header("基本属性")]
    public float MaxHP;
    public float HP;

    public Animator mAnimator;
    public SlimeFSM FSM;

    public void OnAnimationEventAttack() => System.OnAttack();
}
