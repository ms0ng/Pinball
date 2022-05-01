using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ThemePageSceneLoader : MonoBehaviour
{
    public TextMeshProUGUI mTapText;
    public string mDefaultScene;
    public DialogData mDialogueData;

    public CanvasGroup mBlackCurtain;

    public void OnTapToStartBtnClick(string sceneName)
    {
        DialogManager.dialogDataStatic = mDialogueData;
        StartCoroutine(LoadScene(string.IsNullOrEmpty(sceneName) ? mDefaultScene : sceneName));
    }

    public IEnumerator LoadScene(string sceneName)
    {
        var op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        op.allowSceneActivation = false;
        while (op.progress < 0.9f)
        {
            if (mTapText) mTapText.text = $"正在载入...{ op.progress * 100 }% ";
            yield return null;
        }
        if (mTapText) mTapText.text = "载入完成";
        yield return new WaitForSeconds(1);
        mBlackCurtain.DOFade(1, 1).onComplete += () => op.allowSceneActivation = true;


    }
}
