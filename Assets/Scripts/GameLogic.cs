using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MSFrame;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public void Pause() => Time.timeScale = 0;

    public void Resume() => Time.timeScale = 1;

    public void ReloadGame()
    {
        Resume();
        SceneManager.LoadSceneAsync(0);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        return;
#endif
        Application.Quit();
    }

    public void OnPauseBtnClick()

    {
        if (Time.timeScale != 1) Resume();
        else Pause();
    }

}
