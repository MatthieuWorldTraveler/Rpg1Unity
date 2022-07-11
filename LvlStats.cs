using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvlStats : MonoBehaviour
{
    public GameObject stats;
    HeroCharCollision hcc;
    public Text Force;
    public GameObject ForceBtn;
    public Text defense;
    public GameObject defenseBtn;
    public Text vie;
    public GameObject vieBtn;
    public Text xp;
    public GameObject xpBtn;
    public Text or;
    public GameObject orBtn;
    public Text Statsrestant;
    HeroFightScript hfs;
    HeroStats hs;
    public GameObject herofight;

    private void Start()
    {
        hs = GetComponent<HeroStats>();
        hfs = herofight.GetComponent<HeroFightScript>();
        hcc = gameObject.GetComponent<HeroCharCollision>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && hcc.AnimOn)
        {
            closeStats();
        }
    }

    public void openStats()
    {
        stats.SetActive(true);
        hcc.AnimOn = true;
        vie.text = hfs.vieMax.ToString();
        Force.text = hs.force.ToString();
        defense.text = hs.defense.ToString();
        xp.text = hs.xpMult.ToString();
        or.text = hs.orMult.ToString();
    }

    public void closeStats()
    {
        stats.SetActive(false);
        hcc.AnimOn = false;
    }
}
