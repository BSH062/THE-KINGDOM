using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    #region Variable

    public bool[] questsAccept = { false, false, false };      // ����Ʈ ���� �ִ�3��
    public bool[] questsCompleted = { false, false, false };   // ����Ʈ �Ϸ� üũ
    public string[] questContent = {"", "", ""};               // ����Ʈ ����
    public string[] questCompensation = {"", "", ""};          // ����Ʈ ����
    public string[] questProgress = { "", "", "" };            // ����Ʈ ���൵
    public string[] questKeyword = {"", "", ""};               // ����Ʈ Ű����
    public int[] questTargetCount = {0, 0, 0};                 // ����Ʈ ��ǥ��
    public int[] questProgressCount = {0, 0, 0};               // ����Ʈ ���൵
    public int questCount =0;                                   // ���� �������� ����Ʈ ����
    



    #endregion Variable

    #region Unity Method



    #endregion Unity Method

    #region Method

    /// <summary>
    /// ����Ʈ ������ ����Ʈ ���� �߰�
    /// </summary>
    /// <param name="index">����Ʈ ��ȣ</param>
    /// <param name="compensation">����Ʈ ����</param>
    /// <param name="questTargetCount">����Ʈ ��ǥ��</param>
    public void AddQuest(int index, string questContent, string questKeyword, string compensation, string progress, int questTargetCount)
    {
        this.questsAccept[index] = true;
        this.questContent[index] = questContent;
        this.questKeyword[index] = questKeyword;
        this.questCompensation[index] = compensation;
        this.questProgress[index] = progress;
        this.questTargetCount[index] = questTargetCount;
        this.questProgressCount[index] = 0;
        questCount++;
    }

    /// <summary>
    /// ����Ʈ ���� ����
    /// </summary>
    /// <param name="index">����Ʈ ��ȣ</param>
    public void UpdateQuest(int index, string questContent, string questKeyword, string compensation, string progress, int questTargetCount, int progressCount)
    {
        this.questContent[index] = questContent;
        this.questKeyword[index] = questKeyword;
        this.questCompensation[index] = compensation;
        this.questProgress[index] = progress;
        this.questTargetCount[index] = questTargetCount;
        this.questProgressCount[index] = progressCount;
    }

    /// <summary>
    /// ����Ʈ ����� ����Ʈ ���� ����
    /// </summary>
    /// <param name="index">����Ʈ ��ȣ</param>
    public void DeleteQuest(int index)
    {
        this.questsAccept[index] = false;
        this.questContent[index] = "";
        this.questKeyword[index] = "";
        this.questCompensation[index] = "";
        this.questProgress[index] = "";
        this.questTargetCount[index] = 0;
        this.questProgressCount[index] = -1;
        questCount--;
    }

    /// <summary>
    /// ����Ʈ �������� ��������
    /// </summary>
    /// <param name="index">����Ʈ ��ȣ</param>
    /// <returns></returns>
    public bool getQeustAccept(int index)
    {
        return questsAccept[index];
    }

    /// <summary>
    /// ����Ʈ �ϷῩ�� ��������
    /// </summary>
    /// <param name="index">����Ʈ ��ȣ</param>
    /// <returns></returns>
    public bool getQeustCompleted(int index)
    {
        return questsCompleted[index];
    }

    /// <summary>
    /// ����Ʈ ���� ��������
    /// </summary>
    /// <param name="index">����Ʈ ��ȣ</param>
    /// <returns></returns>
    public string getQuestContent(int index)
    {
        return questContent[index];
    }

    /// <summary>
    /// ����Ʈ Ű���� ��������
    /// </summary>
    /// <param name="index">����Ʈ ��ȣ</param>
    /// <returns></returns>
    public string getQuestKeyword(int index)
    {
        return questKeyword[index];
    }

    /// <summary>
    /// ����Ʈ ���� ��������
    /// </summary>
    /// <param name="index">����Ʈ ��ȣ</param>
    /// <returns></returns>
    public string getQuestCompensation(int index)
    {
        return questCompensation[index];
    }

    /// <summary>
    /// ����Ʈ ���� ��������
    /// </summary>
    /// <param name="index">����Ʈ ��ȣ</param>
    /// <returns></returns>
    public string getQuestProgress(int index)
    {
        return questProgress[index];
    }

    /// <summary>
    /// ����Ʈ ��ǥ�� ��������
    /// </summary>
    /// <param name="index">����Ʈ ��ȣ</param>
    /// <returns></returns>
    public int getQuestTargetCount(int index)
    {
        return questProgressCount[index];
    }

    /// <summary>
    /// ����Ʈ ���൵ ��������
    /// </summary>
    /// <param name="index">����Ʈ ��ȣ</param>
    /// <returns></returns>
    public int getQuestProgressCount(int index)
    {
        return questProgressCount[index];
    }

    /// <summary>
    /// ����Ʈ ���൵ ����
    /// </summary>
    /// <param name="index">����Ʈ ��ȣ</param>
    /// <returns></returns>
    public void setQuestProgressCount(int index)
    {
        questProgressCount[index]++;
    }

    #endregion Method
}
