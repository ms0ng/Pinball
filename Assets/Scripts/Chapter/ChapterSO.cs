using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Chapter and Mission/Create Chapter Sriptable Object")]
public class ChapterSO : ScriptableObject
{
    public string chapterName;
    public List<MissionSO> missions;
}