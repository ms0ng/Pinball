using System;
using UnityEngine;

namespace MEventCenter
{
    public class SimpleEventCenter : EventCenter<SimpleEventCenter, string, Action>
    {
        public override bool Fire(string key, params object[] args)
        {
            if (args.Length > 0)
            {
                Debug.LogWarning("You don't need to fire event with any parameter!");
            }
            Debug.Log($"Fire Event: {key}");
            return base.Fire(key);
        }
    }
}