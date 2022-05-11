using System.Collections;
using UnityEngine;

/// <summary>
/// <para>Delete this file if you need.It's just an example.</para>
/// <para>此文件只是个示范,可以删除这个文件.</para>
/// </summary>
namespace MEventCenter.Example
{
    public delegate void SimpleDelegate();//It's the same as using System.Action.Try use SimpleEventCenter.
    public delegate int ReturnAnInt();
    public delegate bool ReturnABoolean();
    public delegate void FunctionWithParam(int number);//Only this is used for CustomEventCenter.Delegates Above are just example;
}