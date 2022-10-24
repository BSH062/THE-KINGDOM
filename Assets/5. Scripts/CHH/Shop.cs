using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    #region Variable

    public string itemName;         // ������ ������ �̸�
    public int[] itemPrice;         // ������ ����
    public Sprite[] allItems;       // ��ü ������ �̹���
    public Sprite inventoryItem;    // �÷��̿� �ִ� �κ��丮 

    public GameObject player;       // �÷��̾� ������Ʈ 
    public GameObject inventoryUi;  //�÷��̾� �κ�

    #endregion Variable

    #region Method

    /// <summary>
    /// ���� UIâ���� �������� Ŭ������ �� ������ ���� ����
    /// </summary>
    /// <param name="index">�κ��丮 ��ȣ �ε���</param>
    public void ItemCllick()
    {
        // ���� ������ ��ư�� ������ �̸�
        itemName = EventSystem.current.currentSelectedGameObject.name;

        // �÷��̾� �κ��丮 ������ �̸���
        string[] playerInventory = inventoryUi.GetComponent<Inventory>().items;
        Image[] playerInventoryItem = inventoryUi.GetComponent<Inventory>().inventoryItemImg;
        Text[] playerInventoryItemName = inventoryUi.GetComponent<Inventory>().inventoryItemTxt;


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

    }

    /// <summary>
    /// �������� ������ ���Ž� ����
    /// </summary>
    /// <param name="index">�κ��丮 ��ȣ �ε���</param>
    public void Buy(int index)
    {
        // �÷��̾� ������ ��������
        int coin = player.GetComponent<Player>().coin;

        // ������ ���ݺ��� �÷��̾� �������� ������ ���źҰ� �޼��� ���� �� ����
        if (itemPrice[index] > coin)
        {
            UIManager.Instance.DontBuy();
            return;
        }
        // ������ ���Ű� ������ �� ����
        else
        {
            // �����ݿ��� ������ ���� ����
            coin -= itemPrice[index];

            // �÷��̾� �����ݿ� ������ ���ΰ� ����
            player.GetComponent<Player>().coin = coin;

            // coin Text UI �� ������Ʈ
            UIManager.Instance.CoinUIUpdate(coin);

            // ���� UIâ���� �������� Ŭ������ �� ������ ���� ����
            ItemCllick();
        }
    }

    #endregion Method

}