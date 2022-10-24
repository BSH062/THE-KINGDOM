using UnityEngine;
using UnityEngine.UI;

public class NPCManager : MonoBehaviour
{
    #region Variable

    public string generalContent;           // �Ϲ� NPC ��ȭ ����
    public string questContent;             // ����Ʈ ����
    public int questTargetCount;            // ����Ʈ ��ǥ��
    public string questProgress;            // ����Ʈ ���൵
    public string questKeyword;             // ����Ʈ Ű����
    public string questCompensation;        // ����Ʈ ����

    #endregion Variable

    #region Unity Method

    /// <summary>
    /// �÷��̾ 'e'Ű�� ���� NPC�� ��ȣ�ۿ� ���� ��
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        // ������ ������Ʈ�� �±װ� �÷��̾�鼭 'e'Ű�� ������ ��
        if (other.tag == "Player" && Input.GetButtonDown("Interaction"))
        {
            string thisTag = gameObject.tag;    // ���� ������Ʈ�� �±�

            // �÷��̾� ����Ʈ ������Ʈ ��������

            // �� �±׿� �´� UI ��� ����
            switch (thisTag)
            {
                // ���� NPC
                case "MerchantNPC":
                    // shopUI, inventoryUI ĵ���� ����
                    UIManager.Instance.BtnOpenUI("shopUI");
                    UIManager.Instance.BtnOpenUI("inventoryUI");
                    break;
                // ����Ʈ NPC
                case "QuestNPC":
                    // �Էµ� ����Ʈ �������� QuestNPCUI �ؽ�Ʈ ���� �� ����Ʈ ���൵ ����
                    UIManager.Instance.QuestNPCContentUpdate(questContent, questKeyword, questTargetCount, questProgress, questCompensation);
                    // questNPCUI ĵ���� ����
                    UIManager.Instance.BtnOpenUI("questNPCUI");
                    break;
                // �Ϲ� NPC
                case "GeneralNPC":
                    // �Ϲ� NPC ��ȭ UI�� �ؽ�Ʈ ����
                    UIManager.Instance.GeneralUIUpdate(generalContent);
                    // generalUI ĵ���� ����
                    UIManager.Instance.BtnOpenUI("generalUI");
                    break;
            }
        }
    }

    #endregion Unity Method

    #region Method



    #endregion Method
}
