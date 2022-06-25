using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public GameObject questItem;
    public TMP_Text questItemQty;
    public QuestOver questOver;
    public int currentQty = 0;

    public GameObject questPanl;
    public GameObject questPanelOver;

    public Text[] questInfos;
    public Text[] questInfosOver;
    public GameObject[] toHideAfterQuestCompleted;
    public GameObject[] toShowAfterQuestCompleted;



    public void HideObjectAfterQuest()
    {
        foreach (GameObject go in toHideAfterQuestCompleted)
        {
            go.SetActive(false);
        }
    }

    public void ShowObjectAfterQuestTaken()
    {
        foreach (GameObject go in toShowAfterQuestCompleted)
        {
            go.SetActive(true);
        }
    }
}
