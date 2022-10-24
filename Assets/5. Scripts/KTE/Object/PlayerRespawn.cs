using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRespawn : MonoBehaviour
{
    public PlayerHpController playerHpController;

    public Vector3 respawn;  // 플레이어 현재 위치 저장
    private float time = 0;    // 저장완료 텍스트 바꾼 후 원래 텍스트로 초기화 시간

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            // 리스폰 UI창 오픈
            UIManager.Instance.BtnOpenUI("respawnUI");

            time += Time.deltaTime;

            // 저장완료 텍스트 바꾼 후 원래 텍스트로 초기화 시간
            if (time >= 5.0f)
            {
                time = 0;
                UIManager.Instance.RespawnUIReset();
            }

            if(Input.GetKeyDown(KeyCode.E))
            {
                respawn = other.gameObject.transform.position + new Vector3(0, 2f, 0);
                playerHpController.spawnPoint = respawn;

                // 저장 완료 텍스트 변경
                UIManager.Instance.RespawnUIClick();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 리스폰 UI창 닫기
        UIManager.Instance.BtnCloseUI("respawnUI");
    }
}
