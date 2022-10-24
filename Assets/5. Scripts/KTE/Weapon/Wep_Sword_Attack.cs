using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wep_Sword_Attack : MonoBehaviour
{
    // EnemyController 는 임시로 만든 스크립트이며 Enemy스크립트를 추후에 만들면 수정요구
    // 해당 무기의 내부쿨이니 추후 여러개의 무기를 객체화 시켜서 별도로 딜레이 및 데미지 수정 가능할 것이라 봄

    #region Variable
    // 공격 내부 쿨타임 변수
    private bool nowDamage = false;
    private float timer;

    // 공격력을 받아오기위한 선언
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
