using MSFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectManager : MonoSingleton<HitEffectManager>
{
    public GameObject mHitEffectPrefab;
    public float mEffectStayTime = 0.5f;

    List<Effect> mHitEffects;

    class Effect
    {
        public GameObject obj;
        public float time;
    }

    // Start is called before the first frame update
    void Start()
    {
        mHitEffects = new();
    }

    private void Update()
    {
        for (int i = 0; i < mHitEffects.Count; i++)
        {
            Effect effect = mHitEffects[i];
            if (effect.time > mEffectStayTime)
            {
                Destroy(effect.obj);
                mHitEffects.RemoveAt(i);
                i--;
                continue;
            }
            effect.time += Time.deltaTime;
        }
    }

    public void Show(Vector3 pos)
    {
        var effect = Instantiate(mHitEffectPrefab);
        effect.transform.position = pos;
        effect.transform.parent = transform;
        mHitEffects.Add(new Effect { obj = effect });
    }
}
