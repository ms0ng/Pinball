using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public UIDialogController dialogController;
    public DialogData dialogData;
    public static DialogData dialogDataStatic;

    [Header("场地移动动画参数")]
    public Image mFiledImage;
    public Sprite mFieldSprite;
    public RectTransform mFieldImage;
    public float mStartPosY;
    public float mEndPosY;
    public float mMoveDuarion = 1.5f;

    [Header("黑幕")]
    public Image mBlackCurtain;

    // Start is called before the first frame update
    void Start()
    {
        if (dialogDataStatic) dialogData = dialogDataStatic;
        mFiledImage.sprite = mFieldSprite;
        AudioManager.Instance.Play("对话1");
        PlayAnimation_MoveField();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void PlayAnimation_MoveField()
    {
        mFieldImage.anchoredPosition = new Vector2(mFieldImage.anchoredPosition.x, mStartPosY);
        mFieldImage.DOAnchorPosY(mEndPosY, mMoveDuarion).onComplete += () =>
        {
            if (dialogData != null)
            {
                dialogController.OnDialogueComplete += OnDialogueComplete;
                dialogController.SetDataAndPlay(dialogData);
            }
        };
    }

    private void OnDialogueComplete()
    {
        mBlackCurtain.DOFade(1, 2).onComplete += () => SceneManager.LoadSceneAsync("Scenes/ChapterScene");
    }

    private void OnDestroy()
    {
        AudioManager.Instance.Stop("对话1");
    }
}
