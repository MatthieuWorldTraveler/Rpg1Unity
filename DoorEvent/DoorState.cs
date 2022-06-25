using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorState : MonoBehaviour
{
    bool DoorOC;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "door")
        {
            DoorOC = true;
            collision.GetComponent<DoorParam>().doorAnimator.SetBool("DoorSta", DoorOC);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "door")
        {
            DoorOC = false;
            collision.GetComponent<DoorParam>().doorAnimator.SetBool("DoorSta", DoorOC);
        }
    }
}
