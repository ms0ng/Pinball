using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFieldManager : MonoBehaviour
{
    public List<MonstersList> BattleInfo;
    public Camera CameraOverride;

    [Header("变量")]
    public int _curBattleField = 0;

    [System.Serializable]
    public class MonstersList
    {
        public string desc;
        public Transform CameraPosition;
        public List<GameObject> Monsters;
    }

    private void Awake()
    {
        BattleInfo.ForEach((ml) => ml.Monsters.ForEach((obj) => obj.SetActive(false)));
    }

    void Start()
    {
        GoField(_curBattleField);
    }

    float testTimeCount = 0;
    bool goSecond;
    void Update()
    {
        if (!goSecond) testTimeCount += Time.deltaTime;
        if (!goSecond && testTimeCount > 1)
        {
            //GoField(++_curBattleField);
            //goSecond = true;
            testTimeCount = 0;
            CheckField();
        }
    }

    public void GoField(int fieldIndex)
    {
        if (fieldIndex >= BattleInfo.Count) return;
        LookField(fieldIndex, () => SpawnMonsters(fieldIndex));

    }
    public void LookField(int index, System.Action onComplete = null)
    {
        Camera camera = Camera.current;
        if (CameraOverride) camera = CameraOverride;
        //camera.transform.position = BattleInfo[index].CameraPosition.position;
        camera.transform.DOMoveY(BattleInfo[index].CameraPosition.position.y, 1).SetEase(Ease.OutQuart).onComplete = () => onComplete?.Invoke();
    }

    public void SpawnMonsters(int spawnListIndex)
    {
        List<GameObject> monsters = BattleInfo[spawnListIndex].Monsters;
        monsters.ForEach((m) => m.SetActive(true));
    }

    public void CheckField()
    {
        var monsters = BattleInfo[_curBattleField].Monsters;
        for (int i = 0; i < monsters.Count; i++)
        {
            if (monsters[i] == null)
            {
                monsters.RemoveAt(i);
                i--;
                continue;
            }
        }
        if (monsters.Count < 1 && _curBattleField < BattleInfo.Count)
        {
            GoField(++_curBattleField);
        }
    }

}
