using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MSFrame;

public class TigerMasterSystem : MonoSingleton<TigerMasterSystem>
{
    public List<TigerMasterData> datas = new List<TigerMasterData>();

    public void AddAndInitData(TigerMasterData data)
    {
        data.MaxHP = 800;
        data.HP = data.MaxHP;
        data.BreakdownThreshold = 100;
        data.HPBar.InitHPBar(data.MaxHP);
        data.FSM = new TigerMasterFSM();
        data.FSM.Start(data);
        if (!datas.Contains(data)) datas.Add(data);
    }

    public void Start()
    {
        for (int i = 0; i < datas.Count; i++)
        {
            var data = datas[i];
            AddAndInitData(data);
        }
    }

    public void Update()
    {
        if (datas.Count <= 0) GameObject.Destroy(gameObject);
        for (int i = 0; i < datas.Count; i++)
        {
            var data = datas[i];
            if (data == null)
            {
                datas.RemoveAt(i);
                i--;
                continue;
            }
            GetHurt(data, UnityEngine.Random.Range(0, 100));
            UpdateFSM(data, Time.deltaTime);
        }
    }
    public void UpdateFSM(TigerMasterData data, float deltaTime)
    {
        if (!data.FSM.Pause) data.FSM.Update(deltaTime);
    }

    public void GetHurt(TigerMasterData data, float damage)
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        data.HP -= damage;
        data.BreakdownDamage += damage * 0.5f;
        data.HPBar.AddValue(-damage);
        Debug.Log($"Enemy Get Damage : {data.HP}");
    }

    public void Attack_Scratch(TigerMasterData data)
    {
        data.skill_Scratch.SetActive(true);
        var atkArea = data.skill_Scratch.GetComponent<BoxCollider2D>();
        if (atkArea.OverlapPoint(Manipulator.Instance.PlayerPos.position))
        {
            Debug.Log("Attak Player!");
        }
    }
    public void Attack_Scratch_End(TigerMasterData data)
    {
        data.skill_Scratch.SetActive(false);
    }
}
