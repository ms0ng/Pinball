using DG.Tweening;
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
    [Header("黑幕")]
    public CanvasGroup mBlackCurtain;

    public void SetData(MissionSO missionData, int index)
    {
        this.mMissionData = missionData;
        ChapterName.text = $"{missionData.missionName}";
        ChapterIndex_1.text = $"第{index}章";
        ChapterIndex.text = index.ToString("D2");
        Thumbnail.sprite = missionData.thumbnailSprite;
        //Preconditions.text = $"通关第{missionData.missionIndex - 1}章";
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
                sceneName = "Scenes/BattleScene/" + mMissionData.sceneName;
                break;
            case MissionType.Dialogue:
                DialogManager.dialogDataStatic = mMissionData.dialogData;
                sceneName = "Scenes/DialogueScene";
                break;
            default:
                break;
        }
        StartCoroutine(LoadScene(sceneName));
    }
    public IEnumerator LoadScene(string sceneName)
    {
        var op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        op.allowSceneActivation = false;
        mBlackCurtain.blocksRaycasts = true;
        mBlackCurtain.DOFade(1, 1);
        while (op.progress < 0.9f)
        {
            yield return null;
        }
        yield return new WaitForSeconds(1);
        op.allowSceneActivation = true;

    }
}
