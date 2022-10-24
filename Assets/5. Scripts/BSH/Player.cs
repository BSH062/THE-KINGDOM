using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int coin;
    public int maxCoin;
    

    public void FixedUpdate()
    {
        // 'i' ��ư�� ������ �κ��丮 UI ����
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
