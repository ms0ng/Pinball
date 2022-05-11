using System.Collections;
using UnityEngine;

/// <summary>
/// <para>Delete this file if you need.It's just an example.</para>
/// <para>此文件只是个示范,可以删除这个文件.</para>
/// </summary>
namespace MEventCenter.Example
{
    public class CustomEventCenter : EventCenter<CustomEventCenter, CustomEventID, FunctionWithParam>
    {
        public override bool Fire(CustomEventID eventID, params object[] args)
        {
            Debug.Log($"Fire Custom Event:{eventID}");
            if (args.Length > 0 && args[0].GetType().Equals(typeof(int)))
            {
                return base.Fire(eventID, args[0]);
            }
            else
            {
                Debug.LogError($"{GetType().Name}: Parameters are not fit");
                return false;
            }
        }

        public bool Fire(CustomEventID eventID, int number)
        {
            return Fire(eventID, number);
        }
    }
}