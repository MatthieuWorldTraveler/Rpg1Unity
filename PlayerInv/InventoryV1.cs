using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryV1 : MonoBehaviour
{
    public GameObject ScreenStats;
    public bool invState = true;

    HeroCharCollision camFight;


    private void Start()
    {
        camFight = gameObject.GetComponent<HeroCharCollision>();
    }
    // Start is called before the first frame update
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.I))
        {
            if(!invState && !camFight.camFight.activeInHierarchy)
            {
                ScreenStats.SetActive(true);
                invState = true;
                Debug.Log("StatsAffiché");
            }
            else
            {
                ScreenStats.SetActive(false);
                invState = false;
                Debug.Log("StatsCaché");
            }
        }
    }
}
