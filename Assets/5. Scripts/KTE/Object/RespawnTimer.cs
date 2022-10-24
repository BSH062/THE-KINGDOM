using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnTimer : MonoBehaviour
{
    public GameObject Respawn;
    public Text text;
    public GameObject btn;

    public bool end = true;
    public float time = 3f;

    void Update()
    {
        Timer();
    }

    private void Timer()
    {
        if (Respawn.activeSelf == true)
        {
            time -= Time.deltaTime;
            if (time >= 0 && end)
            {
                text.text = "��Ȱ ��� ��.. " + ((int)time).ToString();
                btn.SetActive(false);
            }
            else
            {
                text.text = "��Ȱ�Ͻðڽ��ϱ�?";
                btn.SetActive(true);
            }
        }
    }
}
