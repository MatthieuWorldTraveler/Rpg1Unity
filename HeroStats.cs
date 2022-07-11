using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using System;

public class HeroStats : MonoBehaviour
{
    public double xpforlvlUp = 400;
    double xpmultiplicator1;
    double xpmultiplicator2;
    public int lvlStats = 1;
    public TMP_Text lvlTxt;
    public int LVlUpStats;
    public Animator lvlup;
    public GameObject StartingPoint;

    public GameObject XpBar1;
    public GameObject XpBar2;
    public RectTransform BarProgress1;
    public RectTransform BarProgress2;

    public double rightScale1;
    public double rightScale2;
    public string lastScene;


    public int xpStats;
    public int goldStats;
    HeroCharCollision hcc;


    public TMP_Text goldTxt;
    Vector2 Lastpos;
    public Vector2 InitPos;
    string FirstLaunch;
    string[] PlayerStats;
    string sr;
    HeroFightScript hfs;
    QuestObjective qo;
    public GameObject herofight;
    

    public int force;
    public int defense;
    public float xpMult;
    public float orMult;
    LvlStats lvls;
    public bool QuetesEncours;
    public bool Quetes1Over;
    public bool Quete1RewardOver;
    public int Queteitem;
    public GameObject Quetes1;

    private void Start()
    {
        lvls = gameObject.GetComponent<LvlStats>();
        hfs = herofight.GetComponent<HeroFightScript>();
        hcc = gameObject.GetComponent<HeroCharCollision>();
        XpSetup();
    }

    public void XpSetup()
    {
        xpmultiplicator1 = (76 - 3) / xpforlvlUp;
        xpmultiplicator2 = (78 - 6) / xpforlvlUp;

        rightScale1 = Math.Round((xpStats * xpmultiplicator1) - 76, 2);
        rightScale2 = Math.Round((xpStats * xpmultiplicator2) - 78, 2);

        BarProgress1.offsetMax = new Vector2(Convert.ToInt32(rightScale1), BarProgress1.offsetMax.y);
        BarProgress2.offsetMax = new Vector2(Convert.ToInt32(rightScale2), BarProgress2.offsetMax.y);
        goldTxt.text = goldStats.ToString();
        lvlTxt.text = lvlStats.ToString();
    }

    public void SaveAllStats()
    {
        string BaseStats = File.ReadAllText("PlayerStatsSaved.txt");
        File.WriteAllText("PlayerStats.txt", BaseStats);
        File.WriteAllText("FirstLaunch.txt", "Saved");
        sr = File.ReadAllText("PlayerStats.txt");
        PlayerStats = sr.Split(";"[0]);
        Lastpos = transform.position;
        string newtext = PlayerStats[1];
        sr = sr.Replace(newtext, Lastpos.x.ToString());
        File.WriteAllText("PlayerStats.txt", sr);
        newtext = PlayerStats[2];
        sr = sr.Replace(newtext, Lastpos.y.ToString());
        File.WriteAllText("PlayerStats.txt", sr);
        newtext = PlayerStats[3];
        sr = sr.Replace(newtext, goldStats.ToString());
        File.WriteAllText("PlayerStats.txt", sr);
        newtext = PlayerStats[4];
        sr = sr.Replace(newtext, xpStats.ToString());
        File.WriteAllText("PlayerStats.txt", sr);
        newtext = PlayerStats[5];
        sr = sr.Replace(newtext, xpforlvlUp.ToString());
        File.WriteAllText("PlayerStats.txt", sr);
        newtext = PlayerStats[6];
        sr = sr.Replace(newtext, lvlStats.ToString());
        File.WriteAllText("PlayerStats.txt", sr);
        newtext = PlayerStats[7];
        sr = sr.Replace(newtext, hfs.vie.ToString());
        File.WriteAllText("PlayerStats.txt", sr);
        newtext = PlayerStats[8];
        sr = sr.Replace(newtext, hfs.vieMax.ToString());
        File.WriteAllText("PlayerStats.txt", sr);
        newtext = PlayerStats[9];
        sr = sr.Replace(newtext, force.ToString());
        File.WriteAllText("PlayerStats.txt", sr);
        newtext = PlayerStats[10];
        sr = sr.Replace(newtext, defense.ToString());
        File.WriteAllText("PlayerStats.txt", sr);
        newtext = PlayerStats[11];
        sr = sr.Replace(newtext, xpMult.ToString());
        File.WriteAllText("PlayerStats.txt", sr);
        newtext = PlayerStats[12];
        sr = sr.Replace(newtext, orMult.ToString());
        File.WriteAllText("PlayerStats.txt", sr);
        newtext = PlayerStats[13];
        sr = sr.Replace(newtext, LVlUpStats.ToString());
        File.WriteAllText("PlayerStats.txt", sr);
        newtext = PlayerStats[14];
        sr = sr.Replace(newtext, QuetesEncours.ToString());
        File.WriteAllText("PlayerStats.txt", sr);
        newtext = PlayerStats[15];
        sr = sr.Replace(newtext, Quetes1Over.ToString());
        File.WriteAllText("PlayerStats.txt", sr);
        newtext = PlayerStats[16];
        sr = sr.Replace(newtext, Quete1RewardOver.ToString());
        File.WriteAllText("PlayerStats.txt", sr);
        newtext = PlayerStats[17];
        sr = sr.Replace(newtext, Queteitem.ToString());
        File.WriteAllText("PlayerStats.txt", sr);
        print(sr);
    }

