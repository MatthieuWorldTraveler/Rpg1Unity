using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EchapMenu : MonoBehaviour
{
    string lastScene;
    HeroBehaviour hero;
    HeroCharCollision hcc;
    public GameObject heromain;
    HeroStats hStats;
    
    void Update()
    {  
        if(Input.GetKeyDown(KeyCode.Escape))
            Continue();
    }

    private void Start()
    {
        lastScene = PlayerPrefs.GetString("LastScene");
        hStats = heromain.GetComponent<HeroStats>();
        hcc = heromain.GetComponent<HeroCharCollision>();
        hero = heromain.GetComponent<HeroBehaviour>();
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("0_StartMenu");
    }

    public void Continue()
    {
        SceneManager.LoadScene(lastScene);
        hStats.InitAllStats();
    }

}
