using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class HitNumPool
{
    public GameObject HitNumPrefab;
    public Transform SpawnTransform;
    public ObjectPool<HitNumData> mHitNumPool;
    public List<HitNumData> Actives;

    private int _sortingLayerID = SortingLayer.NameToID("UI");

    private static HitNumPool _Instance;
    public static HitNumPool Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new HitNumPool();
            }
            return _Instance;
        }
    }


    public void Init(GameObject HitNumPrefab, Transform SpawnTransform)
    {
        //Debug.Log("HitNumPool Init");
        this.HitNumPrefab = HitNumPrefab;
        this.SpawnTransform = SpawnTransform;
        this.mHitNumPool = new ObjectPool<HitNumData>(CreatHitNum, OnGetHitNum, OnReleaseHitNum, OnDestroyHitNum, collectionCheck: true, defaultCapacity: 10, maxSize: 200);
        this.Actives = new List<HitNumData>();
    }

    #region mHitNumPool Methods

    private void OnDestroyHitNum(HitNumData obj)
    {
        GameObject.Destroy(obj);
    }

    private void OnReleaseHitNum(HitNumData obj)
    {
        obj.transform.localScale = Vector3.zero;
        Actives.Remove(obj);
    }

    private void OnGetHitNum(HitNumData obj)
    {
        obj.transform.gameObject.SetActive(true);
        obj.transform.localScale = Vector3.one;
        Actives.Add(obj);
    }

    private HitNumData CreatHitNum()
    {
        if (!HitNumPrefab)
        {
            Debug.LogError($"Error while generating hitnum prefab.");
            return null;
        }
        var obj = GameObject.Instantiate(HitNumPrefab, SpawnTransform).GetComponent<HitNumData>();
        obj.GetComponentInChildren<MeshRenderer>().sortingLayerID = _sortingLayerID;
        return obj;
    }
    #endregion

}
