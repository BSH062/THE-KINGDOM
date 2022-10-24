using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapeons : MonoBehaviour
{
    public PlayerHpController player;
    public float attackDamage;
    private float timer;
    private bool nowDamge;

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player" && nowDamge == false)
        {

            if (player.hp_damage >= 0)
            {

                player.hp_damage -= attackDamage;
                player.Damage();
                nowDamge = true;
                //Debug.Log("Ãæµ¹");
                StartCoroutine(DamageDelay());
            }
        }
    }

    IEnumerator DamageDelay()
    {
        yield return new WaitForSeconds(1f);
        nowDamge = false;
    }

}
