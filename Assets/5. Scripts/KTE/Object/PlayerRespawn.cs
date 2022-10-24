using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRespawn : MonoBehaviour
{
    public PlayerHpController playerHpController;

    public Vector3 respawn;  // �÷��̾� ���� ��ġ ����
    private float time = 0;    // ����Ϸ� �ؽ�Ʈ �ٲ� �� ���� �ؽ�Ʈ�� �ʱ�ȭ �ð�

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            // ������ UIâ ����
            UIManager.Instance.BtnOpenUI("respawnUI");

            time += Time.deltaTime;

            // ����Ϸ� �ؽ�Ʈ �ٲ� �� ���� �ؽ�Ʈ�� �ʱ�ȭ �ð�
            if (time >= 5.0f)
            {
                time = 0;
                UIManager.Instance.RespawnUIReset();
            }

            if(Input.GetKeyDown(KeyCode.E))
            {
                respawn = other.gameObject.transform.position + new Vector3(0, 2f, 0);
                playerHpController.spawnPoint = respawn;

                // ���� �Ϸ� �ؽ�Ʈ ����
                UIManager.Instance.RespawnUIClick();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // ������ UIâ �ݱ�
        UIManager.Instance.BtnCloseUI("respawnUI");
    }
}
