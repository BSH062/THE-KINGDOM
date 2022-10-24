using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyController : MonoBehaviour
{
    #region Variable

    [SerializeField]
    private Text moneyText; // UI�� ���� ǥ�õ� �ؽ�Ʈ

    public float money = 0; // �÷��̾� ��

    #endregion Variable

    #region Unity Method

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Money();
    }

    #endregion Unity Method

    #region Method

    private void Money()
    {
        moneyText.text = money.ToString();
    }
    #endregion Method

}