    public void ClearAllSaves()
    {
        string BaseStats = File.ReadAllText("PlayerStatsSaved.txt");
        File.WriteAllText("PlayerStats.txt", BaseStats);
        File.WriteAllText("FirstLaunch.txt", "NotSaved");
        sr = File.ReadAllText("PlayerStats.txt");
        Debug.Log("AllSavedCleared");
        PlayerPrefs.DeleteAll();
    }

    public void InitAllStats()
    {
        sr = File.ReadAllText("PlayerStats.txt");
        PlayerStats = sr.Split(";"[0]);
        goldStats = Convert.ToInt32(PlayerStats[3]);
        goldTxt.text = goldStats.ToString();
        xpStats = Convert.ToInt32(PlayerStats[4]);
        xpforlvlUp = Convert.ToDouble(PlayerStats[5]);
        lvlStats = Convert.ToInt32(PlayerStats[6]);
        lvlTxt.text = lvlStats.ToString();
        LVlUpStats = Convert.ToInt32(PlayerStats[13]);
        hfs = herofight.GetComponent<HeroFightScript>();
        defense = Convert.ToInt32(PlayerStats[10]);
        hfs.defDisplay.text = defense + " %";
        QuetesEncours = Convert.ToBoolean(PlayerStats[14]);
        Quetes1Over = Convert.ToBoolean(PlayerStats[15]);
        Quete1RewardOver = Convert.ToBoolean(PlayerStats[16]);
        Queteitem = Convert.ToInt32(PlayerStats[17]);
        qo = gameObject.GetComponent<QuestObjective>();
        qo.QuestItemQtyFleur.text = Queteitem.ToString();
        while (xpStats > xpforlvlUp)
        {
            xpStats -= Convert.ToInt32(xpforlvlUp);
            xpforlvlUp = Math.Ceiling(xpforlvlUp * 1.2f);
            lvlStats++;
            LVlUpStats++;
        }
        if (LVlUpStats > 0)
            lvlUpInit();
        XpSetup();
        FirstLaunch = File.ReadAllText("FirstLaunch.txt");
        if (FirstLaunch == "NotSaved")
            FirstInitStats();
        else
        {
            string newmap = PlayerPrefs.GetString("NewMap");
            if (newmap == "false")
            {
                float x = float.Parse(PlayerStats[1]);
                float y = float.Parse(PlayerStats[2]);
                transform.position = new Vector2(x, y);
            }
            else if (newmap == "true")
            {
                string point = PlayerPrefs.GetString("Point");
                Vector2 teleportPoint = GameObject.Find(point).transform.position;
                transform.position = teleportPoint;
                PlayerPrefs.SetString("NewMap", "false");
            }
        }
        hcc = gameObject.GetComponent<HeroCharCollision>();
        hfs.vie = Convert.ToInt32(PlayerStats[7]);
        hfs.vieMax = Convert.ToInt32(PlayerStats[8]);
        force = Convert.ToInt32(PlayerStats[9]);
        hfs.forceDisplay.text = "1 - " + force;
        xpMult = Convert.ToInt32(PlayerStats[11]);
        orMult = Convert.ToInt32(PlayerStats[12]);
        hfs.PDV.text = "X " + hfs.vie;
        hcc.Pdvmainscreen.text = "X " + hfs.vie;    
    }

