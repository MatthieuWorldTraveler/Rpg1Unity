using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitFight : MonoBehaviour
{
    public GameObject mob;
    EnemyScript es;
    public HeroFightScript hfs;
    MobBehaviour mb;
    HeroCharCollision hcc;
    public GameObject heromainscreen;

    private void Start()
    {
        hcc = heromainscreen.GetComponent<HeroCharCollision>();
    }

    public void initFight()
    {
        mb = hfs.baseEnemy.GetComponent<MobBehaviour>();
        mob.SetActive(true);
        es = mob.GetComponent<EnemyScript>();
        es.forceDisplay.text = "1 - " + mb.force;
        es.defDisplay.text = mb.defense + " %";
        es.vie = mb.vie;
        hfs.PlayerTurn = true;
        hfs.FightBtn.SetActive(true);
        es.PDV.text = es.vie + " X";
    }
}
