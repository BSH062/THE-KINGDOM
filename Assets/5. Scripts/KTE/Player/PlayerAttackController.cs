using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{

    #region Variable
    public float attack;       // 플레이어 공격력
    public float attackDelay;  // 공격 딜레이

    public BoxCollider coli;   // 무기 충돌체

    // 공격 딜레이를 위한 변수
    private float timer = 0f; 
    private bool nowAttack;

    // 애니메이션
    Animator anim;
    #endregion Variable

    #region Unity Method
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        Attack();

    }
    #endregion Unity Method

    #region Method

    // 공격 메서드

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0) && nowAttack) // 공격 딜레이인지 확인 및 실행
        {
            nowAttack = false; // 딜레이를 위한 bool값
            StartCoroutine(AttackDel()); // 무기 충돌체 활성화 및 코루틴 시작
        }
        else if (nowAttack == false)  // 공격시 bool 값이 변동되며 타이머가 돌아감
        {
            timer += Time.deltaTime;
            if(timer > attackDelay) // 타이머가 일정 수치 이상에 도달할 시 bool값이 변동되며 다시 공격실행 가능
            {
                nowAttack = true;
                timer = 0f; // 타이머초기화
            }

        }
    }
    // 무기 충돌체 활성화 코루틴
    IEnumerator AttackDel()
    {
        coli.enabled = true;
        anim.SetTrigger("isAttack");

        yield return new WaitForSeconds(attackDelay);
        coli.enabled = false;
    }
    #endregion Method

}
