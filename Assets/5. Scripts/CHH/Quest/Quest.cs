using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    #region Variable

    public bool[] questsAccept = { false, false, false };      // 퀘스트 수락 최대3개
    public bool[] questsCompleted = { false, false, false };   // 퀘스트 완료 체크
    public string[] questContent = {"", "", ""};               // 퀘스트 내용
    public string[] questCompensation = {"", "", ""};          // 퀘스트 보상
    public string[] questProgress = { "", "", "" };            // 퀘스트 진행도
    public string[] questKeyword = {"", "", ""};               // 퀘스트 키워드
    public int[] questTargetCount = {0, 0, 0};                 // 퀘스트 목표량
    public int[] questProgressCount = {0, 0, 0};               // 퀘스트 진행도
    public int questCount =0;                                   // 현재 진행중인 퀘스트 개수
    



    #endregion Variable

    #region Unity Method



    #endregion Unity Method

    #region Method

    /// <summary>
    /// 퀘스트 수락시 퀘스트 정보 추가
    /// </summary>
    /// <param name="index">퀘스트 번호</param>
    /// <param name="compensation">퀘스트 보상</param>
    /// <param name="questTargetCount">퀘스트 목표량</param>
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
    /// 퀘스트 내용 수정
    /// </summary>
    /// <param name="index">퀘스트 번호</param>
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
    /// 퀘스트 포기시 퀘스트 정보 삭제
    /// </summary>
    /// <param name="index">퀘스트 번호</param>
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
    /// 퀘스트 수락여부 가져오기
    /// </summary>
    /// <param name="index">퀘스트 번호</param>
    /// <returns></returns>
    public bool getQeustAccept(int index)
    {
        return questsAccept[index];
    }

    /// <summary>
    /// 퀘스트 완료여부 가져오기
    /// </summary>
    /// <param name="index">퀘스트 번호</param>
    /// <returns></returns>
    public bool getQeustCompleted(int index)
    {
        return questsCompleted[index];
    }

    /// <summary>
    /// 퀘스트 내용 가져오기
    /// </summary>
    /// <param name="index">퀘스트 번호</param>
    /// <returns></returns>
    public string getQuestContent(int index)
    {
        return questContent[index];
    }

    /// <summary>
    /// 퀘스트 키워드 가져오기
    /// </summary>
    /// <param name="index">퀘스트 번호</param>
    /// <returns></returns>
    public string getQuestKeyword(int index)
    {
        return questKeyword[index];
    }

    /// <summary>
    /// 퀘스트 보상 가져오기
    /// </summary>
    /// <param name="index">퀘스트 번호</param>
    /// <returns></returns>
    public string getQuestCompensation(int index)
    {
        return questCompensation[index];
    }

    /// <summary>
    /// 퀘스트 보상 가져오기
    /// </summary>
    /// <param name="index">퀘스트 번호</param>
    /// <returns></returns>
    public string getQuestProgress(int index)
    {
        return questProgress[index];
    }

    /// <summary>
    /// 퀘스트 목표량 가져오기
    /// </summary>
    /// <param name="index">퀘스트 번호</param>
    /// <returns></returns>
    public int getQuestTargetCount(int index)
    {
        return questProgressCount[index];
    }

    /// <summary>
    /// 퀘스트 진행도 가져오기
    /// </summary>
    /// <param name="index">퀘스트 번호</param>
    /// <returns></returns>
    public int getQuestProgressCount(int index)
    {
        return questProgressCount[index];
    }

    /// <summary>
    /// 퀘스트 진행도 수정
    /// </summary>
    /// <param name="index">퀘스트 번호</param>
    /// <returns></returns>
    public void setQuestProgressCount(int index)
    {
        questProgressCount[index]++;
    }

    #endregion Method
}
