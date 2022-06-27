using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int vie = 3;
    public int force = 1;
    public GameObject hero;
    public GameObject AtkPos;



    public GameObject[] PDV;
    HeroFightScript hfs;

    Vector3 InitialPos;

    private void Start()
    {
        InitialPos = transform.position;
        hfs = hero.GetComponent<HeroFightScript>();
    }

    public void Atkhero()
    {
        //iTween.MoveFrom(gameObject, ennemy.transform.position, 1);
        StartCoroutine("PlayAtk");
    }
    IEnumerator PlayAtk()
    {
        iTween.MoveTo(gameObject, AtkPos.transform.position, 0.2f);
        StartCoroutine("DamageAnim");
        PertePvHero();
        yield return new WaitForSeconds(0.3f);
        iTween.MoveTo(gameObject, InitialPos, 0.6f);
        if (hfs.vie <= 0)
        {
            print("GameOver");
        }
        else
        {
            yield return new WaitForSeconds(0.6f);
            hfs.PlayerTurn = true;
        }
    }

    IEnumerator DamageAnim()
    {
        hero.SetActive(false);
        yield return new WaitForSeconds(0.05f);
        hero.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        hero.SetActive(false);
        yield return new WaitForSeconds(0.05f);
        hero.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        hero.SetActive(false);
        yield return new WaitForSeconds(0.05f);
        hero.SetActive(true);
        yield return new WaitForSeconds(0.05f);
    }

    public void PertePvHero()
    {
        for (int i = 0; i < force; i++)
        {
            hfs.vie--;
            hfs.PDV[hfs.vie].SetActive(false);
        }
    }

}
