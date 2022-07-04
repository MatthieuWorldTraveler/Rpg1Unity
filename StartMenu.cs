using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    HeroStats hs;
    public GameObject mainhero;
    string FirstLaunch;
    public GameObject SavedBtn;
    public GameObject ResumeBtn;
    public GameObject Restart;

    private void Start()
    {
        hs = mainhero.GetComponent<HeroStats>();
        FirstLaunch = File.ReadAllText("FirstLaunch.txt");
        if (FirstLaunch == "Saved")
        {
            SavedBtn.SetActive(false);
            ResumeBtn.SetActive(true);
            Restart.SetActive(true);
            Debug.Log("SaveAvailable");
        }
        else
        {
            SavedBtn.SetActive(true);
            ResumeBtn.SetActive(false);
            Restart.SetActive(false);
            Debug.Log("SaveNotAvailable");
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("LastScene", "1_BourgPalette"));
        hs.InitAllStats();
    }

    public void RestartGame()
    {
        hs.ClearAllSaves();
        SceneManager.LoadScene("1_BourgPalette");
        hs.InitiateBaseStats();
        hs.InitAllStats();
        hs.SaveAllStats();
    }

    public void StopTheGame()
    {
        Application.Quit();
    }
}
