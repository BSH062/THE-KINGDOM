using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent agent;
    private Animator anim;
    public PlayerHpController player;
    public GameObject compensationItem; // 보상 아이템

    public enum State
    {
        IDLE,
        TRACE,
        ATTACK,
        DIE
    }

    //Animator 파라미터의 해시값 추출
    private readonly int hashWalk = Animator.StringToHash("IsWalk");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");
    private readonly int hashHit = Animator.StringToHash("Hit");
    private readonly int hashPlayerDie = Animator.StringToHash("PlayerDie");
    private readonly int hashDie = Animator.StringToHash("Die");

    //몬스터의 현재 상태
    public State state = State.IDLE;

    //추적 사정거리
    public float traceDist = 10.0f;

    //공격 사정거리
    public float attackDist = 2.0f;

    //몬스터의 사망 여부
    public bool isDie = false;

    //몬스터 생명 변수
    public float Hp = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        //몬스터의 Transform활당
        monsterTr = GetComponent<Transform>();

        //추적 대상인 Player의 Transform 할당
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();

        //NavMeshAgent 컴포넌트 활당
        agent = GetComponent<NavMeshAgent>();

        //Animator 컴포넌트 활당
        anim = GetComponent<Animator>();

        //추적 대상의 위치를 설정하면 바로 추적 시작
        //agent.destination = playerTr.position;

        //몬스터의 상태를 체크하는 코루틴 함수 호출
        StartCoroutine(CheckMonsterState());

        //상태에 따라 몬스터의 행동을 수행하는 코루틴 함수 호출
        StartCoroutine(MonsterAction());
    }
    void Update()
    {
       
    }

    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            //0.3초 동안 중지(대기)하는 동안 제어권을 메시지 루프에 양보
            yield return new WaitForSeconds(0.3f);

            //몬스터 상태가 DIE일때 코루틴 종료
            if (state == State.DIE) yield break;

            //몬스터 와 캐릭터 사이의 거리 측정
            float distance = Vector3.Distance(playerTr.position, monsterTr.position);

            if (distance <= attackDist)
            {
                state = State.ATTACK;
            }

            else if (distance <= traceDist)
            {
                state = State.TRACE;
            }
            
            else if (Hp <= 0)
            {
                state = State.DIE;
            }

            else 
            {
                state = State.IDLE;
            }
        }
    }

    IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (state)
            {
                //IDLE 상태
                case State.IDLE:
                    //추적 중지
                    agent.isStopped = true;
                    //Animator의 IsWalk변수를 false로 설정
                    anim.SetBool(hashWalk, false);                  
                    break;

                case State.TRACE:
                    //추적 대상의 좌표로 이동 시작
                    agent.SetDestination(playerTr.position);
                    agent.isStopped = false;

                    //Animator의 IsWalk 변수를 true로 설정
                    anim.SetBool(hashWalk, true);

                    //Animator의 IsAttack 변수를 false로 설정
                    anim.SetBool(hashAttack, false);
                    break;

                //공격상태
                case State.ATTACK:                   
                    //Animator의 IsAttack 변수를 true로 설정
                    anim.SetBool(hashAttack, true);
                  
                    break;

                //사망
                case State.DIE:
                    isDie = true;

                    //추적 중지
                    agent.isStopped = true;

                    //사망 애니메이션 실행
                    anim.SetTrigger(hashDie);

                    //몬스터의 콜라이더 비활성화
                    GetComponent<CapsuleCollider>().enabled = false;

                    // 보상 아이템 생성
                    Vector3 itemPos = transform.position + new Vector3(0, 1, 0);

                    // 보상아이템이 존재할 경우만 실행
                    if(compensationItem != null)
                    {
                        GameObject item = Instantiate(compensationItem, itemPos, transform.rotation);
                    }
                    
                    break;
            }
            yield return new WaitForSeconds(0.3f);      
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Sword")
        {
            anim.SetTrigger(hashHit);

            if (Hp <= 0)
            {
                state = State.DIE;
            }
        }
    }
    

 
    private void OnDrawGizmos()
    {
        //추적 사정거리 표시
        if(state == State.TRACE)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, traceDist);
        }

        //공격 사정거리 표시
        if(state == State.ATTACK)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDist);
        }

    }

    public void onEnemyDie()
    {
        //몬스터의 상태를 체크하는 코루틴 함수를 모두 정지시킴
        StopAllCoroutines();

        //추적을 정지하고 애니메이션을 수행
        agent.isStopped = true;
        anim.SetTrigger(hashDie);
        StartCoroutine(DestoryObject());
        Destroy(gameObject, 5f);
    }

   void onPlayerDie()
    {
        //몬스터의 상태를 체크하는 코루틴 함수를 모두 정지시킴
        StopAllCoroutines();

        //추적을 정지하고 애니메이션을 수행
        agent.isStopped = true;
        anim.SetTrigger(hashPlayerDie);

    }

    IEnumerator DestoryObject()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
