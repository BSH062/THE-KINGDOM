using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSt : MonoBehaviour
{
    public GameObject missile; //미사일 프리팹 받을곳
    public Transform spawnTns; //스포너 
    bool isStart = false; //플레이어가 들어왔는지 체크 
    float shotTime = 0; //미사일이 발사되기까지 걸리는 시간 
    private void Update()
    {
        if (isStart) //플레이어가 안으로 들어왔다면 
            shotTime += Time.deltaTime; //발사시간 갱신
        else //범위에 없으면 실행 안함
            return;

        if (shotTime > 2) //발사 시간이 2초가 되면 
        {
            
            GameObject ins = Instantiate(missile); //프리팹 미사일을 가져온다
            ins.transform.position = spawnTns.position; //미사일이 생성될 위치 초기화 
            ins.GetComponent<Bossmissile>().spawner = gameObject; //불릿안에 들어있는 스포너에 게임오브젝트를 담는다 
            shotTime = 0; //발사시간 다시 초기화 
        }
        
    }
  
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player") //플레이어가 들어오면 발사시간 시작 
        {
            
            isStart = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") //플레이어가 나가면 발사시간 멈춤
        {
            isStart = false;
        }
    }
}
