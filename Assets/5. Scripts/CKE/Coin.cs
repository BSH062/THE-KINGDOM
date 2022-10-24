using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    #region Variable

    static AudioSource audioSource;         // 오디오 컴포넌트
    public static AudioClip audioClip;      // 코인 먹을 때 소리

    #endregion Variable

    #region Unity Method

    /// <summary>
    /// 컴포넌트 초기화
    /// </summary>
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioClip = Resources.Load<AudioClip>("Coin1");
    }

    // 매 프레임마다 실행
    void Update()
    {
        //Time.deltaTime은 지난 프레임이 완료되는데까지 걸린 시간을 나타내며, 단위는 초를 사용한다. 
        //아래 수식은 초당 15, 30, 45를 이동하라는 의미이다. 

        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    // 코인과 충돌시 실행
    private void OnTriggerEnter(Collider collision)
    {
        // 충돌한 객체가 플레이어 일때만 실행
        if(collision.tag == "Player")
        {
            // Player 스크립트의 coin에 +100 추가
            collision.GetComponent<Player>().coin += 100;

            // coin Text UI 값 업데이트
            UIManager.Instance.CoinUIUpdate(collision.GetComponent<Player>().coin);

            //코인획득 시 효과음 발생
            SoundPlay();

            // 충돌 후 코인 안보이게 감추기
            gameObject.SetActive(false);

            // 1초 후 충돌한 코인 없애기(코인획득 소리 나온 후에 삭제하기 위해 1초간 유예)
            Destroy(gameObject, 1f);

        }
    }

    #endregion Unity Method

    #region Method

    /// <summary>
    /// 코인 사운드 재생
    /// </summary>
    public static void SoundPlay()
    {
        audioSource.PlayOneShot(audioClip);
    }

    #endregion Method
}
