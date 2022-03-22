using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogController : MonoBehaviour
{
    public TextMeshProUGUI mSpeakerText;
    public TextMeshProUGUI mContentText;
    public Image mSpeakerL;
    public Image mSpeakerR;
    public Sprite mDefaultSprite;
    public float mSpeakerLOffsetX = 100f;
    public float mSpeakerROffsetX = -100f;

    public float mPrintInterval = 0.1f;
    public bool mAutoPlay = false;
    public float mAutoPlayWaitTime = 2f;

    public System.Action OnDialogueComplete;

    private Vector2 _speakerLPos;
    private Vector2 _speakerRPos;

    private DialogData _dialogData;
    private int _dialogIndex;
    private WaitForSeconds _printInterval;
    private WaitForSeconds _waitForAutoPlayNext;
    private Coroutine _playingCoroutine;
    private RectTransform _rectTransform;

    private string _speakerLName;
    private string _speakerRName;
    private bool _speakerRActive;

    private void Awake()
    {
        _rectTransform = (RectTransform)transform;
    }

    void Start()
    {
        Init();
    }

    private void OnDisable()
    {
        mSpeakerL.sprite = null;
        mSpeakerR.sprite = null;
        Resources.UnloadUnusedAssets();
    }

    /// <summary>
    /// 此方法会重置角色头像的位置，务必在移动动画播放完毕后调用
    /// </summary>
    private void Init()
    {
        _speakerLPos = ((RectTransform)mSpeakerL.transform).anchoredPosition;
        _speakerRPos = ((RectTransform)mSpeakerR.transform).anchoredPosition;

        mSpeakerR.transform.localScale = Vector3.zero;
        mSpeakerL.transform.localScale = Vector3.zero;

        mSpeakerL.sprite = mDefaultSprite;
        mSpeakerR.sprite = mDefaultSprite;

        mSpeakerText.text = string.Empty;
        mContentText.text = string.Empty;

        _speakerLName = null;
        _speakerRName = null;
        _speakerRActive = true;
    }

    public void Show()
    {
        _rectTransform.anchoredPosition = new Vector2(0, -_rectTransform.rect.height);
        _rectTransform.DOAnchorPosY(0, 0.5f).onComplete = () =>
        {
            Init();
            Play();
        };
    }

    public void Hide()
    {
        _rectTransform.DOAnchorPosY(-_rectTransform.rect.height, 0.5f).onComplete += () =>
        {
            OnDialogueComplete?.Invoke();
        };
    }

    public void SetDialogData(DialogData dialogData, bool playImmediately = false)
    {
        dialogData.LoadTextAsset();
        _dialogData = dialogData;
        _dialogIndex = 0;
        //初始化
        if (playImmediately) Show();
    }

    public void SetDataAndPlay(DialogData dialogData) => SetDialogData(dialogData, true);

    public void Play()
    {
        if (_dialogIndex >= _dialogData.Dialogs.Count)
        {
            Hide();
            return;
        }
        if (_playingCoroutine == null)
        {
            //----------
            SetSpeakerImage();
            //-----仅设置一边的头像-----
            //mSpeakerL.transform.localScale = Vector3.one;
            //SetSpeakerImageOneSideOnly(mSpeakerL);
            //----------
            PrintSpeakerName();
            _playingCoroutine = StartCoroutine(PrintText());
        }
        else
        {
            StopCoroutine(_playingCoroutine);
            _playingCoroutine = null;
            PrintTextAll();
            _dialogIndex++;
        }
    }

    public void PlayFrom(int index)
    {
        StopAllCoroutines();
        _playingCoroutine = null;
        _dialogIndex = index;
        Play();
    }

    private void SetSpeakerImage()
    {
        bool needPlayAnimation = false;
        string curSpeakerName = _dialogData.Dialogs[_dialogIndex].speaker;
        string lastSpeakerName = null;
        if (_dialogIndex > 0) lastSpeakerName = _dialogData.Dialogs[_dialogIndex - 1].speaker;
        needPlayAnimation = !curSpeakerName.Equals(lastSpeakerName);


        //更换角色头像
        if (!curSpeakerName.Equals(_speakerLName) && !curSpeakerName.Equals(_speakerRName))
        {
            Sprite curSpeakerSprite = _dialogData.GetSpeakerSprite(curSpeakerName);
            if (curSpeakerSprite == null)
            {
                Debug.LogWarning($"找不到角色头像,将使用默认头像");
                curSpeakerSprite = mDefaultSprite;
            }
            //上次说话的角色头像 在左边/在右边 的两种情况
            if (_speakerRActive)
            {
                //在右边,修改左边的头像
                mSpeakerL.sprite = curSpeakerSprite;
                _speakerLName = curSpeakerName;
            }
            else
            {
                //在左边,修改右边的头像
                mSpeakerR.sprite = curSpeakerSprite;
                _speakerRName = curSpeakerName;
            }
            needPlayAnimation = true;
        }

        if (needPlayAnimation)
        {
            RectTransform SpeakerL = (RectTransform)mSpeakerL.transform;
            RectTransform SpeakerR = (RectTransform)mSpeakerR.transform;
            if (_speakerRActive)
            {
                SpeakerL.SetAsLastSibling();
                mSpeakerL.color = Color.white;
                SpeakerL.localScale = Vector3.one;
                SpeakerL.DOAnchorPosX(_speakerLPos.x + mSpeakerLOffsetX, 0.5f).SetEase(Ease.OutBack);

                mSpeakerR.color = Color.grey;
                SpeakerR.localScale = Vector3.one * 0.8f;
                SpeakerR.DOAnchorPosX(_speakerRPos.x, 0.5f).SetEase(Ease.OutBack);
            }
            else
            {
                SpeakerR.SetAsLastSibling();
                mSpeakerR.color = Color.white;
                SpeakerR.localScale = Vector3.one;
                SpeakerR.DOAnchorPosX(_speakerRPos.x + mSpeakerROffsetX, 0.5f).SetEase(Ease.OutBack);

                mSpeakerL.color = Color.gray;
                SpeakerL.localScale = Vector3.one * 0.8f;
                SpeakerL.DOAnchorPosX(_speakerLPos.x, 0.5f).SetEase(Ease.OutBack);
            }
            _speakerRActive = !_speakerRActive;
        }
    }

    private void SetSpeakerImageOneSideOnly(Image image)
    {
        string curSpeakerName = _dialogData.Dialogs[_dialogIndex].speaker;
        Sprite curSpeakerSprite = _dialogData.GetSpeakerSprite(curSpeakerName);
        if (curSpeakerSprite == null)
        {
            Debug.LogWarning($"找不到角色头像,将使用默认头像");
            curSpeakerSprite = mDefaultSprite;
        }
        image.sprite = curSpeakerSprite;
    }

    private void PrintSpeakerName()
    {
        mSpeakerText.text = _dialogData.Dialogs[_dialogIndex].speaker;
    }

    private IEnumerator PrintText()
    {
        if (mPrintInterval < 0) mPrintInterval = 0.1f;
        _printInterval = new WaitForSeconds(mPrintInterval);
        _waitForAutoPlayNext = new WaitForSeconds(mAutoPlayWaitTime);
        do
        {
            mContentText.text = string.Empty;
            string text = _dialogData.Dialogs[_dialogIndex].content;
            for (int i = 0; i < text.Length; i++)
            {
                mContentText.text += text[i];
                yield return _printInterval;
            }
            _dialogIndex++;
            if (_dialogIndex == _dialogData.Dialogs.Count) break;
            if (!mAutoPlay) break;
            yield return _waitForAutoPlayNext;
        } while (mAutoPlay);
        _playingCoroutine = null;
    }

    private void PrintTextAll()
    {
        mContentText.text = _dialogData.Dialogs[_dialogIndex].content;
    }

    //public void TestCallDialogData(DialogData dialogData)
    //{
    //    SetDataAndPlay(dialogData);
    //    return;

    //    if (_dialogData == null)
    //    {
    //        //Debug.Log($"载入对话: {_dialogs[0].content}");
    //        SetDialogData(dialogData);
    //        return;
    //    }
    //    if (_dialogIndex == _dialogData.Dialogs.Count)
    //    {
    //        Debug.Log("文本已播放完毕,重新载入...");
    //        _dialogData = null;
    //        return;
    //    }
    //    else
    //    {
    //        Play();
    //    }
    //}
}