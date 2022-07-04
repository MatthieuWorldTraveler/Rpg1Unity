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
    public GameObject StartingPoint;

    public GameObject XpBar1;
    public GameObject XpBar2;
    public RectTransform BarProgress1;
    public RectTransform BarProgress2;

    public double rightScale1;
    public double rightScale2;
    public string lastScene;


    public float xpStats;
    public float goldStats;
    HeroCharCollision hcc;


    public TMP_Text goldTxt;
    Vector2 Lastpos;
    public Vector2 InitPos;
    string FirstLaunch;
    string[] PlayerStats;
    string sr;
    HeroFightScript hfs;
    public GameObject herofight;
    private void Start()
    {
        hfs = herofight.gameObject.GetComponent<HeroFightScript>();
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
        /*newtext = PlayerStats[7];
        sr = sr.Replace(newtext, hfs.vie.ToString());
        File.WriteAllText("PlayerStats.txt", sr);*/
        Debug.Log(sr);
    }

    public void ClearAllSaves()
    {
        string BaseStats = File.ReadAllText("PlayerStatsSaved.txt");
        File.WriteAllText("PlayerStats.txt", BaseStats);
        File.WriteAllText("FirstLaunch.txt", "NotSaved");
        sr = File.ReadAllText("PlayerStats.txt");
        Debug.Log("AllSavedCleared");
        Debug.Log(sr);
        PlayerPrefs.DeleteAll();
    }

    public void InitAllStats()
    {
        sr = File.ReadAllText("PlayerStats.txt");
        Debug.Log(sr);
        PlayerStats = sr.Split(";"[0]);
        goldStats = Convert.ToInt32(PlayerStats[3]);
        goldTxt.text = goldStats.ToString();
        xpStats = Convert.ToInt32(PlayerStats[4]);
        xpforlvlUp = Convert.ToDouble(PlayerStats[5]);
        lvlStats = Convert.ToInt32(PlayerStats[6]);
        lvlTxt.text = lvlStats.ToString();
        while(xpStats > xpforlvlUp)
        {
            xpStats -= Convert.ToInt32(xpforlvlUp);
            xpforlvlUp = Math.Ceiling(xpforlvlUp * 1.2f);
            lvlStats++;
        }
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
        //InitVie();
        sr = File.ReadAllText("PlayerStats.txt");
        Debug.Log(sr);



    }

    public void InitiateBaseStats()
    {
        sr = File.ReadAllText("PlayerStats.txt");
        PlayerStats = sr.Split(";"[0]);
        string newtext = PlayerStats[3];
        sr = sr.Replace(newtext, "0");
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
        /*newtext = PlayerStats[7];
        sr = sr.Replace(newtext, "10");
        File.WriteAllText("PlayerStats.txt", sr);*/
        Debug.Log(sr);
    }

    public void FirstInitStats()
    {
        transform.position = InitPos;
    }

    public void InitVie()
    {
        for (int i = 10; i > hfs.vie; i--)
        {
            hfs.PDV[hfs.vie - 1].SetActive(false);
            hcc.Pdvmainscreen[hfs.vie - 1].SetActive(false);
        }
    }
}