    public void InitiateBaseStats()
    {
        sr = File.ReadAllText("PlayerStats.txt");
        PlayerStats = sr.Split(";"[0]);
        string newtext = PlayerStats[3];
        sr = sr.Replace(newtext, "10");
        File.WriteAllText("PlayerStats.txt", sr);
        newtext = PlayerStats[4];
        sr = sr.Replace(newtext, "0");
        File.WriteAllText("PlayerStats.txt", sr);
        newtext = PlayerStats[5];
        sr = sr.Replace(newtext, "400");
        File.WriteAllText("PlayerStats.txt", sr);
        newtext = PlayerStats[6];
        sr = sr.Replace(newtext, "1");
        File.WriteAllText("PlayerStats.txt", sr);
        newtext = PlayerStats[7];
        sr = sr.Replace(newtext, "10");
        File.WriteAllText("PlayerStats.txt", sr);
        newtext = PlayerStats[8];
        sr = sr.Replace(newtext, "10");
        File.WriteAllText("PlayerStats.txt", sr);
        newtext = PlayerStats[9];
        sr = sr.Replace(newtext, "2");
        File.WriteAllText("PlayerStats.txt", sr);
        newtext = PlayerStats[10];
        sr = sr.Replace(newtext, "0");
        File.WriteAllText("PlayerStats.txt", sr);
        newtext = PlayerStats[11];
        sr = sr.Replace(newtext, "1");
        File.WriteAllText("PlayerStats.txt", sr);
        newtext = PlayerStats[12];
        sr = sr.Replace(newtext, "1");
        File.WriteAllText("PlayerStats.txt", sr);
        newtext = PlayerStats[13];
        sr = sr.Replace(newtext, "0");
        File.WriteAllText("PlayerStats.txt", sr);
        newtext = PlayerStats[14];
        sr = sr.Replace(newtext, "false");
        File.WriteAllText("PlayerStats.txt", sr);
        newtext = PlayerStats[15];
        sr = sr.Replace(newtext, "false");
        File.WriteAllText("PlayerStats.txt", sr);
        newtext = PlayerStats[16];
        sr = sr.Replace(newtext, "false");
        File.WriteAllText("PlayerStats.txt", sr);
        newtext = PlayerStats[17];
        sr = sr.Replace(newtext, "0");
        File.WriteAllText("PlayerStats.txt", sr);
        PlayerPrefs.SetString("NewMap", "false");
    }

    public void FirstInitStats()
    {
        transform.position = InitPos;
    }

    public void lvlUpInit()
    {
        lvlup.SetBool("lvlUp", true);
        if (LVlUpStats == 1)
            lvls.Statsrestant.text = LVlUpStats + " restant";
        else if (LVlUpStats > 1)
            lvls.Statsrestant.text = LVlUpStats + " restants";
        lvls.ForceBtn.SetActive(true);
        if(defense < 50)
            lvls.defenseBtn.SetActive(true);
        lvls.vieBtn.SetActive(true);
        lvls.xpBtn.SetActive(true);
        lvls.orBtn.SetActive(true);
    }

    public void lvlUpOver()
    {
        lvlup.SetBool("lvlUp", false);
        lvls.Statsrestant.text = "0 restant";
        lvls.ForceBtn.SetActive(false);
        lvls.defenseBtn.SetActive(false);
        lvls.vieBtn.SetActive(false);
        lvls.xpBtn.SetActive(false);
        lvls.orBtn.SetActive(false);
    }

    public void ForceUp()
    {
        LVlUpStats--;
        force++;
        lvls.Force.text = force.ToString();
        hfs.forceDisplay.text = "1 - " + force;
        if (LVlUpStats > 0)
            lvlUpInit();
        else
            lvlUpOver();
    }

    public void VieUp()
    {
        LVlUpStats--;
        hfs.vieMax++;
        hfs.vie++;
        lvls.vie.text = hfs.vieMax.ToString();
        hfs.PDV.text = "X " + hfs.vie;
        hcc.Pdvmainscreen.text = "X " + hfs.vie;
        if (LVlUpStats > 0)
            lvlUpInit();
        else
            lvlUpOver();
    }

    public void defenseUp()
    {
        LVlUpStats--;
        defense++;
        lvls.defense.text = defense.ToString();
        if(defense > 49)    
            lvls.defenseBtn.SetActive(false);
        if (LVlUpStats > 0)
            lvlUpInit();
        else
            lvlUpOver();
    }
}
