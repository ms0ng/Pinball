using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillShow : MonoBehaviour
{
    public Image mMainCharacter;
    public TextMeshProUGUI mSkillText;
    public List<Image> Backgrounds;

    [Header("参数设置")]
    public float mDuration = 0.25f;
    public Vector2 mInitPosition = new Vector2(300, 0);
    public AnimationCurve mAnimationCurve;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = (RectTransform)transform;
    }
    // Start is called before the first frame update
    private void OnEnable()
    {

    }

    void Update()
    {

    }

    public void Show(Sprite mainCharacter, string skillName)
    {
        rectTransform.anchoredPosition = mInitPosition;
        foreach (var item in Backgrounds)
        {
            item.fillAmount = 0;
        }
        rectTransform.localScale = Vector3.one;


        mMainCharacter.sprite = mainCharacter;
        mSkillText.text = skillName;
        var seq = DOTween.Sequence();
        seq.Append(rectTransform.DOAnchorPosX(0, mDuration).SetEase(mAnimationCurve))
            .Join(
            DOTween.To(() => Backgrounds[0].fillAmount, x =>
            {
                foreach (var item in Backgrounds)
                {
                    item.fillAmount = x;
                }
            }, 1, mDuration).SetEase(mAnimationCurve)
            )
            //.AppendInterval(1)
            .Append(rectTransform.DOScale(Vector3.one * 1.2f, 0.1f))
            .onComplete += () =>
            {
                Hide();
            };
        seq.Play();

    }

    public void Hide()
    {
        rectTransform.localScale = Vector3.zero;
        rectTransform.anchoredPosition = mInitPosition;
    }
}
