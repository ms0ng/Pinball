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
    public Text ChapterIndex_1;
    public Text ChapterIndex;
    public Text Preconditions;
    public GameObject Lock;
    [Header("完成度")]
    public GameObject ProgressPercent;
    public Text ProgressPercentText;
    [Header("参数")]
    public int chapter;
    public string chapterName;

    private int _OpenSceneID;

    public void SetData(int chapter, string chapterName, Sprite sprite, bool setLock, int openSceneID)
    {
        _OpenSceneID = openSceneID;
        this.chapter = chapter;
        //设置文字
        ChapterName.text = $"{chapterName}";
        ChapterIndex_1.text = $"第{chapter}章";
        ChapterIndex.text = chapter.ToString("D2");
        Thumbnail.sprite = sprite;
        Preconditions.text = $"通关第{chapter - 1}章";
        Lock.SetActive(setLock);
        //计算完成度
        //float progress = 0f;
        //if (ProgressPercentText) ProgressPercentText.text = $"{progress * 100}%";
    }

    public void OnClick()
    {
        DOLoadScene(loadScene);
    }

    public void DOLoadScene(LoadScene loadScene)
    {
        string sceneName = "Scenes/StartScene";
        switch (loadScene)
        {
            case ChapterPanelItem.LoadScene.BattleScene:
                sceneName = "Scenes/BattleScene";
                break;
            case ChapterPanelItem.LoadScene.DialogueScene:
                sceneName = "Scenes/DialogueScene";
                break;
            default:
                break;
        }
        SceneManager.LoadSceneAsync(SceneManager.GetSceneByName(sceneName).buildIndex);
    }
}
