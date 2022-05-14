using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccelerationMove : MonoBehaviour
{
    [Header("移动目标(留空默认本身)")]
    public RectTransform mRectTransform;
    [Header("动画参数")]
    [Tooltip("步进")]
    [Range(0.01f, 0.99f)]
    public float MoveBackLerpT = 0.8f;
    [Tooltip("速度")]
    public float MoveBackSpeed = 5;
    [Tooltip("回正死区")]
    public float MoveBackDeadZone = 10;

    [Header("移动范围")]
    public float XMulty = 100;
    public float YMulty = 100;
    //public float ZMulty = 100;

    [Header("加速度传感器模拟")]
    [Range(-1, 1)]
    public float SimulationX = 0;
    [Range(-1, 1)]
    public float SimulationY = 0;
    //[Range(-1, 1)]
    //public float SimulationZ = 0;

    private Vector2 _target;
    private Vector2 _accelOffset = Vector2.zero;
    private float sqrMoveBackDeadZone;


    void Start()
    {
        sqrMoveBackDeadZone = MoveBackDeadZone * MoveBackDeadZone;
    }

    void Update()
    {
        Vector2 acc = Input.acceleration;
#if UNITY_EDITOR
        acc = new Vector2(SimulationX, SimulationY);
#endif
        _accelOffset = Vector2.Lerp(_accelOffset, acc, Time.deltaTime);//逐渐回正
        _target = acc - _accelOffset;
        _target.x *= XMulty;
        _target.y *= YMulty;
        DoMove(_target, mRectTransform);
    }

    private void DoMove(Vector2 target, RectTransform rectTransform)
    {
        if ((target - rectTransform.anchoredPosition).sqrMagnitude < sqrMoveBackDeadZone) return;
        //Debug.Log($"Moving to {target}");
        float lerpT = MoveBackLerpT * MoveBackSpeed * Time.deltaTime;
        rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, target, lerpT);
    }

    private IEnumerator RandomMove(RectTransform rectTransform, float randomRange = 50)
    {
        Vector2 offset = new Vector2(0, UnityEngine.Random.Range(0, randomRange));
        float lerpT = 0;
        while (lerpT < 1)
        {
            lerpT += Time.deltaTime;
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, offset, lerpT);
            yield return null;
        }
    }
}
