using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    #region Variable

    public Transform[] itemSpawnPoint;
    private int randRos;
    public GameObject[] items;
    private int randItem;

    #endregion Variable

    #region Unity Method

    private void Start()
    {
        InvokeRepeating("SpawnItem", 5, 1);     // 5초에 1번씩 랜덤 아이템 생성
    }

    #endregion Unity Method

    #region Method

    private void SpawnItem()
    {
        //randRos = Random.Range(0, itemSpawnPoint.Length);
        for(int i = 0; i < itemSpawnPoint.Length; i++)
        {
            randItem = Random.Range(0, items.Length);

            float randomX = Random.Range(-30f, 30f);
            float randomZ = Random.Range(-30f, 30f);
            Vector3 itemPos = itemSpawnPoint[i].position + new Vector3(randomX, 0, randomZ);

            GameObject item = Instantiate(items[randItem], itemPos, Quaternion.identity);
            Destroy(item, 5f);
        }
    }

    #endregion Method
}