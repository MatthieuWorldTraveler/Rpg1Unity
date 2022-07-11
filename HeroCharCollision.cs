using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

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
    public GameObject Encounter;
    public float goldKeep = 0;
    public float xpKeep = 0;
    public TMP_Text Pdvmainscreen;
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
    public string SceneIn;
    public string SceneOut;
    HeroBehaviour hb;
    public bool NewMap;
    public GameObject lvlup;
    Vector2 Lastpos;
    public bool AnimOn;
    BeachBehavior beach;
    public GameObject bored;
    public GameObject boredRot;
    public SpriteRenderer hero;
    public AudioClip SignBtn;
    public AudioClip heal;
    public AudioClip drowning;
    public AudioClip gold;

    private void Start()
    {
        
        hstats = gameObject.GetComponent<HeroStats>();
        hstats.InitAllStats();
    }

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
            PlayerPrefs.SetString("NewMap", "true");
            hstats.SaveAllStats();
            string point = other.gameObject.GetComponent<ExitBehaviour>().teleportPoint;
            PlayerPrefs.SetString("Point", point);
            SceneManager.LoadScene(other.gameObject.name);
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
            camFight.SetActive(true);
            mb = other.GetComponent<MobBehaviour>();
            InitFight initF = camFight.GetComponent<InitFight>();
            initF.hfs.baseEnemy = other.gameObject;
            initF.initFight();
            if (inv.ScreenStats.activeInHierarchy)
            {
                inv.ScreenStats.SetActive(false);
                inv.invState = false;
                Debug.Log("StatsCachéAuto");
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
        if (other.gameObject.tag == "Beach")
        {
            AnimOn = true;
            otherObj = other;
            beach = otherObj.GetComponent<BeachBehavior>();
            StartCoroutine("Drowning");
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
        if(Input.GetKeyUp(KeyCode.E) && otherObj != null && !AnimOn)
        {
            if (otherObj.gameObject.tag == "sign")
            {
                GetComponent<AudioSource>().PlayOneShot(SignBtn);
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
        if(Input.GetKeyUp(KeyCode.Escape) && !AnimOn)
        {
            hb = gameObject.GetComponent<HeroBehaviour>();
            PlayerPrefs.SetString("LastScene", SceneIn);
            hstats.SaveAllStats();
            SceneManager.LoadScene("X_PauseMenu");
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
        GetComponent<AudioSource>().PlayOneShot(gold);
        Destroy(otherObj.gameObject);
        dialCanvas.SetActive(true);
        mb = Encounter.GetComponent<MobBehaviour>();
        for (int i = 1; i < killchain; i++)
            killchainBonus += 0.15f;
        if(killchain > 1)
            dialcanvasTxt.text = $"Tu trouves {goldKeep} pièces d'or et {xpKeep} points d'expériences KillStreak x{killchainBonus}";
        else
            dialcanvasTxt.text = $"Tu trouves {goldKeep} pièces d'or et {xpKeep} points d'expériences";
        Invoke("HidePnjDialPanel", 4);
        IsLootHere = false;
        hstats = gameObject.GetComponent<HeroStats>();
        hstats.goldStats += Convert.ToInt32(goldKeep * killchainBonus);
        hstats.xpStats += Convert.ToInt32(xpKeep * killchainBonus);
        lvlUp();
        hstats.XpSetup();
        goldKeep = 0;
        xpKeep = 0;
        killchain = 0;
        killchainBonus = 1;
        hstats.SaveAllStats();
    }

    public void lvlUp()
    {
        while(hstats.xpStats > hstats.xpforlvlUp)
        {
            lvlup.SetActive(true);
            Invoke("HidelvlUpPanel", 3);
            hstats.xpStats -= Convert.ToInt32(hstats.xpforlvlUp);
            hstats.xpforlvlUp = Math.Ceiling(hstats.xpforlvlUp * 1.2f);
            hstats.lvlStats++;
            hstats.LVlUpStats++;
            hfs.vie = hfs.vieMax;
            Pdvmainscreen.text = "X " + hfs.vie;
        }
        if (hstats.LVlUpStats > 0)
            hstats.lvlUpInit();
    }

    public void Heal()
    {
        hfs = HeroFight.gameObject.GetComponent<HeroFightScript>();
        HealCanvas.SetActive(false);
        hstats = gameObject.GetComponent<HeroStats>();
        if (hstats.goldStats >= 50 && hfs.vie < hfs.vieMax)
        {
            GetComponent<AudioSource>().PlayOneShot(heal);
            hstats.goldStats -= 50;
            hstats.goldTxt.text = hstats.goldStats.ToString();
            hfs.vie = hfs.vieMax;
            hfs.PDV.text = "X " + hfs.vie;
            Pdvmainscreen.text = "X " + hfs.vie;
            
        }
        else if (hfs.vie == hfs.vieMax)
        {
            dialWorldSpace.SetActive(true);
            dialTxt.text = "Tu as déjà toute ta vie !";
            Invoke("HideDialPanel", 2);
        }
        else
        {
            dialWorldSpace.SetActive(true);
            dialTxt.text = "Pas assez d'or en stock !";
            Invoke("HideDialPanel", 2);
        }
        HealCanvas.SetActive(false);
    }

    public void HideHealPanel()
    {
        HealCanvas.SetActive(false);
    }

    public void HidelvlUpPanel()
    {
        lvlup.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        hstats.SaveAllStats();
    }

    IEnumerator DamageAnim()
    {
        hero.enabled = false;
        yield return new WaitForSeconds(0.05f);
        hero.enabled = true;
        yield return new WaitForSeconds(0.05f);
        hero.enabled = false;
        yield return new WaitForSeconds(0.05f);
        hero.enabled = true;
        yield return new WaitForSeconds(0.05f);
        hero.enabled = false;
        yield return new WaitForSeconds(0.05f);
        hero.enabled = true;
        yield return new WaitForSeconds(0.05f);
    }

    IEnumerator Drowning()
    {
        GetComponent<AudioSource>().PlayOneShot(drowning);
        yield return new WaitForSeconds(0.5f);
        transform.position = beach.respawn.transform.position;
        yield return new WaitForSeconds(0.05f);
        StartCoroutine("DamageAnim");
        yield return new WaitForSeconds(0.5f);
        StartCoroutine("Bored");
        yield return new WaitForSeconds(0.15f);
    }

    IEnumerator Bored()
    {
        bored.SetActive(true);
        boredRot.transform.Rotate(0, 0, 25f);
        yield return new WaitForSeconds(0.2f);
        boredRot.transform.Rotate(0, 0, 0);
        yield return new WaitForSeconds(0.2f);
        boredRot.transform.Rotate(0, 0, 25f);
        yield return new WaitForSeconds(0.2f);
        boredRot.transform.Rotate(0, 0, 0);
        yield return new WaitForSeconds(0.2f);
        boredRot.transform.Rotate(0, 0, 25f);
        yield return new WaitForSeconds(0.2f);
        boredRot.transform.Rotate(0, 0, 0);
        yield return new WaitForSeconds(0.2f);
        bored.SetActive(false);
        AnimOn = false;
    }
}
