﻿using SuperScrollView;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterPanel : MonoBehaviour
{
    public LoopListView2 mLoopListView;
    public ChapterSO mChapterData;
    private int CheckPoint;

    private void Start()
    {
        InitListView();
        AudioManager.Instance.Play("Theme", loop: true);
    }
    private void OnDestroy()
    {
        AudioManager.Instance.Stop("Theme");
    }

    public void InitListView()
    {
        mLoopListView.InitListView(mChapterData.missions.Count, OnGetItemByIndex);
        mLoopListView.SetSnapTargetItemIndex(0);
    }

    LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
    {
        if (index < 0 || index >= mChapterData.missions.Count)
        {
            return null;
        }
        LoopListViewItem2 item = listView.NewListViewItem("Chapter");
        item.gameObject.name = "Chapter" + index;
        ChapterPanelItem itemScript = item.GetComponent<ChapterPanelItem>();

        if (itemScript)
        {
            itemScript.SetData(mChapterData.missions[index], index + 1);
        }
        return item;
    }

    public void Refresh()
    {
        mLoopListView.RefreshAllShownItem();
        int index = CheckPoint - 1;
        mLoopListView.SetSnapTargetItemIndex(index);
    }
}
