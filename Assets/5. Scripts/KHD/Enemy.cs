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
    public GameObject compensationItem; // ���� ������

    public enum State
    {
        IDLE,
        TRACE,
        ATTACK,
        DIE
    }

    //Animator �Ķ������ �ؽð� ����
    private readonly int hashWalk = Animator.StringToHash("IsWalk");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");
    private readonly int hashHit = Animator.StringToHash("Hit");
    private readonly int hashPlayerDie = Animator.StringToHash("PlayerDie");
    private readonly int hashDie = Animator.StringToHash("Die");

    //������ ���� ����
    public State state = State.IDLE;

    //���� �����Ÿ�
    public float traceDist = 10.0f;

    //���� �����Ÿ�
    public float attackDist = 2.0f;

    //������ ��� ����
    public bool isDie = false;

    //���� ���� ����
    public float Hp = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        //������ TransformȰ��
        monsterTr = GetComponent<Transform>();

        //���� ����� Player�� Transform �Ҵ�
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();

        //NavMeshAgent ������Ʈ Ȱ��
        agent = GetComponent<NavMeshAgent>();

        //Animator ������Ʈ Ȱ��
        anim = GetComponent<Animator>();

        //���� ����� ��ġ�� �����ϸ� �ٷ� ���� ����
        //agent.destination = playerTr.position;

        //������ ���¸� üũ�ϴ� �ڷ�ƾ �Լ� ȣ��
        StartCoroutine(CheckMonsterState());

        //���¿� ���� ������ �ൿ�� �����ϴ� �ڷ�ƾ �Լ� ȣ��
        StartCoroutine(MonsterAction());
    }
    void Update()
    {
       
    }

    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            //0.3�� ���� ����(���)�ϴ� ���� ������� �޽��� ������ �纸
            yield return new WaitForSeconds(0.3f);

            //���� ���°� DIE�϶� �ڷ�ƾ ����
            if (state == State.DIE) yield break;

            //���� �� ĳ���� ������ �Ÿ� ����
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
                //IDLE ����
                case State.IDLE:
                    //���� ����
                    agent.isStopped = true;
                    //Animator�� IsWalk������ false�� ����
                    anim.SetBool(hashWalk, false);                  
                    break;

                case State.TRACE:
                    //���� ����� ��ǥ�� �̵� ����
                    agent.SetDestination(playerTr.position);
                    agent.isStopped = false;

                    //Animator�� IsWalk ������ true�� ����
                    anim.SetBool(hashWalk, true);

                    //Animator�� IsAttack ������ false�� ����
                    anim.SetBool(hashAttack, false);
                    break;

                //���ݻ���
                case State.ATTACK:                   
                    //Animator�� IsAttack ������ true�� ����
                    anim.SetBool(hashAttack, true);
                  
                    break;

                //���
                case State.DIE:
                    isDie = true;

                    //���� ����
                    agent.isStopped = true;

                    //��� �ִϸ��̼� ����
                    anim.SetTrigger(hashDie);

                    //������ �ݶ��̴� ��Ȱ��ȭ
                    GetComponent<CapsuleCollider>().enabled = false;

                    // ���� ������ ����
                    Vector3 itemPos = transform.position + new Vector3(0, 1, 0);

                    // ����������� ������ ��츸 ����
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
        //���� �����Ÿ� ǥ��
        if(state == State.TRACE)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, traceDist);
        }

        //���� �����Ÿ� ǥ��
        if(state == State.ATTACK)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDist);
        }

    }

    public void onEnemyDie()
    {
        //������ ���¸� üũ�ϴ� �ڷ�ƾ �Լ��� ��� ������Ŵ
        StopAllCoroutines();

        //������ �����ϰ� �ִϸ��̼��� ����
        agent.isStopped = true;
        anim.SetTrigger(hashDie);
        StartCoroutine(DestoryObject());
        Destroy(gameObject, 5f);
    }

   void onPlayerDie()
    {
        //������ ���¸� üũ�ϴ� �ڷ�ƾ �Լ��� ��� ������Ŵ
        StopAllCoroutines();

        //������ �����ϰ� �ִϸ��̼��� ����
        agent.isStopped = true;
        anim.SetTrigger(hashPlayerDie);

    }

    IEnumerator DestoryObject()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
