using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{
    public int vie = 3;
    public int force = 1;
    public GameObject hero;
    public GameObject AtkPos;
    public GameObject heromainscreen;
    HeroCharCollision hcc;

    public GameObject[] PDV;
    HeroFightScript hfs;

    Vector3 InitialPos;

    private void Start()
    {
        hcc = heromainscreen.GetComponent<HeroCharCollision>();
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
            SceneManager.LoadScene("0_StartMenu");
            HeroStats hs = heromainscreen.gameObject.GetComponent<HeroStats>();
            hs.ClearAllSaves();
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
        force = Random.Range(1, 4);
        for (int i = 0; i < force; i++)
        {
            if (hfs.vie > 0)
            {
                hfs.vie--;
                hfs.PDV[hfs.vie].SetActive(false);
                hcc.Pdvmainscreen[hfs.vie].SetActive(false);
            }
        }
        hcc.vie = vie;
    }

}
