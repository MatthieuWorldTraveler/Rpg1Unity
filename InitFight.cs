using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitFight : MonoBehaviour
{
    public GameObject mob;
    EnemyScript es;
    public HeroFightScript hfs;
    
    public void initFight()
    {
        mob.SetActive(true);
        es = mob.GetComponent<EnemyScript>();
        es.vie = 3;
        hfs.PlayerTurn = true;
        hfs.FightBtn.SetActive(true);
        foreach(GameObject gameObject in es.PDV)
            gameObject.SetActive(true);
        
    }
}
