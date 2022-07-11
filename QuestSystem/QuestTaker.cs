using UnityEngine;
using UnityEngine.UI;

public class QuestTaker : MonoBehaviour
{
    QuestGiver qg;
    QuestTag QuestActive;
    Collider2D otherObj;
    public int flowerCount = 16;
    public GameObject dialCanvas;
    public Text dialcanvasTxt;
    public GameObject itemquest;
    public GameObject npc;
    HeroStats hstats;

    private void Start()
    {
        hstats = gameObject.GetComponent<HeroStats>();
        if (hstats.QuetesEncours)
            takeQuest();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Quest_Giver" && !hstats.Quete1RewardOver)
        {
            otherObj = collision;
            if (qg == null)
                qg = otherObj.gameObject.GetComponent<QuestGiver>();
            otherObj.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Quest_Giver" && !hstats.Quete1RewardOver)
        {
            otherObj.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            qg.questPanl.SetActive(false);
            otherObj = null;
            qg = null;
        }
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && otherObj != null && otherObj.gameObject.tag == "Quest_Giver" && !hstats.Quete1RewardOver)
        {
            hstats = gameObject.GetComponent<HeroStats>();
            otherObj.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            if (!hstats.Quetes1Over)
            {
                showQuest();
                if (hstats.QuetesEncours)
                {
                    qg.questPanl.gameObject.transform.GetChild(17).gameObject.SetActive(false);
                    qg.questPanl.gameObject.transform.GetChild(16).gameObject.SetActive(true);
                    GetComponent<AudioSource>().PlayOneShot(qg.Uhm);
                }
                else
                    GetComponent<AudioSource>().PlayOneShot(qg.Greetings);
            }
            else
            {
                GetComponent<AudioSource>().PlayOneShot(qg.Farewell);
                HeroCharCollision hcc = gameObject.GetComponent<HeroCharCollision>();
                qg.questPanelOver.SetActive(true);
                qg.questInfosOver[0].text = qg.questOver.title;
                qg.questInfosOver[1].text = qg.questOver.description;
                qg.questInfosOver[2].text = $"XP: {qg.quest.xp} | Gold: {qg.quest.gold}";
                qg.questItem.SetActive(false);
                hstats.goldStats += qg.quest.gold;
                hstats.goldTxt.SetText(hstats.goldStats.ToString());
                hstats.xpStats += qg.quest.xp;
                while (hstats.xpStats > hstats.xpforlvlUp)
                {
                    hcc.lvlUp();
                }
                hstats.XpSetup();
                hstats.Quete1RewardOver = true;
                hstats.Queteitem = 0;
                otherObj.gameObject.tag = "smiley";
                qg.quest.isActive = false;
                for (int i = 1; i < 16 + 1; i++)
                    otherObj.transform.GetChild(i).GetComponent<QuestTag>().ItemQuestOn = false;
            }
        }
    }

    private void showQuest()
    {
        qg = otherObj.gameObject.GetComponent<QuestGiver>();
        qg.questPanl.SetActive(true);
        qg.questInfos[0].text = qg.quest.title;
        qg.questInfos[1].text = qg.quest.description;
        qg.questInfos[2].text = $"XP: {qg.quest.xp} | Gold: {qg.quest.gold}";
        qg.questInfos[3].text = qg.quest.objectif;
        qg.questPanl.gameObject.transform.GetChild(6).gameObject.SetActive(true);
    }

    public void takeQuest()
    {
        qg = npc.gameObject.GetComponent<QuestGiver>();
        for (int i = 1; i < 16+1; i++)
            itemquest.transform.GetChild(i).GetComponent<QuestTag>().ItemQuestOn = true;
        hstats.QuetesEncours = true;
        qg.questPanl.SetActive(false);
        qg.questItem.SetActive(true);
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
}
