using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MSFrame;

public class MonsterComponentData : MonoBehaviour
{
    //public MonsterSystem<SystemType> System;
    //public void Start() => System.AddComponent(this);
    //public void OnDestroy() => System.RemoveComponent(this);


    [Header("基本属性")]
    public float MaxHP;
    public float HP;
}
