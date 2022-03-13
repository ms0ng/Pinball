using Assets.AI.GreenSlime;
using MSFrame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSystem
{
    private static SlimeSystem instance;
    public static SlimeSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SlimeSystem();
            }
            return instance;
        }
    }

    internal void RemoveComponent(SlimeComponentData slimeComponentData)
    {
        throw new NotImplementedException();
    }

    internal void AddComponent(SlimeComponentData slimeComponentData)
    {
        throw new NotImplementedException();
    }

    public void InitComponent(SlimeComponentData monsterComponentData)
    {
        var data = (SlimeComponentData)monsterComponentData;
        data.FSM = new SlimeFSM(data);
        data.MaxHP = 100;
        data.HP = data.MaxHP;
    }

    public void UpdateComponent(SlimeComponentData monsterComponentData)
    {
        var data = (SlimeComponentData)monsterComponentData;
        data.FSM.Update();
    }

    public void OnAttack()
    {

    }
}
