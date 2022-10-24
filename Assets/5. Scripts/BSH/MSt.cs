using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSt : MonoBehaviour
{
    public GameObject missile; //�̻��� ������ ������
    public Transform spawnTns; //������ 
    bool isStart = false; //�÷��̾ ���Դ��� üũ 
    float shotTime = 0; //�̻����� �߻�Ǳ���� �ɸ��� �ð� 
    private void Update()
    {
        if (isStart) //�÷��̾ ������ ���Դٸ� 
            shotTime += Time.deltaTime; //�߻�ð� ����
        else //������ ������ ���� ����
            return;

        if (shotTime > 2) //�߻� �ð��� 2�ʰ� �Ǹ� 
        {
            
            GameObject ins = Instantiate(missile); //������ �̻����� �����´�
            ins.transform.position = spawnTns.position; //�̻����� ������ ��ġ �ʱ�ȭ 
            ins.GetComponent<Bossmissile>().spawner = gameObject; //�Ҹ��ȿ� ����ִ� �����ʿ� ���ӿ�����Ʈ�� ��´� 
            shotTime = 0; //�߻�ð� �ٽ� �ʱ�ȭ 
        }
        
    }
  
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player") //�÷��̾ ������ �߻�ð� ���� 
        {
            
            isStart = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") //�÷��̾ ������ �߻�ð� ����
        {
            isStart = false;
        }
    }
}
