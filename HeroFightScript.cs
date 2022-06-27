using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFightScript : MonoBehaviour
{
    public int vie = 10;
    public int force = 1;

    public GameObject enemy;
    public GameObject AtkPos;
    EnemyScript enemyScript;
    public GameObject FightBtn;
    public GameObject camFight;
    public GameObject baseEnemy;

    public GameObject[] PDV;

    public bool PlayerTurn = true;
    


    Vector3 InitialPos;

    private void Start()
    {
        InitialPos = transform.position;
        enemyScript = enemy.GetComponent<EnemyScript>();
    }
    public void Atk()
    {
        if (PlayerTurn)
        { 
            //iTween.MoveFrom(gameObject, ennemy.transform.position, 1);
            StartCoroutine("PlayAtk");
            PlayerTurn = false;
        }

    }

    IEnumerator PlayAtk()
    {
        FightBtn.SetActive(false);
        iTween.MoveTo(gameObject, AtkPos.transform.position, 0.2f);
        StartCoroutine("DamageAnim");
        PertePvEn();
        yield return new WaitForSeconds(0.45f);
        iTween.MoveTo(gameObject, InitialPos, 0.8f);
        if (enemyScript.vie <= 0)
        {
            enemy.SetActive(false);
            baseEnemy.SetActive(false);
            yield return new WaitForSeconds(0.6f);
            MobBehaviour mb = baseEnemy.GetComponent<MobBehaviour>();
            if(mb.loot != null)
            {
                mb.dropLoot();
            }
            camFight.SetActive(false);
        }
        else
        {
            yield return new WaitForSeconds(0.8f);
            enemyScript.Atkhero();
            FightBtn.SetActive(true);
        }
    }

    IEnumerator DamageAnim()
    {
        enemy.SetActive(false);
        yield return new WaitForSeconds(0.05f);
        enemy.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        enemy.SetActive(false);
        yield return new WaitForSeconds(0.05f);
        enemy.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        enemy.SetActive(false);
        yield return new WaitForSeconds(0.05f);
        enemy.SetActive(true);
        yield return new WaitForSeconds(0.05f);
    }

    public void PertePvEn()
    {
        for (int i = 0; i < force; i++)
        {
            enemyScript.vie--;
            enemyScript.PDV[enemyScript.vie].SetActive(false);
        }
    }
}
