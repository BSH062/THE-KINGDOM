using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    #region Variable

    public Transform Player; // ����ٴ� Ÿ��

    #endregion Variable

    #region Unity Method

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.position;
    }

    #endregion Unity Method

}
