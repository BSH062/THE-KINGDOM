using UnityEngine;
using UnityEngine.UI;

public class NPCManager : MonoBehaviour
{
    #region Variable

    public string generalContent;           // 일반 NPC 대화 내용
    public string questContent;             // 퀘스트 내용
    public int questTargetCount;            // 퀘스트 목표량
    public string questProgress;            // 퀘스트 진행도
    public string questKeyword;             // 퀘스트 키워드
    public string questCompensation;        // 퀘스트 보상

    #endregion Variable

    #region Unity Method

    /// <summary>
    /// 플레이어가 'e'키를 눌러 NPC와 상호작용 했을 때
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        // 접촉한 오브젝트의 태그가 플레이어면서 'e'키를 눌렀을 때
        if (other.tag == "Player" && Input.GetButtonDown("Interaction"))
        {
            string thisTag = gameObject.tag;    // 현재 오브젝트의 태그

            // 플레이어 퀘스트 컴포넌트 가져오기

            // 각 태그에 맞는 UI 기능 진행
            switch (thisTag)
            {
                // 상인 NPC
                case "MerchantNPC":
                    // shopUI, inventoryUI 캔버스 열기
                    UIManager.Instance.BtnOpenUI("shopUI");
                    UIManager.Instance.BtnOpenUI("inventoryUI");
                    break;
                // 퀘스트 NPC
                case "QuestNPC":
                    // 입력된 퀘스트 내용으로 QuestNPCUI 텍스트 변경 및 퀘스트 진행도 저장
                    UIManager.Instance.QuestNPCContentUpdate(questContent, questKeyword, questTargetCount, questProgress, questCompensation);
                    // questNPCUI 캔버스 열기
                    UIManager.Instance.BtnOpenUI("questNPCUI");
                    break;
                // 일반 NPC
                case "GeneralNPC":
                    // 일반 NPC 대화 UI의 텍스트 변경
                    UIManager.Instance.GeneralUIUpdate(generalContent);
                    // generalUI 캔버스 열기
                    UIManager.Instance.BtnOpenUI("generalUI");
                    break;
            }
        }
    }

    #endregion Unity Method

    #region Method



    #endregion Method
}
