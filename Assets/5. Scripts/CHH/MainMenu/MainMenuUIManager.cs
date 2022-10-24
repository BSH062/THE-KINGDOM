using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    #region Variable

    public GameObject settingUI;

    #endregion Variable

    #region Method

    // 게임 시작
    public void BtnStartClick()
    {
        SceneManager.LoadScene(1);
    }

    // 설정창 열기
    public void BtnSettingClick()
    {
        settingUI.SetActive(true);
    }

    // 설정창 닫기
    public void BtnSettingClose()
    {
        settingUI.SetActive(false);
    }

    // 게임종료
    public void BtnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    #endregion Method
}
