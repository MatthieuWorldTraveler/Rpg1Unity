using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroCharCollision : MonoBehaviour
{
    public GameObject dialWorldSpace;
    public TMP_Text dialTxt;
    Collider2D otherObj;
    public SpriteRenderer spriteRenderer;
    public GameObject dialCanvas;
    public Text dialcanvasTxt;
   


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "sign")
        {
            otherObj = other;
            otherObj.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            // dialWorldSpace.SetActive(true);
            // dialTxt.text = sb.signText;
            //dialTxt.SetText(sb.signText);

        }
        if (other.gameObject.tag == "exit")
        {
            string point = other.gameObject.GetComponent<ExitBehaviour>().teleportPoint;
            PlayerPrefs.SetString("Point", point);
            Application.LoadLevel(other.gameObject.name);
        }
        if (other.gameObject.tag == "smiley")
        {
            PnjSmiley smiley = other.gameObject.GetComponent<PnjSmiley>();
            smiley.Smiley.SetActive(true);
        }
        if (other.gameObject.tag == "mob")
        {
            print("Combat !");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "garde")
        {
            dialCanvas.SetActive(true);
            dialcanvasTxt.text = other.gameObject.GetComponent<PnjSimpleDial>().PnjDial;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "garde")
        {
            Invoke("HidePnjDialPanel", 2);
        }      
    }

    void HidePnjDialPanel()
    {
        dialCanvas.SetActive(false);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "sign")
        {
            SignBehaviour sb = otherObj.gameObject.GetComponent<SignBehaviour>();
            sb.ui.SetActive(false);
            //HideDialPanel();
            otherObj = null;
            Invoke("HideDialPanel", 1);
        }
        if (other.gameObject.tag == "smiley")
        {
            PnjSmiley smiley = other.gameObject.GetComponent<PnjSmiley>();
            smiley.Smiley.SetActive(false);
        }

    }

    void HideDialPanel()
    {
        dialWorldSpace.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.E) && otherObj != null)
        {
            if (otherObj.gameObject.tag == "sign")
            {
                otherObj.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                ShowDial();
            }           
        }
    }

    public void ShowDial()
    {
        SignBehaviour sb = otherObj.gameObject.GetComponent<SignBehaviour>();
        dialTxt.SetText(sb.signText);
        dialWorldSpace.SetActive(true);
    }
}
