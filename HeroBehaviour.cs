using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        string point = PlayerPrefs.GetString("Point", "Point_0");
        Vector3 teleportPosition = GameObject.Find(point).transform.position;
        transform.position = teleportPosition;
    }
}
