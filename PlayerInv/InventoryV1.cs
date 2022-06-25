using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryV1 : MonoBehaviour
{
    public GameObject inventory;
    public bool invState;

    // Start is called before the first frame update
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.I))
        {
            if(!invState)
            {
                inventory.SetActive(true);
                invState = true;
                Debug.Log("InvFermée");
            }
            else
            {
                inventory.SetActive(false);
                invState = false;
                Debug.Log("InvOuvert");
            }
        }
    }
}
