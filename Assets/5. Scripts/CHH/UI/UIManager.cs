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

    // �̱����� �Ҵ�� static ����
    private static UIManager instance;

    // �ܺο��� �̱��� ������Ʈ�� ������ �� ����� ������Ƽ
    public static UIManager Instance
    {
        get
        {
            // ���� �̱��� ������ ���� ������Ʈ�� �Ҵ���� �ʾҴٸ�
            if (instance == null) instance = FindObjectOfType<UIManager>();
            return instance;
        }
    }


    public Player player;               // �÷��̾��� �÷��̾� ������Ʈ
    public Quest playerQeust;           // �÷��̾� ����Ʈ ������Ʈ
    public Shop shop;                   // ���� ������Ʈ

    public GameObject scenarioUI;       // �ó����� UI

    public GameObject playerUI;         // �÷��̾� UI
    public Text coinTxt;                // ���� �ؽ�Ʈ

    public GameObject shopUI;           // ���� UI
    public Text shopCoinTxt;            // �������� ���̴� �÷��̾� ���� �ؽ�Ʈ
    public Text shopMsgTxt;             // ���� ��ǳ�� �ؽ�Ʈ
    public string[] shopMsgs;           // ���� ��ǳ�� ��ȭ ����

    public GameObject inventoryUI;      // �κ��丮 UI
    public GameObject questNPCUI;       // ����Ʈ NPC UI
    public Text questNPCContentTxt;     // questNPCUI �ȿ� �ִ� ����Ʈ ���� ����� ����� TextUI
    public string questKeyword;         // ����Ʈ Ű����
    public int questTargetCount;        // ����Ʈ ��ǥ��
    public int questProgressCount;      // ����Ʈ ���෮
    public string questNPCProgress;     // NPC ����Ʈ ���൵ ����
    public string questNPCCompensation; // NPC ����Ʈ ���൵ ����

    public GameObject questNPCResultUI; // ����Ʈ NPC ��� UIâ
    public Text questNPCResultTxt;      // ����Ʈ NPC ��� �ؽ�Ʈ

    public GameObject questPlayerUI;    // ����Ʈ ������ ������ ����Ʈ UI
    public GameObject questBackBtn;     // ���� ����Ʈ ��ư
    public GameObject questNextBtn;     // ���� ����Ʈ ��ư
    public Text questContentTxt;        // �������� ����Ʈ ����
    public Text questProgressTxt;       // questPlayerUI�� questProgressTxt  ����Ʈ ���൵ ex) 0 / 3
    public Text questCompensationTxt;   // ����Ʈ ����
    public int nowQeustIndex;           // ���� ����Ʈ ��ȣ

    public GameObject questGiveupUI;    // ����Ʈ ���� UI

    public GameObject generalUI;        // �Ϲ� NPC UI
    public Text generalContentTxt;      // �Ϲ� NPC ��ȭ ����

    public GameObject doorOpenUI;       // �� ��ȣ�ۿ� UI

    public GameObject respawnUI;        // ������ UI
    public Text respawnUITxt;


    // �̱���

    #endregion Variable

    #region Method

    public void QuestProgressUpdate(int index)
    {
        questProgressTxt.text = playerQeust.getQuestProgressCount(index).ToString() + " / "+ playerQeust.getQuestTargetCount(index).ToString();
    }

    /// <summary>
    /// ������ UI â ���� + ���� �Ͻ�����
    /// </summary>
    /// <param name="uiName">ĵ������</param>
    public void BtnOpenUI(string uiName)
    {
        switch (uiName)
        {
            case "scenarioUI": scenarioUI.SetActive(true); break;
            case "playerUI": scenarioUI.SetActive(true); break;
            case "shopUI": 
                shopUI.SetActive(true);

                // ������ ������ �÷��̾� ������ ������Ʈ
                shopCoinTxt.text = coinTxt.text;    
                break;
            case "inventoryUI": inventoryUI.SetActive(true); break;
            case "questNPCUI":
                // ����Ʈ�� �޾�����
                if (isQuestAccept())
                {
                    questNPCResultUI.SetActive(true);
                }
                // ����Ʈ�� ���� ������������
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
    /// ������ UI â �ݱ� + ���� ���
    /// </summary>
    /// <param name="uiName">ĵ������</param>
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
    /// �̹� ����Ʈ�� �����ߴ��� Ȯ��
    /// </summary>
    /// <returns></returns>
    public bool isQuestAccept()
    {
        for (int i = 0; i < 3; i++)
        {
            // �̹� ����Ʈ�� ���� NPC���
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
    /// NPC�� �ִ� ����Ʈâ���� ������ư ������ ����
    /// questPlayerUI�� ����Ʈ ���� �� ���൵�� NPC ����Ʈ�� ������ �������� ���� 
    /// questNPCUI �ݱ�, questPlayerUI ����
    /// </summary>
    public void BtnQuestAccept()
    {
        // �ִ� ����Ʈ�� 3������ ��������
        if(playerQeust.questCount < 3)
        {
            Debug.Log("questTargetCount : " + questTargetCount);

            // ����Ʈ ������ ���� ����
            playerQeust.AddQuest(playerQeust.questCount, questNPCContentTxt.text, questKeyword, questNPCCompensation, questNPCProgress, questTargetCount);

            // ����Ʈ 1�� �̻� �� ��� ���� ����Ʈ ��ư Ȱ��ȭ
            //if (playerQeust.questCount > 1) questBackBtn.SetActive(true);
        }

        // ���� ����Ʈ ��ȣ�� ���� ������ ����Ʈ ��ȣ�� ����
        nowQeustIndex = playerQeust.questCount;

        // ����Ʈ ���� �Է� �� questNPCUI ���� questPlayerUI �ѱ�
        questContentTxt.text = questNPCContentTxt.text;
        questProgressTxt.text = questNPCProgress;
        questCompensationTxt.text = questNPCCompensation;
        BtnCloseUI("questNPCUI");
        BtnOpenUI("questPlayerUI");

        // �Ͻ����� ����
        Time.timeScale = 1f;
    }

    /// <summary>
    /// ����Ʈ ���̶�� â�� ������ ����Ʈ�� �Ϸ������� ����� �Բ� â�� ����
    /// </summary>
    public void BtnNPCQuestResult()
    {
        // ����Ʈ ��ǥ���� ���൵�� ���ų� ũ�� ����Ʈ ����, ���� ȹ��
        if (playerQeust.getQuestTargetCount(nowQeustIndex) <= playerQeust.getQuestProgressCount(nowQeustIndex)
            && playerQeust.getQeustAccept(nowQeustIndex) == true)
        {
            string compensation = playerQeust.getQuestCompensation(nowQeustIndex);

            // ������ ���̶��
            if (compensation.Contains("$"))
            {
                // ���ڿ� "$" ����
                string reCompensation = compensation.Replace("$", string.Empty);
                // string -> int �ڷ��� ��ȯ
                int compensationCoin = Convert.ToInt32(reCompensation);

                // �÷��̾�� ����� ����
                player.coin += compensationCoin;

                // ���� UI ������Ʈ
                CoinUIUpdate(player.coin);
            }
            // ������ �������̶��
            else
            {
                // �����۸� ���� ����
                switch (compensation)
                {
                    case "����":
                        // �κ��丮�� ���� �߰�
                        EventSystem.current.currentSelectedGameObject.name = "Key_B";
                        shop.ItemCllick();
                        break;
                    case "Į":
                        // �κ��丮�� Į �߰�
                        EventSystem.current.currentSelectedGameObject.name = "item_sword_long";
                        shop.ItemCllick();
                        break;
                }
            }

            // �Ϸ�� ����Ʈ �����
            BtnGiveUpCheck();
        }

        questNPCResultUI.SetActive(false);
        Time.timeScale = 1f;
    }

    /// <summary>
    /// questPlayerUI�� ����Ʈ �����ư�� ������ �� ����
    /// questGiveupUI ����
    /// </summary>
    public void BtnGiveUp()
    {
        BtnOpenUI("questGiveupUI");
        Time.timeScale = 0f;
    }

    /// <summary>
    /// questGiveupUI�� ����Ʈ ���� Ȯ�� ��ư�� ������ �� ����
    /// questGiveupUI + questPlayerUI �ݱ� �� ����Ʈ ���� ����
    /// </summary>
    public void BtnGiveUpCheck()
    {
        // ����Ʈ ����� ������ ����Ʈ ��ȣ�� �ش��ϴ� ����Ʈ ������ �÷��̾� ����Ʈ ����Ʈ���� �����
        playerQeust.DeleteQuest(nowQeustIndex);

        // ����Ʈ�� �ϳ��� ������ UI ���� ���� �����
        if (playerQeust.questCount == 0)
        {
            questContentTxt.text = "";
            questProgressTxt.text = "";
            questCompensationTxt.text = "";
            BtnCloseUI("questPlayerUI");
        }
        // ���� ����Ʈ �Ǵ� ���� ����Ʈ�� ���� ������ ���� ����Ʈ ��ȣ�� ����Ʈ UI ���� ����
        else 
        {
            // ����Ʈ 1�� �̻� �� ��� ���� ����Ʈ ��ư Ȱ��ȭ
            if (playerQeust.questCount-1 > 1)
            {
                questNextBtn.SetActive(true);

                // ���� ����Ʈ ��ư ��Ȱ��ȭ
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

        // ����Ʈ ���� UI �ݱ�
        BtnCloseUI("questGiveupUI");

        // �Ͻ����� ����
        Time.timeScale = 1f;
    }

    /// <summary>
    /// ���� ����Ʈ ��ư Ŭ���� ����
    /// </summary>
    public void BtnBackQuest()
    {
        // ����Ʈ ��ȣ�� 0���� Ŭ ���� ���� ����Ʈ ��ư Ȱ��ȭ
        if(nowQeustIndex > 0)
        {
            // ���� ����Ʈ ��ȣ--
            nowQeustIndex--;

            // �÷��̾� ����Ʈ UI ����Ʈ ������ ���� ��ư Ȱ��ȭ ����
            //PlayerQuestButtonActive();

            // ����Ʈ UI ���� ����Ʈ ������ ����
            PlayerQuestUIUpdate();
        }
    }

    /// <summary>
    /// ���� ����Ʈ ��ư Ŭ���� ����
    /// </summary>
    public void BtnNextQeust()
    {
        // ����Ʈ ��ȣ�� 2���� ���� ���� ���� ����Ʈ ��ư Ȱ��ȭ
        if (nowQeustIndex < playerQeust.questCount)
        {
            // ���� ����Ʈ ��ȣ--
            nowQeustIndex++;

            // �÷��̾� ����Ʈ UI ����Ʈ ������ ���� ��ư Ȱ��ȭ ����
            //PlayerQuestButtonActive();

            // ����Ʈ UI ���� ����Ʈ ������ ����
            PlayerQuestUIUpdate();
        }
    }

    /// <summary>
    /// �÷��̾� ����Ʈ UI ����Ʈ ������ ���� ��ư Ȱ��ȭ ����
    /// </summary>
    public void PlayerQuestButtonActive()
    {
        // ����Ʈ�� 1�� �� ��
        if (playerQeust.questCount == 1)
        {
            questBackBtn.SetActive(false);
            questNextBtn.SetActive(true);
        }
        // ����Ʈ�� 2�� �� ��
        else if (playerQeust.questCount == 2)
        {
            questBackBtn.SetActive(true);
            questNextBtn.SetActive(true);
        }
        // ����Ʈ�� 3�� �� ��
        else
        {
            questBackBtn.SetActive(true);
            questNextBtn.SetActive(false);
        }
    }

    /// <summary>
    /// �Էµ� ����Ʈ �������� QuestNPCUI �ؽ�Ʈ ���� �� ����Ʈ ���൵ ����
    /// </summary>
    /// <param name="questContent"> ������ ����Ʈ ���� </param>
    /// <param name="questTargetCount"> ������ ����Ʈ ��ǥ�� </param>
    /// <param name="questProgress"> ������ ����Ʈ ���൵ </param>
    /// <param name="questProgress"> ����Ʈ ���� </param>
    public void QuestNPCContentUpdate(string questContent, string questKeyword, int questTargetCount, string questProgress, string questCompensation)
    {
        this.questNPCContentTxt.text = questContent;
        this.questKeyword = questKeyword;
        this.questTargetCount = questTargetCount;
        this.questNPCProgress = questProgress;
        this.questNPCCompensation = questCompensation;
    }

    /// <summary>
    /// PlayerQuestUI ����Ʈ ���� ����
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
    /// �Ϲ� NPC ��ȭ UI�� �ؽ�Ʈ ����
    /// </summary>
    /// <param name="content"> ��ȭ ���� </param>
    public void GeneralUIUpdate(string content)
    {
        generalContentTxt.text = content;
    }

    /// <summary>
    /// UI ���� �� ������Ʈ
    /// </summary>
    /// <param name="coin">������ ���� ��</param>
    public void CoinUIUpdate(int coin)
    {
        coinTxt.text = coin.ToString();
        shopCoinTxt.text = coinTxt.text;
    }

    /// <summary>
    /// ������ ���ݺ��� �÷��̾� �������� ������ ���źҰ� �޼��� 5�ʰ� ���
    /// </summary>
    public void DontBuy()
    {
        StopCoroutine(DontBuyMsg());
        // ���� ������ ���� ������ ������ ��ǳ�� ����
        StartCoroutine(DontBuyMsg());
    }

    /// <summary>
    /// ���� ������ ���� ������ ������ ��ǳ�� ����
    /// </summary>
    /// <returns></returns>
    IEnumerator DontBuyMsg()
    {
        //�ݾ׺��� ��Ʈ 2�ʵ��� ����
        shopMsgTxt.text = shopMsgs[1];
        yield return new WaitForSecondsRealtime(5f);

        // 5�ʵڿ� ��ȭ�����ͷ� ����
        shopMsgTxt.text = shopMsgs[0];
    }

    /// <summary>
    /// Player Respawn �浹�� RespawnUI �ؽ�Ʈ �ʱ�ȭ
    /// </summary>
    public void RespawnUIReset()
    {
        respawnUITxt.text = "�����Ͻ÷���  'e'  ��ư�� �����ּ���.";
    }

    /// <summary>
    /// RespawnUI 'e' ��ư�� ���� ����� �ؽ�Ʈ ����
    /// </summary>
    public void RespawnUIClick()
    {
        respawnUITxt.text = "���� �Ϸ� �Ǿ����ϴ�.";
    }

    #endregion Method
}
