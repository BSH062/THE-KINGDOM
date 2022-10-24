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
        // 마지막 시나리오를 읽고 확인을 누르면 시나리오 UI 비활성화
        if(scenarioNum == strScenario.Length - 1)
        {
            scenarioUI.SetActive(false);
            Time.timeScale = 1f;
            return;
        }

        scenarioNum++;

        // 현재 시나리오 텍스트가 마지막 이라면 버튼의 텍스트를 Check로 변환
        if(scenarioNum == strScenario.Length - 1)
        {
            scenarioNum = strScenario.Length - 1;
            btnTxt.text = "CHECK";
        }

        // 다음 시나리오 문구로 변경
        contentTxt.text = strScenario[scenarioNum];
    }

    public void BtnSkip()
    {
        scenarioUI.SetActive(false);
        Time.timeScale = 1f;
    }

    #endregion Method
}
