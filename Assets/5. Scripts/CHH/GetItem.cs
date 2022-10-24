using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GetItem : MonoBehaviour
{
    #region Variable

    public Sprite[] allItems;       // ��ü ������ �̹���
    public Inventory inventory;     // �κ��丮 ������Ʈ

    #endregion Variable

    #region Unity Method

    /// <summary>
    /// ������ �浹�� ����
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            // ���� ������ ��ư�� ������ �̸�
            string itemName = gameObject.name.Replace("(Clone)", "");
            Debug.Log(itemName);

            // �÷��̾� �κ��丮 ������ �̸���
            string[] playerInventory = inventory.items;
            Image[] playerInventoryItem = inventory.inventoryItemImg;
            Text[] playerInventoryItemName = inventory.inventoryItemTxt;


            // ��ü �÷��̾� �κ��丮 ĭ���� ������� �������� ���� �� �ֵ�����
            for (int i = 0; i < playerInventory.Length; i++)
            {
                // ����ִ��� Ȯ��
                if (playerInventory[i] == null || playerInventory[i] == "")
                {
                    // �÷��̾� �κ��丮�� ������ �̸� �߰�
                    playerInventory[i] = itemName;

                    foreach (Sprite item in allItems)
                    {
                        // ���� ������ ��ư�� ������ �̸��� ������ �ִ� ������ ��� ������ Ȯ��
                        if (item.name == itemName)
                        {
                            // ������ �ִ� �������̶�� �÷��̾� �κ��丮�� �߰�
                            //if (playerInventoryItem[i] == null) Debug.Log("����");
                            // if (item == null) Debug.Log("item ����");
                            playerInventoryItem[i].sprite = item;
                            playerInventoryItemName[i].text = itemName;
                            break;
                        }
                    }

                    break;  // break ���� ������ �ڿ� �ִ� ��ĭ�� ������ �����۸����� ä����
                }
            }


            Destroy(this.gameObject);
        }
    }

    #endregion Unity Method
}
