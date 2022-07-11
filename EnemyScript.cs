using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{
    public int vie;
    public TMP_Text forceDisplay;
    public TMP_Text defDisplay;
    public GameObject hero;
    public GameObject AtkPos;
    public GameObject heromainscreen;
    HeroStats hs;
    HeroCharCollision hcc;
    MobBehaviour mb;
    public TMP_Text PDV;
    HeroFightScript hfs;
    public AudioClip GameOver;
    public AudioClip JoueurDamaged;
    public AudioClip Blocked;
    public GameObject Shield;

    Vector3 InitialPos;

    private void Start()
    {
        hs = heromainscreen.GetComponent<HeroStats>();
        hcc = heromainscreen.GetComponent<HeroCharCollision>();
        InitialPos = transform.position;
        hfs = hero.GetComponent<HeroFightScript>();
    }

    public void Atkhero()
    {
        defRoll();
    }
    IEnumerator PlayAtk()
    {
        GetComponent<AudioSource>().PlayOneShot(JoueurDamaged);
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
            PlayerPrefs.SetString("NewMap", "false");
        }
        else
        {
            yield return new WaitForSeconds(0.6f);
            hfs.PlayerTurn = true;
        }
    }

    IEnumerator PlayAtkBlock()
    {
        GetComponent<AudioSource>().PlayOneShot(Blocked);
        iTween.MoveTo(gameObject, AtkPos.transform.position, 0.2f);
        StartCoroutine("DamageAnimBlocked");
        yield return new WaitForSeconds(0.45f);
        iTween.MoveTo(gameObject, InitialPos, 0.8f);
        yield return new WaitForSeconds(0.8f);
        hfs.PlayerTurn = true;
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
        mb = hcc.Encounter.gameObject.GetComponent<MobBehaviour>();
        int degats = Random.Range(1, mb.force + 1);
        for (int i = 0; i < degats; i++)
        {
            if (hfs.vie > 0)
            {
                hfs.vie--;
                hfs.PDV.text = "X " + hfs.vie;
                hcc.Pdvmainscreen.text = "X " + hfs.vie;
            }
            else
                hero.GetComponent<AudioSource>().PlayOneShot(GameOver);


        }
    }
    public void defRoll()
    {
        hs = heromainscreen.GetComponent<HeroStats>();
        hfs = hero.GetComponent<HeroFightScript>();
        int defProtec = Random.Range(0, 101);
        print(defProtec + " / " + hs.defense + " - " + hfs.defBonus + " /// " + hfs.defHit);
        if (defProtec > hs.defense+hfs.defBonus)
            StartCoroutine("PlayAtk");
        else
            StartCoroutine("PlayAtkBlock");
        if (hfs.defHit == 1)
        {
            hfs.defBonus = 0;
            hfs.defDisplay.text = $"{hs.defense + hfs.defBonus} %";
            hfs.defHit--;
        }
        else if (hfs.defHit > 1)
            hfs.defHit--;
    }

}
