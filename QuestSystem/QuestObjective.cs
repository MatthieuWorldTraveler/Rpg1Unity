using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestObjective : MonoBehaviour
{
    public int QuestId;
    Collider2D otherObj;
    public GameObject InteractBtn;
    public int QuestNbr = 0;
    QuestTag QuestItemOn;
    QuestGiver qg;
    int NbrItemMax;

    public GameObject dialWorldSpace;
    public TMP_Text dialTxt;
    public TMP_Text QuestItemQtyFleur;
    public TMP_Text QuestItemQtyShell;
    public bool queteFinit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        
        if (collision.gameObject.tag == "Flower")
        {
            NbrItemMax = collision.GetComponentInParent<QuestGiver>().quest.ItemQuest;
            QuestItemOn = collision.gameObject.GetComponent<QuestTag>();
            if (QuestItemOn.ItemQuestOn && !queteFinit)
            {
                otherObj = collision;
                InteractBtn.SetActive(true);
            }
        }
        if (collision.gameObject.tag == "shell")
        {
            NbrItemMax = collision.GetComponentInParent<QuestGiver>().quest.ItemQuest;
            QuestItemOn = collision.gameObject.GetComponent<QuestTag>();
            if (QuestItemOn.ItemQuestOn && !queteFinit)
            {
                otherObj = collision;
                InteractBtn.SetActive(true);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Flower")
        {
            InteractBtn.SetActive(false);
            otherObj = null;
        }
        if (collision.gameObject.tag == "shell")
        {
            InteractBtn.SetActive(false);
            otherObj = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && otherObj != null)
        {
            if (otherObj.gameObject.tag == "Flower")
            {
                if (QuestItemOn.ItemQuestOn && !queteFinit)
                {
                    otherObj.gameObject.SetActive(false);
                    QuestNbr++;
                    QuestItemQtyFleur.text = QuestNbr.ToString();
                    IsQuestOver();
                }
            }
            else if (otherObj.gameObject.tag == "shell")
            {
                if (QuestItemOn.ItemQuestOn && !queteFinit)
                {
                    otherObj.gameObject.SetActive(false);
                    QuestNbr++;
                    QuestItemQtyShell.text = QuestNbr.ToString();
                    IsQuestOver();
                }
            }
        }
    }

    void IsQuestOver()
    {
            if (QuestNbr == NbrItemMax)
            {
                Debug.Log("QuestOver");
                dialTxt.SetText("Quête finis");
                dialWorldSpace.SetActive(true);
                Invoke("HideQuestOver", 2);
                queteFinit = true;
                QuestNbr = 0;
            /*if (otherObj.gameObject.tag == "Flower")
                    quete1Finit = true;
            if (otherObj.gameObject.tag == "shell")
                    quete2Finit = true;*/
            }
            else
                Debug.Log("QuestNotOver");

            /*if (quete1Finit || quete2Finit)
                queteFinit = true;*/
    }

    void HideQuestOver()
    {
        dialWorldSpace.SetActive(false);
    }
}
