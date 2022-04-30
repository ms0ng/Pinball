using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MSFrame;

public class MonsterSystem<T> : Singleton<T>, IMonsterSystem where T : new()
{
    public bool Pause { get => _pause; }
    public List<MonsterComponentData> Components { get => _components; }

    private bool _pause;
    private List<MonsterComponentData> _components = new();

    public virtual void Start()
    {
        for (int i = 0; i < _components.Count; i++)
        {
            InitComponent(_components[i]);
        }
    }

    public virtual void Update()
    {
        for (int i = 0; i < _components.Count; i++)
        {
            var data = _components[i];
            if (data == null)
            {
                _components.RemoveAt(i);
                i--;
                continue;
            }
            else
            {
                UpdateComponent(data);
            }
        }
    }

    public virtual void InitComponent(MonsterComponentData monsterComponentData)
    {

    }

    public virtual void UpdateComponent(MonsterComponentData monsterComponentData)
    {

    }

    public void AddComponent(MonsterComponentData monsterComponent)
    {
        InitComponent(monsterComponent);
        _components.Add(monsterComponent);
        MonsterSystemManager.Instance.AddSystem(this);
    }

    public void RemoveComponent(MonsterComponentData monsterComponent)
    {
        _components.Remove(monsterComponent);
    }
}
