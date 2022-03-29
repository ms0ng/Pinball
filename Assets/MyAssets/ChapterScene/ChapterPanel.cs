using SuperScrollView;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterPanel : MonoBehaviour
{
    public LoopListView2 mLoopListView;
    private int ListCount = 3;
    private int CheckPoint;

    private void Start()
    {
        InitListView();
    }

    public void InitListView()
    {
        mLoopListView.InitListView(ListCount, OnGetItemByIndex);
        mLoopListView.SetSnapTargetItemIndex(0);
    }

    LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
    {
        if (index < 0 || index >= ListCount)
        {
            return null;
        }
        LoopListViewItem2 item = listView.NewListViewItem("Chapter");
        item.gameObject.name = "Chapter" + index;
        ChapterPanelItem itemScript = item.GetComponent<ChapterPanelItem>();

        if (itemScript)
        {
            itemScript.SetData(index, $"第{index}章", null, true, 1);
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
