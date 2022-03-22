using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using MSFrame;
using UnityEngine;
using UnityEngine.Pool;

public class HitNumManager : MonoSingleton<HitNumManager>
{
    public GameObject HitNumPrefab;
    public Transform HitNumSpawnParent;
    public float jumpDuration;
    public float keepDuration;
    public float YOffset;
    public float ZDepth;
    public AnimationCurve animationCurve;

    [Header("Debug")]
    public int PoolCount;
    public int PoolActiveCount;

    public ObjectPool<HitNumData> mHitNumPool;
    public List<HitNumData> mActives;

    void Start()
    {
        HitNumPool.Instance.Init(HitNumPrefab, HitNumSpawnParent);
        mHitNumPool = HitNumPool.Instance.mHitNumPool;
        mActives = HitNumPool.Instance.Actives;
    }

    private void Update()
    {
        if (mHitNumPool != null) PoolCount = mHitNumPool.CountActive;
        if (mHitNumPool != null) PoolActiveCount = mActives.Count;

        float dt = Time.deltaTime;
        for (int i = 0; i < mActives.Count; i++)
        {
            var data = mActives[i];
            if (!data.active) return;

            if (data.timeCount <= 0)
            {
                //初始化
                data.transform.localPosition = new Vector3(data.spawnPos.x, data.spawnPos.y, ZDepth);
                data.transform.DOLocalMoveY(data.transform.localPosition.y + YOffset, jumpDuration)
                    .SetEase(animationCurve);
            }
            if (data.timeCount < data.duration)
            {
                data.timeCount += dt;
            }
            else
            {
                //回收
                HitNumPool.Instance.mHitNumPool.Release(data);
                data.timeCount = 0;
                data.active = false;
            }
        }
    }


    [Button("增加1个伤害数字")]
    public void AddHitNum1(Vector2 screenPos, int shownNum, bool randomNum = false, bool randomPos = false)
    {
        shownNum = randomNum ? UnityEngine.Random.Range(0, 1) : shownNum;
        screenPos = randomPos ? new Vector2(UnityEngine.Random.Range(-3f, 3f), UnityEngine.Random.Range(-3f, 3f)) : screenPos;
        AddHitnumOne(screenPos, shownNum);
    }

    [Button("增加10个伤害数字")]
    public void AddHitNum10()
    {
        StartCoroutine(AddHitNums(10));
    }

    [Button("增加50个伤害数字")]
    public void AddHitNum50()
    {
        StartCoroutine(AddHitNums(50));
    }

    [Button("增加1000个伤害数字")]
    public void AddHitNum2500()
    {
        StartCoroutine(AddHitNums(1000));
    }

    private IEnumerator AddHitNums(int Count)
    {
        float t0 = Time.realtimeSinceStartup;
        while (Count > 0)
        {
            float x = UnityEngine.Random.Range(0, Screen.width);
            float y = UnityEngine.Random.Range(0, Screen.height);
            Vector2 randomPos = new Vector2(x, y);
            int shownNum = UnityEngine.Random.Range(0, 999);
            AddHitnumOne(randomPos, shownNum);
            Count--;
            yield return null;
        }
        //Debug.Log($"使用时间:{Time.realtimeSinceStartup - t0}");

    }

    public void AddHitnumOne(Vector2 screenPos, int shownNum)
    {
        //float t0 = Time.realtimeSinceStartup;
        var data = HitNumPool.Instance.mHitNumPool.Get();
        data.mHitNum.text = shownNum.ToString();
        data.duration = keepDuration;
        data.timeCount = 0;
        data.spawnPos = screenPos;
        data.active = true;
        Debug.Log("生成成功");

        //Debug.Log($"生成伤害数字使用所需时间: {Time.realtimeSinceStartup - t0}");
    }

}
