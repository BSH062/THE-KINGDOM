using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Variable

    // 싱글톤이 할당될 static 변수
    private static UIManager instance;

    // 외부에서 싱글톤 오브젝트를 가져올 때 사용할 프로퍼티
    public static UIManager Instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (instance == null) instance = FindObjectOfType<UIManager>();
            return instance;
        }
    }


    public Player player;               // 플레이어의 플레이어 컴포넌트
    public Quest playerQeust;           // 플레이어 퀘스트 컴포넌트
    public Shop shop;                   // 상점 컴포넌트

    public GameObject scenarioUI;       // 시나리오 UI

    public GameObject playerUI;         // 플레이어 UI
    public Text coinTxt;                // 코인 텍스트

    public GameObject shopUI;           // 상점 UI
    public Text shopCoinTxt;            // 상점에서 보이는 플레이어 코인 텍스트
    public Text shopMsgTxt;             // 상점 말풍선 텍스트
    public string[] shopMsgs;           // 상점 말풍선 대화 내용

    public GameObject inventoryUI;      // 인벤토리 UI
    public GameObject questNPCUI;       // 퀘스트 NPC UI
    public Text questNPCContentTxt;     // questNPCUI 안에 있는 퀘스트 내용 변경시 사용할 TextUI
    public string questKeyword;         // 퀘스트 키워드
    public int questTargetCount;        // 퀘스트 목표량
    public int questProgressCount;      // 퀘스트 진행량
    public string questNPCProgress;     // NPC 퀘스트 진행도 설정
    public string questNPCCompensation; // NPC 퀘스트 진행도 설정

    public GameObject questNPCResultUI; // 퀘스트 NPC 결과 UI창
    public Text questNPCResultTxt;      // 퀘스트 NPC 결과 텍스트

    public GameObject questPlayerUI;    // 퀘스트 수락시 진행중 퀘스트 UI
    public GameObject questBackBtn;     // 이전 퀘스트 버튼
    public GameObject questNextBtn;     // 다음 퀘스트 버튼
    public Text questContentTxt;        // 진행중인 퀘스트 내용
    public Text questProgressTxt;       // questPlayerUI의 questProgressTxt  퀘스트 진행도 ex) 0 / 3
    public Text questCompensationTxt;   // 퀘스트 보상
    public int nowQeustIndex;           // 현재 퀘스트 번호

    public GameObject questGiveupUI;    // 퀘스트 포기 UI

    public GameObject generalUI;        // 일반 NPC UI
    public Text generalContentTxt;      // 일반 NPC 대화 내용

    public GameObject doorOpenUI;       // 문 상호작용 UI

    public GameObject respawnUI;        // 리스폰 UI
    public Text respawnUITxt;


    // 싱글톤

    #endregion Variable

    #region Method

    public void QuestProgressUpdate(int index)
    {
        questProgressTxt.text = playerQeust.getQuestProgressCount(index).ToString() + " / "+ playerQeust.getQuestTargetCount(index).ToString();
    }

    /// <summary>
    /// 선택한 UI 창 열기 + 게임 일시정지
    /// </summary>
    /// <param name="uiName">캔버스명</param>
    public void BtnOpenUI(string uiName)
    {
        switch (uiName)
        {
            case "scenarioUI": scenarioUI.SetActive(true); break;
            case "playerUI": scenarioUI.SetActive(true); break;
            case "shopUI": 
                shopUI.SetActive(true);

                // 상점에 나오는 플레이어 소지금 업데이트
                shopCoinTxt.text = coinTxt.text;    
                break;
            case "inventoryUI": inventoryUI.SetActive(true); break;
            case "questNPCUI":
                // 퀘스트를 받았으면
                if (isQuestAccept())
                {
                    questNPCResultUI.SetActive(true);
                }
                // 퀘스트를 아직 수락안했으면
                else
                {
                    questNPCUI.SetActive(true);
                }
                break;
            case "questPlayerUI": questPlayerUI.SetActive(true); break;
            case "questGiveupUI": questGiveupUI.SetActive(true); break;
            case "generalUI": generalUI.SetActive(true); break;
            case "doorUI": doorOpenUI.SetActive(true); break;
            case "respawnUI": respawnUI.SetActive(true); break;
        }
        
        if(uiName != "inventoryUI" && uiName != "respawnUI" && uiName != "doorUI")
        {
            Time.timeScale = 0f;
        }
    }

    /// <summary>
    /// 선택한 UI 창 닫기 + 게임 재생
    /// </summary>
    /// <param name="uiName">캔버스명</param>
    public void BtnCloseUI(string uiName)
    {
        switch (uiName)
        {
            case "scenarioUI": scenarioUI.SetActive(false); break;
            case "playerUI": scenarioUI.SetActive(false); break;
            case "shopUI": shopUI.SetActive(false); break;
            case "inventoryUI": inventoryUI.SetActive(false); break;
            case "questNPCUI": questNPCUI.SetActive(false); break;
            case "questNPCResultUI": questNPCUI.SetActive(false); break;
            case "questPlayerUI": questPlayerUI.SetActive(false); break;
            case "questGiveupUI": questGiveupUI.SetActive(false); break;
            case "generalUI": generalUI.SetActive(false); break;
            case "doorUI": doorOpenUI.SetActive(false); break;
            case "respawnUI": respawnUI.SetActive(false); break;
        }
        Time.timeScale = 1f;
    }

    /// <summary>
    /// 이미 퀘스트를 수락했는지 확인
    /// </summary>
    /// <returns></returns>
    public bool isQuestAccept()
    {
        for (int i = 0; i < 3; i++)
        {
            // 이미 퀘스트를 받은 NPC라면
            if (playerQeust.getQuestKeyword(i) == questKeyword && playerQeust.getQeustAccept(i) ==true)
            {
                nowQeustIndex = i;
                questNPCResultTxt.text = playerQeust.getQuestCompensation(i);
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// NPC가 주는 퀘스트창에서 수락버튼 누르면 실행
    /// questPlayerUI의 퀘스트 내용 및 진행도를 NPC 퀘스트가 지정한 내용으로 변경 
    /// questNPCUI 닫기, questPlayerUI 열기
    /// </summary>
    public void BtnQuestAccept()
    {
        // 최대 퀘스트는 3개까지 수락가능
        if(playerQeust.questCount < 3)
        {
            Debug.Log("questTargetCount : " + questTargetCount);

            // 퀘스트 수락시 정보 전달
            playerQeust.AddQuest(playerQeust.questCount, questNPCContentTxt.text, questKeyword, questNPCCompensation, questNPCProgress, questTargetCount);

            // 퀘스트 1개 이상 일 경우 이전 퀘스트 버튼 활성화
            //if (playerQeust.questCount > 1) questBackBtn.SetActive(true);
        }

        // 현재 퀘스트 번호를 지금 수락한 퀘스트 번호로 설정
        nowQeustIndex = playerQeust.questCount;

        // 퀘스트 정보 입력 후 questNPCUI 끄고 questPlayerUI 켜기
        questContentTxt.text = questNPCContentTxt.text;
        questProgressTxt.text = questNPCProgress;
        questCompensationTxt.text = questNPCCompensation;
        BtnCloseUI("questNPCUI");
        BtnOpenUI("questPlayerUI");

        // 일시정지 해제
        Time.timeScale = 1f;
    }

    /// <summary>
    /// 퀘스트 중이라면 창만 꺼지고 퀘스트를 완료했으면 보상과 함께 창이 꺼짐
    /// </summary>
    public void BtnNPCQuestResult()
    {
        // 퀘스트 목표량과 진행도가 같거나 크면 퀘스트 성공, 보상 획득
        if (playerQeust.getQuestTargetCount(nowQeustIndex) <= playerQeust.getQuestProgressCount(nowQeustIndex)
            && playerQeust.getQeustAccept(nowQeustIndex) == true)
        {
            string compensation = playerQeust.getQuestCompensation(nowQeustIndex);

            // 보상이 돈이라면
            if (compensation.Contains("$"))
            {
                // 문자열 "$" 제거
                string reCompensation = compensation.Replace("$", string.Empty);
                // string -> int 자료형 변환
                int compensationCoin = Convert.ToInt32(reCompensation);

                // 플레이어에게 보상금 지급
                player.coin += compensationCoin;

                // 코인 UI 업데이트
                CoinUIUpdate(player.coin);
            }
            // 보상이 아이템이라면
            else
            {
                // 아이템명에 따라 지급
                switch (compensation)
                {
                    case "열쇠":
                        // 인벤토리에 열쇠 추가
                        EventSystem.current.currentSelectedGameObject.name = "Key_B";
                        shop.ItemCllick();
                        break;
                    case "칼":
                        // 인벤토리에 칼 추가
                        EventSystem.current.currentSelectedGameObject.name = "item_sword_long";
                        shop.ItemCllick();
                        break;
                }
            }

            // 완료된 퀘스트 지우기
            BtnGiveUpCheck();
        }

        questNPCResultUI.SetActive(false);
        Time.timeScale = 1f;
    }

    /// <summary>
    /// questPlayerUI의 퀘스트 포기버튼을 눌렀을 때 실행
    /// questGiveupUI 오픈
    /// </summary>
    public void BtnGiveUp()
    {
        BtnOpenUI("questGiveupUI");
        Time.timeScale = 0f;
    }

    /// <summary>
    /// questGiveupUI의 퀘스트 포기 확인 버튼을 눌렀을 때 실행
    /// questGiveupUI + questPlayerUI 닫기 및 퀘스트 내용 삭제
    /// </summary>
    public void BtnGiveUpCheck()
    {
        // 퀘스트 포기시 선택한 퀘스트 번호에 해당하는 퀘스트 정보를 플레이어 퀘스트 리스트에서 지우기
        playerQeust.DeleteQuest(nowQeustIndex);

        // 퀘스트가 하나도 없으면 UI 정보 전부 지우기
        if (playerQeust.questCount == 0)
        {
            questContentTxt.text = "";
            questProgressTxt.text = "";
            questCompensationTxt.text = "";
            BtnCloseUI("questPlayerUI");
        }
        // 이전 퀘스트 또는 다음 퀘스트가 남아 있으면 빠른 퀘스트 번호로 퀘스트 UI 정보 변경
        else 
        {
            // 퀘스트 1개 이상 일 경우 다음 퀘스트 버튼 활성화
            if (playerQeust.questCount-1 > 1)
            {
                questNextBtn.SetActive(true);

                // 이전 퀘스트 버튼 비활성화
                //questBackBtn.SetActive(false);

                for(int i = 0; i < playerQeust.questCount; i++)
                {
                    if(playerQeust.getQeustAccept(i) == false && playerQeust.getQeustAccept(i+1) == true)
                    {
                        playerQeust.UpdateQuest(i, playerQeust.getQuestContent(i + 1), playerQeust.getQuestKeyword(i + 1),
                            playerQeust.getQuestCompensation(i + 1), playerQeust.getQuestProgress(i + 1),
                            playerQeust.getQuestTargetCount(i + 1), playerQeust.getQuestProgressCount(i + 1));
                    }
                }

                nowQeustIndex = 0;
                questContentTxt.text = playerQeust.getQuestContent(0);
                questProgressTxt.text = playerQeust.getQuestProgress(0);
                questCompensationTxt.text = playerQeust.getQuestCompensation(0);
            }
        }

        // 퀘스트 포기 UI 닫기
        BtnCloseUI("questGiveupUI");

        // 일시정지 해제
        Time.timeScale = 1f;
    }

    /// <summary>
    /// 이전 퀘스트 버튼 클릭시 실행
    /// </summary>
    public void BtnBackQuest()
    {
        // 퀘스트 번호가 0보다 클 때만 이전 퀘스트 버튼 활성화
        if(nowQeustIndex > 0)
        {
            // 현재 퀘스트 번호--
            nowQeustIndex--;

            // 플레이어 퀘스트 UI 퀘스트 개수에 따라 버튼 활성화 관리
            //PlayerQuestButtonActive();

            // 퀘스트 UI 이전 퀘스트 정보로 변경
            PlayerQuestUIUpdate();
        }
    }

    /// <summary>
    /// 다음 퀘스트 버튼 클릭시 실행
    /// </summary>
    public void BtnNextQeust()
    {
        // 퀘스트 번호가 2보다 작을 때만 이전 퀘스트 버튼 활성화
        if (nowQeustIndex < playerQeust.questCount)
        {
            // 현재 퀘스트 번호--
            nowQeustIndex++;

            // 플레이어 퀘스트 UI 퀘스트 개수에 따라 버튼 활성화 관리
            //PlayerQuestButtonActive();

            // 퀘스트 UI 이전 퀘스트 정보로 변경
            PlayerQuestUIUpdate();
        }
    }

    /// <summary>
    /// 플레이어 퀘스트 UI 퀘스트 개수에 따라 버튼 활성화 관리
    /// </summary>
    public void PlayerQuestButtonActive()
    {
        // 퀘스트가 1개 일 때
        if (playerQeust.questCount == 1)
        {
            questBackBtn.SetActive(false);
            questNextBtn.SetActive(true);
        }
        // 퀘스트가 2개 일 때
        else if (playerQeust.questCount == 2)
        {
            questBackBtn.SetActive(true);
            questNextBtn.SetActive(true);
        }
        // 퀘스트가 3개 일 때
        else
        {
            questBackBtn.SetActive(true);
            questNextBtn.SetActive(false);
        }
    }

    /// <summary>
    /// 입력된 퀘스트 내용으로 QuestNPCUI 텍스트 변경 및 퀘스트 진행도 저장
    /// </summary>
    /// <param name="questContent"> 변경할 퀘스트 내용 </param>
    /// <param name="questTargetCount"> 변경할 퀘스트 목표량 </param>
    /// <param name="questProgress"> 변경할 퀘스트 진행도 </param>
    /// <param name="questProgress"> 퀘스트 보상 </param>
    public void QuestNPCContentUpdate(string questContent, string questKeyword, int questTargetCount, string questProgress, string questCompensation)
    {
        this.questNPCContentTxt.text = questContent;
        this.questKeyword = questKeyword;
        this.questTargetCount = questTargetCount;
        this.questNPCProgress = questProgress;
        this.questNPCCompensation = questCompensation;
    }

    /// <summary>
    /// PlayerQuestUI 퀘스트 정보 수정
    /// </summary>
    public void PlayerQuestUIUpdate()
    {
        questContentTxt.text = playerQeust.getQuestContent(nowQeustIndex);
        questKeyword = playerQeust.getQuestKeyword(nowQeustIndex);
        questCompensationTxt.text = playerQeust.getQuestCompensation(nowQeustIndex);
        questProgressTxt.text = playerQeust.getQuestProgress(nowQeustIndex);
        questTargetCount = playerQeust.getQuestTargetCount(nowQeustIndex);
        questProgressCount = playerQeust.getQuestProgressCount(nowQeustIndex);
    }

    /// <summary>
    /// 일반 NPC 대화 UI의 텍스트 변경
    /// </summary>
    /// <param name="content"> 대화 내용 </param>
    public void GeneralUIUpdate(string content)
    {
        generalContentTxt.text = content;
    }

    /// <summary>
    /// UI 코인 값 업데이트
    /// </summary>
    /// <param name="coin">수정될 코인 값</param>
    public void CoinUIUpdate(int coin)
    {
        coinTxt.text = coin.ToString();
        shopCoinTxt.text = coinTxt.text;
    }

    /// <summary>
    /// 아이템 가격보다 플레이어 소지금이 부족시 구매불가 메세지 5초간 띄움
    /// </summary>
    public void DontBuy()
    {
        StopCoroutine(DontBuyMsg());
        // 상점 아이템 구매 소지금 부족시 말풍선 변경
        StartCoroutine(DontBuyMsg());
    }

    /// <summary>
    /// 상점 아이템 구매 소지금 부족시 말풍선 변경
    /// </summary>
    /// <returns></returns>
    IEnumerator DontBuyMsg()
    {
        //금액부족 멘트 2초동안 띄우기
        shopMsgTxt.text = shopMsgs[1];
        yield return new WaitForSecondsRealtime(5f);

        // 5초뒤에 대화데이터로 변경
        shopMsgTxt.text = shopMsgs[0];
    }

    /// <summary>
    /// Player Respawn 충돌시 RespawnUI 텍스트 초기화
    /// </summary>
    public void RespawnUIReset()
    {
        respawnUITxt.text = "저장하시려면  'e'  버튼을 눌러주세요.";
    }

    /// <summary>
    /// RespawnUI 'e' 버튼을 눌러 저장시 텍스트 변경
    /// </summary>
    public void RespawnUIClick()
    {
        respawnUITxt.text = "저장 완료 되었습니다.";
    }

    #endregion Method
}
