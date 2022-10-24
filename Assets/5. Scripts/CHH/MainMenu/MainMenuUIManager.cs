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

    // ���� ����
    public void BtnStartClick()
    {
        SceneManager.LoadScene(1);
    }

    // ����â ����
    public void BtnSettingClick()
    {
        settingUI.SetActive(true);
    }

    // ����â �ݱ�
    public void BtnSettingClose()
    {
        settingUI.SetActive(false);
    }

    // ��������
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
