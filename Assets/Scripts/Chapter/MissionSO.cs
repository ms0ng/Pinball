using UnityEngine;

[CreateAssetMenu(menuName = "Chapter and Mission/Create Mission Sriptable Object")]
public class MissionSO : ScriptableObject
{
    public string missionName;
    public int missionIndex;
    public Sprite thumbnailSprite;
    public MissionType missionType;

    [Header("Dialogue Param")]
    public DialogData dialogData;
}
