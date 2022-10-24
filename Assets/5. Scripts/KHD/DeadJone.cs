using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadJone : MonoBehaviour
{
    public PlayerHpController player;
    private void OnCollisionEnter(Collision collision)
    {
            if (collision.gameObject.tag == "Player")
            {
                player.hp_damage = 0f;
            }   
    }
}
