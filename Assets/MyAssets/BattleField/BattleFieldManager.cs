using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleFieldManager : MonoBehaviour
{
    public List<MonstersList> BattleInfo;
    public Camera CameraOverride;
    public RectTransform mBravo;

    [Header("变量")]
    public int _curBattleField = 0;
    public bool onGameEnd;

    [System.Serializable]
    public class MonstersList
    {
        public string desc;
        public Transform CameraPosition;
        public Transform PlayerSpawnPosition;
        public List<GameObject> Monsters;
    }

    private void Awake()
    {
        BattleInfo.ForEach((ml) => ml.Monsters.ForEach((obj) => obj.SetActive(false)));
    }

    void Start()
    {
        GoNextBattle(_curBattleField);
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

    public void GoNextBattle(int fieldIndex)
    {
        if (fieldIndex >= BattleInfo.Count)
        {
            return;
        }
        Manipulator.Instance.PlayerPos.gameObject.SetActive(false);
        LookField(fieldIndex, () =>
        {
            SpawnMonsters(fieldIndex);
            SpawnPlayer(fieldIndex);
            Manipulator.Instance.PlayerPos.gameObject.SetActive(true);
        });

    }
    public void LookField(int index, System.Action onComplete = null)
    {
        Camera camera = Camera.current;
        if (CameraOverride) camera = CameraOverride;
        camera.transform.DOMoveY(BattleInfo[index].CameraPosition.position.y, 1).SetEase(Ease.OutQuart).onComplete = () => onComplete?.Invoke();
    }

    public void SpawnMonsters(int spawnListIndex)
    {
        List<GameObject> monsters = BattleInfo[spawnListIndex].Monsters;
        monsters.ForEach((m) => m.SetActive(true));
    }

    private void SpawnPlayer(int spawnListIndex)
    {
        Transform player = Manipulator.Instance.PlayerPos;
        player.position = BattleInfo[spawnListIndex].PlayerSpawnPosition.position;
    }

    public void CheckField()
    {
        if (_curBattleField >= BattleInfo.Count)
        {
            if (!onGameEnd) OnGameEnd();
            return;
        }
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
            GoNextBattle(++_curBattleField);
        }
    }

    public void OnGameEnd(bool victory = true)
    {
        onGameEnd = true;
        Manipulator.Instance.PlayerPos.gameObject.SetActive(false);
        mBravo.gameObject.SetActive(true);
        mBravo.localScale = Vector3.zero;
        mBravo.DOScale(Vector3.one, 0.75f).SetEase(Ease.OutExpo).onComplete += () =>
        {
            float t = 0;
            DOTween.To(() => t, x => t = x, 1, 1).onComplete += () => SceneManager.LoadSceneAsync(0);
        };
    }

}
