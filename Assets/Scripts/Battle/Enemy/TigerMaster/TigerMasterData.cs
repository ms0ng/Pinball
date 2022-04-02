using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerMasterData : MonoBehaviour
{
    public float HP;
    public float MaxHP;
    [Header("组件")]
    public Animator animator;
    public GameObject skill_Scratch;
    public HPBar HPBar;

    public TigerMasterFSM FSM;

    public short BreakdownTimes;
    public float BreakdownThreshold;
    public float BreakdownDamage;
    public string NextAttack;
    public float NextAttackCountdown;

    public string currentState;

    private void Awake() => TigerMasterSystem.Instance.AddAndInitData(this);

    private void OnDestroy() => TigerMasterSystem.Instance.datas.Remove(this);

    public void Attack_Scratch() => TigerMasterSystem.Instance.Attack_Scratch(this);

    public void Attack_Scratch_End() => TigerMasterSystem.Instance.Attack_Scratch_End(this);
}
