using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GetItem : MonoBehaviour
{
    #region Variable

    public Sprite[] allItems;       // 전체 아이템 이미지
    public Inventory inventory;     // 인벤토리 컴포넌트

    #endregion Variable

    #region Unity Method

    /// <summary>
    /// 아이템 충돌시 실행
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            // 현재 선택한 버튼의 아이템 이름
            string itemName = gameObject.name.Replace("(Clone)", "");
            Debug.Log(itemName);

            // 플레이어 인벤토리 아이템 이름들
            string[] playerInventory = inventory.items;
            Image[] playerInventoryItem = inventory.inventoryItemImg;
            Text[] playerInventoryItemName = inventory.inventoryItemTxt;


            // 전체 플레이어 인벤토리 칸에서 빈곳에서 아이템을 넣을 수 있도록함
            for (int i = 0; i < playerInventory.Length; i++)
            {
                // 비어있는지 확인
                if (playerInventory[i] == null || playerInventory[i] == "")
                {
                    // 플레이어 인벤토리에 아이템 이름 추가
                    playerInventory[i] = itemName;

                    foreach (Sprite item in allItems)
                    {
                        // 현재 선택한 버튼의 아이템 이름이 상점에 있는 아이템 명과 같은지 확인
                        if (item.name == itemName)
                        {
                            // 상점에 있는 아이템이라면 플레이어 인벤토리에 추가
                            //if (playerInventoryItem[i] == null) Debug.Log("없음");
                            // if (item == null) Debug.Log("item 없음");
                            playerInventoryItem[i].sprite = item;
                            playerInventoryItemName[i].text = itemName;
                            break;
                        }
                    }

                    break;  // break 쓰지 않으면 뒤에 있는 빈칸에 동일한 아이템명으로 채워짐
                }
            }


            Destroy(this.gameObject);
        }
    }

    #endregion Unity Method
}
