using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainDoor : MonoBehaviour
{
    public Camera cam;
    bool isCam = false;
     Animator Anim;
     bool isopen = false;
     public GameObject Doorui; //Canves_Door 

    void Start()
    {
        Anim = GetComponent<Animator>();

    }

    // Update is called once per frame

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && !isopen)
        {
            Doorui.SetActive(true);
            if (!isCam&&Input.GetKeyDown(KeyCode.E)) //던전 입장 
            {
                isCam = true;
                cam.farClipPlane = 50;
                Doorui.SetActive(false);
                Debug.Log("들어감");
                Anim.SetBool("isDoor", true);
                isopen = true;
            }
            else if (isCam&&Input.GetKeyDown(KeyCode.E)) //던전 입장 
            {
                isCam = false;
                cam.farClipPlane = 1000;
                Doorui.SetActive(false);
                Debug.Log("들어감");
                Anim.SetBool("isDoor", true);
                isopen = true;
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Doorui.SetActive(false);
            Debug.Log("나감");
            isopen = false;
            Anim.SetBool("isDoor", false);
        }
    }
}
