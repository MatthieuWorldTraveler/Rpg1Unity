using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestObjective : MonoBehaviour
{
    Collider2D otherObj;
    public GameObject InteractBtn;
    QuestTag QuestItemOn;
    QuestGiver qg;
    HeroStats hs;
    int NbrItemMax;
    public AudioClip itempick;

    public GameObject dialWorldSpace;
    public TMP_Text dialTxt;
    public TMP_Text QuestItemQtyFleur;
    public AudioClip bell;

    private void Start()
    {
        hs = gameObject.GetComponent<HeroStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Flower")
        {
            NbrItemMax = collision.GetComponentInParent<QuestGiver>().quest.ItemQuest;
            QuestItemOn = collision.gameObject.GetComponent<QuestTag>();
            if (QuestItemOn.ItemQuestOn && !hs.Quetes1Over)
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && otherObj != null)
        {
            if (otherObj.gameObject.tag == "Flower")
            {
                if (QuestItemOn.ItemQuestOn && !hs.Quetes1Over)
                {
                    GetComponent<AudioSource>().PlayOneShot(itempick);
                    otherObj.gameObject.SetActive(false);
                    hs.Queteitem++;
                    QuestItemQtyFleur.text = hs.Queteitem.ToString();
                    IsQuestOver();
                }
            }           
        }
    }

    void IsQuestOver()
    {
            if (hs.Queteitem == NbrItemMax)
            {
                GetComponent<AudioSource>().PlayOneShot(bell);
                dialTxt.SetText("Quête finie");
                dialWorldSpace.SetActive(true);
                Invoke("HideQuestOver", 2);
                hs.Quetes1Over = true;
            }
    }

    void HideQuestOver()
    {
        dialWorldSpace.SetActive(false);
    }
}
