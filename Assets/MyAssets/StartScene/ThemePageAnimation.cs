using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemePageAnimation : MonoBehaviour
{
    [Header("组件")]
    public RectTransform mBackground;
    public RectTransform mTitle;
    public RectTransform mCharacter;
    public RectTransform mTapText;
    public Button mSceneLoadBtn;

    [Header("背景动画参数")]
    public float mBGScaleDuration = 1f;
    public Ease mBGScaleEase = Ease.OutQuart;
    public float mBGInitialScale = 1.2f;
    public float mBGFinalScale = 1;

    [Header("角色动画参数")]
    public float mCharaScaleDuration = 1f;
    public Ease mCharaScaleEase = Ease.OutQuart;
    public float mCharaInitialScale = 1.2f;
    public float mCharaFinalScale = 1f;
    public float mCharaMoveDuration = 1f;
    public Ease mCharaMoveEase = Ease.OutQuart;
    public Vector3 mCharaInitialPos = Vector3.zero;
    public Vector3 mCharaFinalPos = Vector3.zero;

    [Header("标题动画参数")]
    public float mTitleMoveDuration = 1f;
    public Ease mTitleMoveEase = Ease.OutQuart;
    public Vector3 mTitleInitialPos = Vector3.zero;
    public Vector3 mTitleFinalPos = Vector3.zero;

    [Header("底部文字动画参数")]
    public float mTapTextMoveDuration = 1f;
    public Ease mTapTextMoveEase = Ease.OutQuart;
    public Vector3 mTapTextInitialPos = Vector3.zero;
    public Vector3 mTapTextFinalPos = Vector3.zero;

    [Header("角色浮动动画参数")]
    public List<RectTransform> mCharaterImages;
    public float mCharaFlotyOffset = 5;

    private void OnEnable()
    {
        mSceneLoadBtn.interactable = false;
        var seq = DOTween.Sequence();

        mBackground.localScale = Vector3.one * mBGInitialScale;
        seq.Append(mBackground.DOScale(mBGFinalScale, mBGScaleDuration).SetEase(mBGScaleEase));

        mCharacter.localScale = Vector3.one * mCharaInitialScale;
        seq.Join(mCharacter.DOScale(mCharaFinalScale, mCharaScaleDuration).SetEase(mCharaScaleEase));
        mCharacter.anchoredPosition = mCharaInitialPos;
        seq.Join(mCharacter.DOAnchorPos(mCharaFinalPos, mCharaMoveDuration).SetEase(mCharaMoveEase));

        mTitle.anchoredPosition = mTitleInitialPos;
        seq.Join(mTitle.DOAnchorPos(mTitleFinalPos, mTitleMoveDuration).SetEase(mTitleMoveEase));

        mTapText.anchoredPosition = mTapTextInitialPos;
        seq.Join(mTapText.DOAnchorPos(mTapTextFinalPos, mTapTextMoveDuration).SetEase(mTapTextMoveEase));

        seq.onComplete += () => mSceneLoadBtn.interactable = true;
        seq.Play();

        //浮动动画
        foreach (var item in mCharaterImages)
        {
            item.DOLocalPath(
                new Vector3[2] {
                     item.localPosition,
                     item.localPosition + new Vector3(0, mCharaFlotyOffset, 0),
                     //item.localPosition,
                     //item.localPosition + new Vector3(0, -mCharaFlotyOffset, 0)
                },
                Random.Range(2, 5))
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}
