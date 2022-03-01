using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIDialogController : MonoBehaviour
{
    public TextMeshProUGUI mContentText;

    public float mPrintWaitTime = 0.1f;
    public bool mAutoPlay = false;
    public float mAutoPlayWaitTime = 2f;

    private List<string> _dialogList;
    private int _dialogIndex;
    private WaitForSeconds _waitForPrint;
    private WaitForSeconds _waitForAutoPlay;
    private Coroutine _playingCoroutine;

    private void Awake()
    {
        _dialogList = new List<string>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetDialogAsset(TextAsset textAsset)
    {
        var textSplit = textAsset.text.Split('\n');
        _dialogList.Clear();
        for (int i = 0; i < textSplit.Length; i++)
        {
            _dialogList.Add(textSplit[i]);
        }
        _dialogIndex = 0;
    }

    public void Play()
    {
        if (_playingCoroutine == null)
            _playingCoroutine = StartCoroutine(PrintText());
        else
        {
            StopCoroutine(_playingCoroutine);
            _playingCoroutine = null;
            PrintTextAll();
            _dialogIndex++;
        }
    }

    public void PlayAt(int index)
    {
        StopAllCoroutines();
        _playingCoroutine = null;
        _dialogIndex = index;
        Play();
    }

    private IEnumerator PrintText()
    {
        if (mPrintWaitTime < 0) mPrintWaitTime = 0.1f;
        _waitForPrint = new WaitForSeconds(mPrintWaitTime);
        do
        {
            mContentText.text = string.Empty;
            string text = _dialogList.Count > _dialogIndex ? _dialogList[_dialogIndex] : "Text Error";
            for (int i = 0; i < text.Length; i++)
            {
                mContentText.text += text[i];
                yield return _waitForPrint;
            }
            _dialogIndex++;
            if (_dialogIndex == _dialogList.Count) break;
            yield return _waitForAutoPlay;
        } while (mAutoPlay);
        _playingCoroutine = null;
    }

    private void PrintTextAll()
    {
        mContentText.text = _dialogList[_dialogIndex];
    }
}
