using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChapterPanelItem : MonoBehaviour
{
    public Text ChapterName;
    public Image Thumbnail;
    public Button mStartBtn;
    public Text ChapterIndex_1;
    public Text ChapterIndex;
    public Text Preconditions;
    public GameObject Lock;
    [Header("完成度")]
    public GameObject ProgressPercent;
    public Text ProgressPercentText;
    [Header("参数")]
    MissionSO mMissionData;

    public void SetData(MissionSO missionData)
    {
        this.mMissionData = missionData;
        ChapterName.text = $"{missionData.missionName}";
        ChapterIndex_1.text = $"第{missionData.missionIndex}章";
        ChapterIndex.text = missionData.missionIndex.ToString("D2");
        Thumbnail.sprite = missionData.thumbnailSprite;
        Preconditions.text = $"通关第{missionData.missionIndex - 1}章";
        Lock.SetActive(false);
        //计算完成度
        //float progress = 0f;
        //if (ProgressPercentText) ProgressPercentText.text = $"{progress * 100}%";

        mStartBtn.onClick.RemoveAllListeners();
        mStartBtn.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        string sceneName = "Scenes/StartScene";
        string scenePath = "Scenes/StartScene";
        switch (mMissionData.missionType)
        {
            case MissionType.Battle:
                sceneName = "Scenes/BattleScene";
                break;
            case MissionType.Dialogue:
                DialogManager.dialogDataStatic = mMissionData.dialogData;
                sceneName = "Scenes/DialogueScene";
                break;
            default:
                break;
        }
        SceneManager.LoadSceneAsync(sceneName);
    }
}
