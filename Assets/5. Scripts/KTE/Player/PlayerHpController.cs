using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHpController : MonoBehaviour
{
    #region Variable
    // UI 적용
    [SerializeField]
    private Animator anim;  //플레이어 애니메이션
    [SerializeField]
    private Slider hp_slider;  // 플레이어 Hp바
    [SerializeField]
    private TextMeshProUGUI HpText; // HP바 텍스트
    [SerializeField]
    private ShadowThresholdCustomEffect Shadow; // 죽음 화면
    [SerializeField]
    private GameObject respawnUI;

    // 여기까지

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
    public float hp_max = 100; // 플레이어 Hp
    [SerializeField]
    public float hp_damage; // 플레이어가 데미지를 받을 Hp

    #endregion Variable

    #region Unity Method
    // Start is called before the first frame update
    void Start()
    {
        hp_damage = hp_max; // Hp 초기화
    }


    // Update is called once per frame
    void Update()
    {
        Hp(); 
        Die();
    }

    #endregion Unity Method

    #region Method

    //UI 적용 메서드
    private void Hp() 
    {
        hp_slider.maxValue = hp_max; // Slider의 최대값을 Hp 최대값으로 변경
        hp_slider.value = hp_damage; // slider의 현재 값을 데미지를 적용받는 Hp값으로 변경
        HpText.text = $"{hp_damage} / {hp_max}"; // 출력
    }

    // 플레이어 적용 메서드
    // 체력이 0 이하일시 사망 트리거 발동
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
    // 데미지 입었을 때 애니메이션 발동
    public void Damage()
    {
        anim.SetTrigger("isDamage");
    }

    // 리스폰 메서드
    public void Respawn()
    {
        hp_damage = hp_max; // 체력 충전
        player.transform.position = spawnPoint; // 스폰텔레포트
        RespawnTimer respawn = respawnUI.GetComponent<RespawnTimer>(); // 리스폰 타이머 초기화를 위한 불러오기
        respawn.end = true; // 리스폰 타이머 초기화
        respawn.time = 3f; // 리스폰 타이머 초기화
        respawnUI.SetActive(false); // 리스폰 창 종료

    }
    #endregion Method

}
