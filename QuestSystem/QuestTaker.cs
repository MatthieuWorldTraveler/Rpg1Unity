using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class QuestTaker : MonoBehaviour
{
    QuestGiver qg;
    QuestTag QuestActive;
    Collider2D otherObj;
    public int flowerCount = 16;
    public bool IsOver;
    public GameObject dialCanvas;
    public Text dialcanvasTxt;
    public int QuestD = 0;
    int QId;
    int QiDOngoing = 0;
    public int QuetesEncours = 0;
    bool Q1Done;
    HeroStats hstats;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Quest_Giver")
        {
            QId = 0;
            QId = collision.GetComponent<QuestGiver>().quest.QuestId;
            if (QiDOngoing == 0)
                QiDSetup();
            otherObj = collision;
            if (qg == null)
                qg = otherObj.gameObject.GetComponent<QuestGiver>();
            otherObj.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Quest_Giver")
        {
            otherObj.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            qg.questPanl.SetActive(false);
            otherObj = null;
            qg = null;
        }
    }
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.E) && otherObj != null && otherObj.gameObject.tag == "Quest_Giver")
        {
            otherObj.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            IsOver = gameObject.GetComponent<QuestObjective>().queteFinit;
            if (!IsOver)
            {
                showQuest();
                if (qg.quest.isActive)
                {
                    qg.questPanl.gameObject.transform.GetChild(17).gameObject.SetActive(false);
                    qg.questPanl.gameObject.transform.GetChild(16).gameObject.SetActive(true);
                }
            }
            else
            {
                if (QId == QuetesEncours || QuetesEncours == 0)
                {
                    if (QuestD == 1)
                    {
                        HeroCharCollision hcc = gameObject.GetComponent<HeroCharCollision>();
                        Debug.Log("QueteFinie");
                        qg.questPanelOver.SetActive(true);
                        qg.questInfosOver[0].text = qg.questOver.title;
                        qg.questInfosOver[1].text = qg.questOver.description;
                        qg.questInfosOver[2].text = $"XP: {qg.quest.xp} | Gold: {qg.quest.gold}";
                        qg.questItem.SetActive(false);
                        hstats = gameObject.GetComponent<HeroStats>();
                        hstats.goldStats += qg.quest.gold;
                        hstats.goldTxt.SetText(hstats.goldStats.ToString());
                        hstats.xpStats += qg.quest.xp;
                        while (hstats.xpStats > hstats.xpforlvlUp)
                        {
                            hcc.lvlUp();
                        }
                        hstats.XpSetup();
                        QuestD = 2;
                        gameObject.GetComponent<QuestObjective>().queteFinit = false;
                        QuetesEncours = 0;
                        otherObj.gameObject.tag = "smiley";
                        QiDOngoing = 0;
                        qg.quest.isActive = false;
                        if (QId == 1)
                        {
                            Q1Done = true;
                            for (int i = 1; i < 16 + 1; i++)
                                otherObj.gameObject.transform.GetChild(i).GetComponent<QuestTag>().ItemQuestOn = false;
                        }
                        else if (QId == 2)
                        {
                            qg.HideObjectAfterQuest();
                            qg.ShowObjectAfterQuestTaken();
                            for (int i = 1; i < 16 + 1; i++)
                                otherObj.gameObject.transform.GetChild(i).GetComponent<QuestTag>().ItemQuestOn = false;
                        }
                        QId = 0;
                    }
                    else if (QuestD == 2)
                    {
                        Debug.Log("Quête deja finie");
                        dialcanvasTxt.text = "Merci pour ton aide !";
                        dialCanvas.SetActive(true);
                        Invoke("HideQuestDone", 2);
                        QuestD = 0;
                        otherObj.gameObject.tag = "Untagged";
                    }
                }
                else
                {
                    qg.questPanl.SetActive(false);
                    dialCanvas.SetActive(true);
                    dialcanvasTxt.text = "Quête déjà en cours reviens quand tu auras fini !";
                    Invoke("HideQuestDone", 2);
                }
            }  
        }
    }

    private void showQuest()
    {
        if (QId == 1 || Q1Done)
        {
            qg = otherObj.gameObject.GetComponent<QuestGiver>();
            qg.questPanl.SetActive(true);
            qg.questInfos[0].text = qg.quest.title;
            qg.questInfos[1].text = qg.quest.description;
            qg.questInfos[2].text = $"XP: {qg.quest.xp} | Gold: {qg.quest.gold}";
            qg.questInfos[3].text = qg.quest.objectif;
            qg.questPanl.gameObject.transform.GetChild(6).gameObject.SetActive(true);
        }
        else
        {
            qg.questPanl.SetActive(false);
            dialCanvas.SetActive(true);
            dialcanvasTxt.text = "Quête non disponible pour le moment";
            Invoke("HideQuestDone", 2);
        }
    }

    public void takeQuest()
    {
        if (QuetesEncours != QiDOngoing)
        {
            if (QId == 1)
            {
                for (int i = 1; i < 16 + 1; i++)
                    otherObj.gameObject.transform.GetChild(i).GetComponent<QuestTag>().ItemQuestOn = true;
            }
            else if (QId == 2)
            {
                for (int i = 1; i < 13 + 1; i++)
                    otherObj.gameObject.transform.GetChild(i).GetComponent<QuestTag>().ItemQuestOn = true;
            }
            QuetesEncours = QId;
            qg.quest.isActive = true;
            qg.questPanl.SetActive(false);
            qg.questItem.SetActive(true);
            QuestD = 1;
            QiDSetup();
        }
        else
        {
            qg.questPanl.SetActive(false);
            dialCanvas.SetActive(true);
            dialcanvasTxt.text = "Quête déjà en cours reviens quand tu auras fini !";
            Invoke("HideQuestDone", 2);
        }
    }

    public void hideQuest()
    {
        qg.questPanl.SetActive(false);
    }

    public void hideQuestOver()
    {
        qg.questPanelOver.SetActive(false);
    }

    public void HideQuestDone()
    {
        dialCanvas.SetActive(false);
    }

    public void QiDSetup()
    {
        QiDOngoing = QId;
    }
}
