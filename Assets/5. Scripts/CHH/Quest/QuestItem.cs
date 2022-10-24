using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour
{
    public string questKeyword;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && Input.GetButtonDown("Interaction"))
        {
            for(int i = 0; i < 3; i++)
            {
                if(other.GetComponent<Quest>().getQuestKeyword(i) == questKeyword)
                {
                    other.GetComponent<Quest>().setQuestProgressCount(i);
                    UIManager.Instance.questProgressTxt.text = other.GetComponent<Quest>().getQuestProgressCount(i).ToString() + " / " + other.GetComponent<Quest>().getQuestTargetCount(i).ToString();
                    UIManager.Instance.QuestProgressUpdate(i);
                    break;
                }
            }

            Destroy(gameObject);
        }
    }
}
