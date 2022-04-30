using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MSFrame;

public class MonsterSystemManager : MonoSingleton<MonsterSystemManager>
{
    public List<IMonsterSystem> System { get => _systems; }
    private List<IMonsterSystem> _systems = new();

    public override void Awake()
    {
        base.Awake();
        //DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        for (int i = 0; i < _systems.Count; i++)
        {
            var system = _systems[i];
            system.Start();
        }
    }


    public void Update()
    {
        for (int i = 0; i < _systems.Count; i++)
        {
            var system = _systems[i];
            if (!system.Pause && system.Components.Count > 0) system.Update();
        }
    }

    public void AddSystem(IMonsterSystem system)
    {
        if (!_systems.Contains(system)) _systems.Add(system);
    }

    public void RemoveSystem(IMonsterSystem system)
    {
        if (_systems.Contains(system)) _systems.Remove(system);
    }
}
