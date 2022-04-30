using UnityEngine;

[CreateAssetMenu(menuName = "Chapter and Mission/Create Mission Sriptable Object")]
public class MissionSO : ScriptableObject
{
    public string missionName;      //关卡名称
    //public int missionIndex;        //关卡索引
    public Sprite thumbnailSprite;  //关卡图片
    public MissionType missionType; //关卡类型:战斗或剧情

    [Header("Battle Param")]
    public string sceneName;
    [Header("Dialogue Param")]
    public DialogData dialogData;   //剧情关卡的对话信息
}
