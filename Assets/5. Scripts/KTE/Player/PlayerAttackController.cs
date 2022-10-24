using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{

    #region Variable
    public float attack;       // �÷��̾� ���ݷ�
    public float attackDelay;  // ���� ������

    public BoxCollider coli;   // ���� �浹ü

    // ���� �����̸� ���� ����
    private float timer = 0f; 
    private bool nowAttack;

    // �ִϸ��̼�
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

    // ���� �޼���

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0) && nowAttack) // ���� ���������� Ȯ�� �� ����
        {
            nowAttack = false; // �����̸� ���� bool��
            StartCoroutine(AttackDel()); // ���� �浹ü Ȱ��ȭ �� �ڷ�ƾ ����
        }
        else if (nowAttack == false)  // ���ݽ� bool ���� �����Ǹ� Ÿ�̸Ӱ� ���ư�
        {
            timer += Time.deltaTime;
            if(timer > attackDelay) // Ÿ�̸Ӱ� ���� ��ġ �̻� ������ �� bool���� �����Ǹ� �ٽ� ���ݽ��� ����
            {
                nowAttack = true;
                timer = 0f; // Ÿ�̸��ʱ�ȭ
            }

        }
    }
    // ���� �浹ü Ȱ��ȭ �ڷ�ƾ
    IEnumerator AttackDel()
    {
        coli.enabled = true;
        anim.SetTrigger("isAttack");

        yield return new WaitForSeconds(attackDelay);
        coli.enabled = false;
    }
    #endregion Method

}
