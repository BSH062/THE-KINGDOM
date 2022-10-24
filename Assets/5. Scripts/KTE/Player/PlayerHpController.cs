using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHpController : MonoBehaviour
{
    #region Variable
    // UI ����
    [SerializeField]
    private Animator anim;  //�÷��̾� �ִϸ��̼�
    [SerializeField]
    private Slider hp_slider;  // �÷��̾� Hp��
    [SerializeField]
    private TextMeshProUGUI HpText; // HP�� �ؽ�Ʈ
    [SerializeField]
    private ShadowThresholdCustomEffect Shadow; // ���� ȭ��
    [SerializeField]
    private GameObject respawnUI;

    // �������

    [SerializeField]
    private PlayerMove playerMove;
    [SerializeField]
    private PlayerAttackController attackController;
    [SerializeField]
    private PlayerRespawn playerRespawn;
    [SerializeField]
    private GameObject player;


    private bool live = true;
    public Vector3 spawnPoint;

    [SerializeField]
    public float hp_max = 100; // �÷��̾� Hp
    [SerializeField]
    public float hp_damage; // �÷��̾ �������� ���� Hp

    #endregion Variable

    #region Unity Method
    // Start is called before the first frame update
    void Start()
    {
        hp_damage = hp_max; // Hp �ʱ�ȭ
    }


    // Update is called once per frame
    void Update()
    {
        Hp(); 
        Die();
    }

    #endregion Unity Method

    #region Method

    //UI ���� �޼���
    private void Hp() 
    {
        hp_slider.maxValue = hp_max; // Slider�� �ִ밪�� Hp �ִ밪���� ����
        hp_slider.value = hp_damage; // slider�� ���� ���� �������� ����޴� Hp������ ����
        HpText.text = $"{hp_damage} / {hp_max}"; // ���
    }

    // �÷��̾� ���� �޼���
    // ü���� 0 �����Ͻ� ��� Ʈ���� �ߵ�
    private void Die()
    {
        
        if(hp_damage <= 0)
        {
            if (live)
            {
                anim.SetTrigger("isNowDie");
                anim.SetBool("isDie", true);
                Shadow.enabled = true;
                playerMove.enabled = false;
                attackController.enabled = false;
                respawnUI.SetActive(true);
                live = false;
            }
        }
        else
        {
            Shadow.enabled = false;
            playerMove.enabled = true;
            attackController.enabled = true;
            anim.SetBool("isDie", false);
            live = true;
        }

    }
    // ������ �Ծ��� �� �ִϸ��̼� �ߵ�
    public void Damage()
    {
        anim.SetTrigger("isDamage");
    }

    // ������ �޼���
    public void Respawn()
    {
        hp_damage = hp_max; // ü�� ����
        player.transform.position = spawnPoint; // �����ڷ���Ʈ
        RespawnTimer respawn = respawnUI.GetComponent<RespawnTimer>(); // ������ Ÿ�̸� �ʱ�ȭ�� ���� �ҷ�����
        respawn.end = true; // ������ Ÿ�̸� �ʱ�ȭ
        respawn.time = 3f; // ������ Ÿ�̸� �ʱ�ȭ
        respawnUI.SetActive(false); // ������ â ����

    }
    #endregion Method

}
