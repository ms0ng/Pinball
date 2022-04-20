using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialog/Create Dialog Sriptable Object")]
public class DialogData : ScriptableObject
{
    public List<SpeakerDefine> mSpeakerDefine;
    public List<TextAsset> mTextAssets;
    public bool mLogDebug;

    /// <summary>
    /// 对话列表
    /// </summary>
    public List<Dialog> Dialogs { get => _dialogs; }
    private List<Dialog> _dialogs;

    [Serializable]
    public struct SpeakerDefine
    {
        public string name;
        public string shortcut;
        public Sprite sprite;
    }

    /// <summary>
    /// 每段对话包含的信息
    /// </summary>
    [Serializable]
    public struct Dialog
    {
        public string content;
        public string speaker;
        public string command;
    }

    public List<Dialog> LoadTextAsset()
    {
        _dialogs = new();
        Log($"支持多文件合并载入，总共需要载入 {mTextAssets.Count} 个文件.");
        foreach (var item in mTextAssets)
        {
            Log($"Load Text File: {item.name}");
            TextAssetHandle(item);
        }
        PrintDialogs();
        Log($"{mTextAssets.Count} 个文件载入完毕.");
        return _dialogs;
    }

    private void TextAssetHandle(TextAsset asset)
    {
        string text = asset.text;
        var strArray = text.Split('\n');
        for (int i = 0; i < strArray.Length; i++)
        {
            var t = strArray[i];
            if (string.IsNullOrEmpty(t) || string.IsNullOrWhiteSpace(t)) continue;
            switch (t[0])
            {
                case '@':
                    break;
                case '#':
                    break;
                case '\r':
                    break;
                default:
                    Dialog dialog = new Dialog();
                    var textSplit = t.Split(':');
                    if (textSplit.Length < 2)
                    {
                        Debug.LogWarning($"文本中存在错误的格式:\t{textSplit[0]}");
                        break;
                    }
                    for (int j = 0; j < textSplit.Length; j++)
                    {
                        if (j == 0)
                        {
                            //dialog.speaker = textSplit[0];
                            var speakerOrSpeakerShortcut = textSplit[0];
                            dialog.speaker = speakerOrSpeakerShortcut;
                            foreach (var item in mSpeakerDefine)
                            {
                                if (item.shortcut.Equals(speakerOrSpeakerShortcut))
                                {
                                    dialog.speaker = item.name;
                                }
                            }
                            continue;
                        }
                        dialog.content += textSplit[j];
                        if (j != textSplit.Length - 1) dialog.content += ':';
                    }
                    _dialogs.Add(dialog);
                    break;
            }
        }
    }

    public Sprite GetSpeakerSprite(string speakerName = null, string speakerShotcut = null)
    {
        Sprite sprite = null;
        for (int i = 0; i < mSpeakerDefine.Count; i++)
        {
            if (speakerName == mSpeakerDefine[i].name || speakerShotcut == mSpeakerDefine[i].shortcut)
            {
                sprite = mSpeakerDefine[i].sprite;
            }
        }
        return sprite;
    }

    public void PrintDialogs()
    {
        foreach (var item in _dialogs)
        {
            Log($"{item.speaker} : {item.content}");
        }
    }

    private void Log(string logMsg)
    {
        if (mLogDebug) Debug.Log(logMsg);
    }

    /// <summary>
    /// 该方法用于在Inspector面板快速添加至按钮事件
    /// </summary>
    public void 播放(UIDialogController dialogPrefab)
    {
        if (!dialogPrefab)
        {
            Debug.LogError($"无法播放：找不到对话框物体，检查按钮是否添加了对话框物体！");
        }
        dialogPrefab.SetDataAndPlay(this);
    }
}
