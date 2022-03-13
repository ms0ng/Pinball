using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MSFrame;

public class Manipulator : MonoSingleton<Manipulator>
{
    public HPBar PlayerHPBar;
    public Transform PlayerPos;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
