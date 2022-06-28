using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroCharCollision : MonoBehaviour
{
    public GameObject dialWorldSpace;
    public TMP_Text dialTxt;
    Collider2D otherObj;
    public SpriteRenderer spriteRenderer;
    public GameObject dialCanvas;
    public Text dialcanvasTxt;
    public GameObject camFight;
    MobBehaviour mb;
    InventoryV1 inv;
    GameObject Encounter;
    public int goldKeep = 0;
    public int xpKeep = 0;
    public GameObject[] Pdvmainscreen;
    Collider2D LootRemind;
    public bool IsLootHere;
    public int killchain = 0;
    float killchainBonus = 1;
    public SpriteRenderer bag;
    public Sprite bagopen;
    public Sprite bagclose;
    HeroStats hstats;
    public GameObject HealCanvas;
    HeroFightScript hfs;
    QuestGiver qg;
    public GameObject HeroFight;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "sign")
        {
            otherObj = other;
            otherObj.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            // dialWorldSpace.SetActive(true);
            // dialTxt.text = sb.signText;
            //dialTxt.SetText(sb.signText);

        }
        if (other.gameObject.tag == "exit")
        {
            string point = other.gameObject.GetComponent<ExitBehaviour>().teleportPoint;
            PlayerPrefs.SetString("Point", point);
            Application.LoadLevel(other.gameObject.name);
        }
        if (other.gameObject.tag == "smiley")
        {
            PnjSmiley smiley = other.gameObject.GetComponent<PnjSmiley>();
            smiley.Smiley.SetActive(true);
        }
        if (other.gameObject.tag == "mob" && !camFight.activeInHierarchy)
        {
            Encounter = other.gameObject;
            inv = gameObject.GetComponent<InventoryV1>();
            print("Combat !");
            camFight.SetActive(true);
            mb = other.GetComponent<MobBehaviour>();
            InitFight initF = camFight.GetComponent<InitFight>();
            initF.hfs.baseEnemy = other.gameObject;
            initF.initFight();
            if (inv.ScreenStats.activeInHierarchy)
            {
                inv.ScreenStats.SetActive(false);
                inv.invState = false;
                Debug.Log("StatsCach�Auto");
            }
        }
        if (other.gameObject.tag == "LootBag")
        {
            mb = other.GetComponentInParent<MobBehaviour>();
            otherObj = other;
            if (LootRemind != null)
                LootRemind = otherObj;
            otherObj.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            bag.sprite = bagopen;
        }
        if (other.gameObject.tag == "Healer")
        {
            otherObj = other;
            otherObj.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "garde")
        {
            dialCanvas.SetActive(true);
            dialcanvasTxt.text = other.gameObject.GetComponent<PnjSimpleDial>().PnjDial;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "garde")
        {
            Invoke("HidePnjDialPanel", 2);
        }      
    }

    void HidePnjDialPanel()
    {
        dialCanvas.SetActive(false);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "sign")
        {
            SignBehaviour sb = otherObj.gameObject.GetComponent<SignBehaviour>();
            sb.ui.SetActive(false);
            //HideDialPanel();
            otherObj = null;
            Invoke("HideDialPanel", 1);
        }
        if (other.gameObject.tag == "smiley")
        {
            PnjSmiley smiley = other.gameObject.GetComponent<PnjSmiley>();
            smiley.Smiley.SetActive(false);
        }
        if (other.gameObject.tag == "LootBag")
        {
            otherObj.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            bag.sprite = bagclose;

        }
        if (other.gameObject.tag == "Healer")
        {
            otherObj.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            otherObj = null;
        }

    }

    void HideDialPanel()
    {
        dialWorldSpace.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.E) && otherObj != null)
        {
            if (otherObj.gameObject.tag == "sign")
            {
                otherObj.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                ShowDial();
            }
            if (otherObj.gameObject.tag == "LootBag")
            {
                otherObj.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                ShowLoot();
            }
            if (otherObj.gameObject.tag == "Healer")
            {
                qg = otherObj.gameObject.GetComponent<QuestGiver>();
                otherObj.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                HealCanvas.SetActive(true);
                qg.questInfos[0].text = qg.quest.title;
                qg.questInfos[1].text = qg.quest.description;
            }
        }
    }

    public void ShowDial()
    {
        SignBehaviour sb = otherObj.gameObject.GetComponent<SignBehaviour>();
        dialTxt.SetText(sb.signText);
        dialWorldSpace.SetActive(true);
    }

    public void ShowLoot()
    {
        Destroy(otherObj.gameObject);
        dialCanvas.SetActive(true);
        mb = Encounter.GetComponent<MobBehaviour>();
        for (int i = 1; i < killchain; i++)
            killchainBonus += 0.15f;
        if(killchain > 1)
            dialcanvasTxt.text = $"Tu trouves {goldKeep} pi�ces d'or et {xpKeep} points d'exp�riences KillStreak x{killchainBonus}";
        else
            dialcanvasTxt.text = $"Tu trouves {goldKeep} pi�ces d'or et {xpKeep} points d'exp�riences";
        Invoke("HidePnjDialPanel", 4);
        IsLootHere = false;
        print(goldKeep*killchainBonus);
        print(Convert.ToInt32(goldKeep * killchainBonus));
        hstats = gameObject.GetComponent<HeroStats>();
        hstats.goldStats += Convert.ToInt32(goldKeep * killchainBonus);
        hstats.xpStats += Convert.ToInt32(xpKeep * killchainBonus);
        if (hstats.xpStats > hstats.xpforlvlUp)
        {
            print("Lvl up !");
            hstats.xpStats -= Convert.ToInt32(hstats.xpforlvlUp);
            hstats.xpforlvlUp = hstats.xpforlvlUp * 2.5f;
        }
        else
            print("No Level up");
        hstats.XpSetup();
        print(goldKeep);
        print(xpKeep);
        goldKeep = 0;
        xpKeep = 0;
        print(goldKeep);
        print(xpKeep);
        killchain = 0;
        killchainBonus = 1;
    }

    public void Heal()
    {
        hfs = HeroFight.gameObject.GetComponent<HeroFightScript>();
        HealCanvas.SetActive(false);
        hstats = gameObject.GetComponent<HeroStats>();
        if (hstats.goldStats >= 50 && hfs.vie < hfs.vieMax)
        {
            print("Heal possible");
            hstats.goldStats -= 50;
            hstats.goldTxt.text = hstats.goldStats.ToString();
            for (int i = hfs.vie; i < hfs.vieMax; i++)
            {
                hfs.vie++;
                hfs.PDV[hfs.vie-1].SetActive(true);
                Pdvmainscreen[hfs.vie-1].SetActive(true);
            }
        }
        else if (hfs.vie == hfs.vieMax)
        {
            print("toute sa vie");
            dialWorldSpace.SetActive(true);
            dialTxt.text = "Tu as d�j� toute ta vie !";
            Invoke("HideDialPanel", 2);
        }
        else
        {
            print("Pas d'or");
            dialWorldSpace.SetActive(true);
            dialTxt.text = "Pas assez d'or en stock !";
            Invoke("HideDialPanel", 2);
        }
        print("Fin du heal");
        HealCanvas.SetActive(false);
    }

    public void HideHealPanel()
    {
        HealCanvas.SetActive(false);
    }

}
