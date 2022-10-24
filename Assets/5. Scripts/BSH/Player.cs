using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int coin;
    public int maxCoin;
    

    public void FixedUpdate()
    {
        // 'i' 버튼을 누르면 인벤토리 UI 오픈
        if (Input.GetButtonDown("Inventory"))
        {
            if (UIManager.Instance.inventoryUI.activeSelf == true)
            {
                UIManager.Instance.BtnCloseUI("inventoryUI");
            }
            else
            {
                UIManager.Instance.BtnOpenUI("inventoryUI");
            }
        }
    }
}
