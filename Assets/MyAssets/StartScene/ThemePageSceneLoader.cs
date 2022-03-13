using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThemePageSceneLoader : MonoBehaviour
{
    public TextMeshProUGUI mTapText;
    public string mDefaultScene;
    public void OnTapToStartBtnClick(string sceneName)
    {
        mTapText.text = "正在载入...";
        StartCoroutine(LoadScene(string.IsNullOrEmpty(sceneName) ? mDefaultScene : sceneName));
    }

    public IEnumerator LoadScene(string sceneName)
    {
        yield return null;
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }
}
