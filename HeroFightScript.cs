using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroFightScript : MonoBehaviour
{
    public int vie = 10;
    public int vieMax = 10;
    public TMP_Text forceDisplay;
    public TMP_Text defDisplay;
    HeroStats hs;

    public GameObject enemy;
    public GameObject AtkPos;
    EnemyScript enemyScript;
    public GameObject FightBtn;
    public GameObject camFight;
    public GameObject baseEnemy;
    public TMP_Text PDV;
    public bool PlayerTurn = true;
    public GameObject heromainscreen;
    HeroCharCollision hcc;
    QuestTaker qt;
    InventoryV1 inv;
    MobBehaviour mb;
    public int defHit = 0;
    public int defBonus = 0;
    public AudioClip MobDamaged;
    public AudioClip blocked;
    public GameObject Shield;



    Vector3 InitialPos;

    private void Start()
    {
        hs = heromainscreen.GetComponent<HeroStats>();
        qt = heromainscreen.GetComponent<QuestTaker>();
        hcc = heromainscreen.GetComponent<HeroCharCollision>();
        InitialPos = transform.position;
        enemyScript = enemy.GetComponent<EnemyScript>();
    }
    public void Atk()
    {
        if (PlayerTurn)
        {
            defRoll();
            PlayerTurn = false;
        }

    }

    IEnumerator PlayAtk()
    {
        GetComponent<AudioSource>().PlayOneShot(MobDamaged);
        FightBtn.SetActive(false);
        iTween.MoveTo(gameObject, AtkPos.transform.position, 0.2f);
        StartCoroutine("DamageAnim");
        PertePvEn();
        yield return new WaitForSeconds(0.45f);
        iTween.MoveTo(gameObject, InitialPos, 0.8f);
        if (mb.vie <= 0)
        {
            if (defHit != 0)
            {
                defHit = 0;
                defBonus = 0;
                defDisplay.text = $"{hs.defense+defBonus} %";
            }
            enemy.SetActive(false);
            baseEnemy.SetActive(false);
            yield return new WaitForSeconds(0.6f);
            mb = baseEnemy.GetComponent<MobBehaviour>();
            if(mb.loot != null)
            {
                mb.dropLoot();
            }
            camFight.SetActive(false);
            print("fight over");
            inv = heromainscreen.gameObject.GetComponent<InventoryV1>();
            inv.ScreenStats.SetActive(true);
            inv.invState = true;
            hcc.goldKeep += mb.Gold;
            hcc.xpKeep += mb.xp;
            hcc.killchain++;
        }
        else
        {
            yield return new WaitForSeconds(0.8f);
            enemyScript.Atkhero();
            FightBtn.SetActive(true);
        }
    }

    IEnumerator PlayAtkBlock()
    {
        GetComponent<AudioSource>().PlayOneShot(blocked);
        FightBtn.SetActive(false);
        iTween.MoveTo(gameObject, AtkPos.transform.position, 0.2f);
        StartCoroutine("DamageAnimBlocked");
        yield return new WaitForSeconds(0.45f);
        iTween.MoveTo(gameObject, InitialPos, 0.8f);
        yield return new WaitForSeconds(0.8f);
        enemyScript.Atkhero();
        FightBtn.SetActive(true);
    }

    IEnumerator DamageAnimBlocked()
    {
        Shield.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        Shield.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        Shield.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        Shield.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        Shield.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        Shield.SetActive(false);
        yield return new WaitForSeconds(0.1f);
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
        mb = baseEnemy.GetComponent<MobBehaviour>();
        int degats = Random.Range(1, hs.force+1);
        for (int i = 0; i < degats; i++)
        {
            if (mb.vie != 0)
            {
                mb.vie--;
                enemyScript.PDV.text = mb.vie + " X";
            }
        }
    }

    public void runAway()
    {
        baseEnemy.SetActive(false);
        camFight.SetActive(false);
        inv = heromainscreen.gameObject.GetComponent<InventoryV1>();
        inv.ScreenStats.SetActive(true);
        inv.invState = true;
    }

    public void defense()
    {
        if(defHit == 0)
            defBonus = 50;
        defHit = 2;
        defDisplay.text = $"{hs.defense + defBonus} %";
        PlayerTurn = false;
        enemyScript.Atkhero();
    }

    public void defRoll()
    {
        mb = baseEnemy.GetComponent<MobBehaviour>();
        int defProtec = Random.Range(0, 101);
        print(defProtec + " / " + mb.defense);
        if(defProtec > mb.defense)
            StartCoroutine("PlayAtk");
        else
            StartCoroutine("PlayAtkBlock");
    }
}
