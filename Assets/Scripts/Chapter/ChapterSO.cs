using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialog/Create Chapter Sriptable Object")]
public class ChapterSO : ScriptableObject
{
    public string chapterName;
    public List<mission> missions;

    [System.Serializable]
    public class mission
    {
        public string missionName;
        public int missionIndex;
        public MissionType missionType;

        [Header("Dialogue Param")]
        public DialogData dialogData;
    }
}

public enum MissionType
{
    Battle,
    Dialogue,
}
