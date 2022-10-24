using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Doorcontroller : MonoBehaviour
{
    
     Animator Anim;
    
    void Start()
    {
        Anim=GetComponent<Animator>();
    }

    // Update is called once per frame
   
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            UIManager.Instance.BtnOpenUI("doorUI");
            if (Input.GetKeyDown(KeyCode.E)) //던전 입장 
            {
                Anim.SetBool("isDoor", true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag=="Player")
        {
            UIManager.Instance.BtnCloseUI("doorUI");
            Anim.SetBool("isDoor", false);
        }
    }
}