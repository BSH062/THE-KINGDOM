using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerOpenUI : MonoBehaviour
{
    #region Variable

    public GameObject openUI;

    #endregion Variable

    #region Unity Method

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetButtonDown("Interaction"))
        {
            openUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    #endregion Unity Method

    #region Method

    public void BtnCloseClick()
    {
        openUI.SetActive(false);
        Time.timeScale = 1f;
    }

    #endregion Method

}
