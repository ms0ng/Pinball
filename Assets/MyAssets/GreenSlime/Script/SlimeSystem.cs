using Assets.AI.GreenSlime;
using MSFrame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSystem : MonsterSystem<SlimeSystem>
{
    public override void InitComponent(MonsterComponentData monsterComponentData)
    {
        base.InitComponent(monsterComponentData);
        InitComponent((SlimeComponentData)monsterComponentData);
    }
    public void InitComponent(SlimeComponentData monsterComponentData)
    {
        var data = (SlimeComponentData)monsterComponentData;
        data.FSM = new SlimeFSM(data);
        data.HP = data.MaxHP;
        data.HPBar.InitHPBar(data.MaxHP);
    }

    public override void UpdateComponent(MonsterComponentData monsterComponentData)
    {
        var data = (SlimeComponentData)monsterComponentData;
        data.FSM.Update();
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space)) GetHurt(data, UnityEngine.Random.Range(0, 100));
#endif
    }
    public void GetHurt(SlimeComponentData data, float damage)
    {
        data.HP -= damage;
        Vector3 randomPos = new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.3f, 0.3f));
        HitNumManager.Instance.AddHitnumOne(Manipulator.Instance.PlayerPos.transform.position + randomPos, (int)damage);
        data.HPBar.AddValue(-damage);
        Debug.Log($"Enemy Slime Get Damage : {data.HP}");
    }
}
