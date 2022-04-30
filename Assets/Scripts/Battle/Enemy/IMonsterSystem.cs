using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMonsterSystem
{
    bool Pause { get; }
    List<MonsterComponentData> Components { get; }

    void Start();
    void Update();
}

public interface IMonsterComponentData
{

}
