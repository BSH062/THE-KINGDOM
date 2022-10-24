using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Bossmissile : Bullet
{
    public GameObject target;
    Vector3 targetPos;
    public PlayerHpController playerHpController;
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");

        playerHpController = GameObject.Find("PlayerHpController").GetComponent<PlayerHpController>();
        if (playerHpController == null) Debug.Log("playerHpController ¾øÀ½");
    }
    private void Update()
    {
        targetPos = target.transform.position;
        targetPos.y += 1.3f;
        transform.LookAt(targetPos); 
        transform.Translate(Vector3.forward * 10 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerHpController.hp_damage -= damage;
            Destroy(gameObject);
        }
        else if (other.gameObject != spawner)
        {
            Destroy(gameObject);
        }
    }
}
