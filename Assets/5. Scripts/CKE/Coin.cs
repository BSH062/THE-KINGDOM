using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    #region Variable

    static AudioSource audioSource;         // ����� ������Ʈ
    public static AudioClip audioClip;      // ���� ���� �� �Ҹ�

    #endregion Variable

    #region Unity Method

    /// <summary>
    /// ������Ʈ �ʱ�ȭ
    /// </summary>
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioClip = Resources.Load<AudioClip>("Coin1");
    }

    // �� �����Ӹ��� ����
    void Update()
    {
        //Time.deltaTime�� ���� �������� �Ϸ�Ǵµ����� �ɸ� �ð��� ��Ÿ����, ������ �ʸ� ����Ѵ�. 
        //�Ʒ� ������ �ʴ� 15, 30, 45�� �̵��϶�� �ǹ��̴�. 

        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    // ���ΰ� �浹�� ����
    private void OnTriggerEnter(Collider collision)
    {
        // �浹�� ��ü�� �÷��̾� �϶��� ����
        if(collision.tag == "Player")
        {
            // Player ��ũ��Ʈ�� coin�� +100 �߰�
            collision.GetComponent<Player>().coin += 100;

            // coin Text UI �� ������Ʈ
            UIManager.Instance.CoinUIUpdate(collision.GetComponent<Player>().coin);

            //����ȹ�� �� ȿ���� �߻�
            SoundPlay();

            // �浹 �� ���� �Ⱥ��̰� ���߱�
            gameObject.SetActive(false);

            // 1�� �� �浹�� ���� ���ֱ�(����ȹ�� �Ҹ� ���� �Ŀ� �����ϱ� ���� 1�ʰ� ����)
            Destroy(gameObject, 1f);

        }
    }

    #endregion Unity Method

    #region Method

    /// <summary>
    /// ���� ���� ���
    /// </summary>
    public static void SoundPlay()
    {
        audioSource.PlayOneShot(audioClip);
    }

    #endregion Method
}
