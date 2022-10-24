using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioUI : MonoBehaviour
{
    #region Variable

    public GameObject scenarioUI;
    public Text contentTxt;
    public Text btnTxt;
    public string[] strScenario;
    public int scenarioNum;

    #endregion Variable

    #region Unity Method

    private void Start()
    {
        scenarioNum = 0;
        contentTxt.text = strScenario[scenarioNum];
        Time.timeScale = 0f;
    }

    #endregion Unity Method

    #region Method

    public void BtnNext()
    {
        // ������ �ó������� �а� Ȯ���� ������ �ó����� UI ��Ȱ��ȭ
        if(scenarioNum == strScenario.Length - 1)
        {
            scenarioUI.SetActive(false);
            Time.timeScale = 1f;
            return;
        }

        scenarioNum++;

        // ���� �ó����� �ؽ�Ʈ�� ������ �̶�� ��ư�� �ؽ�Ʈ�� Check�� ��ȯ
        if(scenarioNum == strScenario.Length - 1)
        {
            scenarioNum = strScenario.Length - 1;
            btnTxt.text = "CHECK";
        }

        // ���� �ó����� ������ ����
        contentTxt.text = strScenario[scenarioNum];
    }

    public void BtnSkip()
    {
        scenarioUI.SetActive(false);
        Time.timeScale = 1f;
    }

    #endregion Method
}
