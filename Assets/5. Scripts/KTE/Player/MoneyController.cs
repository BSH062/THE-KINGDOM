using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyController : MonoBehaviour
{
    #region Variable

    [SerializeField]
    private Text moneyText; // UI에 돈이 표시될 텍스트

    public float money = 0; // 플레이어 돈

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
