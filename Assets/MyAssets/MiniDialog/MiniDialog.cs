using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniDialog : MonoBehaviour
{
    public RectTransform mBackgroundImage;
    public TextMeshProUGUI mContent;

    [Header("参数")]
    public float mDuration = 0.25f;
    public float mPersistence = 3f;

    private Vector3 _oScale;

    public void Show(string content, Vector3 position = default)
    {
        Hide();
        transform.position = position;
        mContent.text = content;
        var seq = DOTween.Sequence();
        seq.Append(mBackgroundImage.DOScale(_oScale, mDuration).SetEase(Ease.OutBack))
            .AppendInterval(mPersistence)
            .onComplete += () =>
            {
                Hide();
                Destroy(gameObject);
            };
        seq.Play();
    }

    public void Hide()
    {
        mBackgroundImage.localScale = Vector3.zero;
    }

    private void Awake()
    {
        _oScale = mBackgroundImage.localScale;
        Hide();
    }

    private void OnEnable()
    {
#if UNITY_EDITOR
        Show($"测试文本测试文本测试文本测试文本测试文本测试文本");
#endif
    }
}
