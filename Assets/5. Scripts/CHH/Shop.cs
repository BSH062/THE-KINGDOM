using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    #region Variable

    public string itemName;         // 선택한 아이템 이름
    public int[] itemPrice;         // 아이템 가격
    public Sprite[] allItems;       // 전체 아이템 이미지
    public Sprite inventoryItem;    // 플레이에 있는 인벤토리 

    public GameObject player;       // 플레이어 오브젝트 
    public GameObject inventoryUi;  //플레이어 인벤

    #endregion Variable

    #region Method

    /// <summary>
    /// 상점 UI창에서 아이템을 클릭했을 때 아이템 구매 진행
    /// </summary>
    /// <param name="index">인벤토리 번호 인덱스</param>
    public void ItemCllick()
    {
        // 현재 선택한 버튼의 아이템 이름
        itemName = EventSystem.current.currentSelectedGameObject.name;

        // 플레이어 인벤토리 아이템 이름들
        string[] playerInventory = inventoryUi.GetComponent<Inventory>().items;
        Image[] playerInventoryItem = inventoryUi.GetComponent<Inventory>().inventoryItemImg;
        Text[] playerInventoryItemName = inventoryUi.GetComponent<Inventory>().inventoryItemTxt;


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

    }

    /// <summary>
    /// 상점에서 아이템 구매시 실행
    /// </summary>
    /// <param name="index">인벤토리 번호 인덱스</param>
    public void Buy(int index)
    {
        // 플레이어 소지금 가져오기
        int coin = player.GetComponent<Player>().coin;

        // 아이템 가격보다 플레이어 소지금이 부족시 구매불가 메세지 오픈 후 종료
        if (itemPrice[index] > coin)
        {
            UIManager.Instance.DontBuy();
            return;
        }
        // 아이템 구매가 가능할 때 실행
        else
        {
            // 소지금에서 아이템 가격 차감
            coin -= itemPrice[index];

            // 플레이어 소지금에 수정된 코인값 적용
            player.GetComponent<Player>().coin = coin;

            // coin Text UI 값 업데이트
            UIManager.Instance.CoinUIUpdate(coin);

            // 상점 UI창에서 아이템을 클릭했을 때 아이템 구매 진행
            ItemCllick();
        }
    }

    #endregion Method

}