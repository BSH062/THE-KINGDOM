using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wep_Sword_Attack : MonoBehaviour
{
    // EnemyController �� �ӽ÷� ���� ��ũ��Ʈ�̸� Enemy��ũ��Ʈ�� ���Ŀ� ����� �����䱸
    // �ش� ������ �������̴� ���� �������� ���⸦ ��üȭ ���Ѽ� ������ ������ �� ������ ���� ������ ���̶� ��

    #region Variable
    // ���� ���� ��Ÿ�� ����
    private bool nowDamage = false;
    private float timer;

    // ���ݷ��� �޾ƿ������� ����
    public PlayerAttackController controller;

    #endregion Variable

    #region Unity Method
    private void Update()
    {
        DamageDelay();
    }
    #endregion Unity Method

    #region Method
    private void OnTriggerEnter(Collider other)
    {
        if((other.gameObject.tag == "Enemy") && nowDamage == false)
        {
            Enemy Enemy = other.GetComponent<Enemy>();
            Enemy.Hp -= controller.attack;
            nowDamage = true;
        }
    }

    private void DamageDelay()
    {
        if (nowDamage == true)
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {
                timer = 0;
                nowDamage = false;
            }
        }
    }
    #endregion Method


}
