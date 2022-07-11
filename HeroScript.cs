using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroScript : MonoBehaviour
{
   
    //Variables
    public float moveSpeed = 5.0f;

    public Rigidbody2D rb;

    public Animator animator;

    public SpriteRenderer spriteRenderer;

    HeroCharCollision hcc;

    int lastkey;


    Vector2 dir;
    int dirValue = 0; // 0 idle, 1 down 2 side 3 up

    private void Start()
    {
        hcc = gameObject.GetComponent<HeroCharCollision>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hcc.camFight.activeInHierarchy)
        {
            HandleKeys();
            HandleMove();
        }
    }

    public void HandleKeys()
    {

        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W)) && !hcc.AnimOn) // Fleche haut
        {
            dir = Vector2.up;
            dirValue = 3;
            lastkey = 3;
        }
        else if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && !hcc.AnimOn) // Fleche haut
        {
            dir = Vector2.right;
            dirValue = 2;
            lastkey = 2;
        }
        else if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A)) && !hcc.AnimOn) // Fleche haut
        {
            dir = Vector2.left;
            dirValue = 4;
            lastkey = 4;

        }
        else if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && !hcc.AnimOn) // Fleche haut
        {
            dir = Vector2.down;
            dirValue = 1;
            lastkey = 1;
        }
        else if (lastkey == 1)
        {
            dirValue = 5;
            dir = Vector2.zero;
        }
        else if (lastkey == 2)
        {
            dirValue = 6;
            dir = Vector2.zero;
        }
        else if (lastkey == 3)
        {
            dirValue = 7;
            dir = Vector2.zero;
        }
        else if (lastkey == 4)
        {
            dirValue = 8;
            dir = Vector2.zero;
        }
        if(Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 15.0f;
        }
        else
        {
            moveSpeed = 10.0f;
        }
    }

    public void HandleMove()
    {
        rb.MovePosition(rb.position + dir * moveSpeed * Time.fixedDeltaTime);
        animator.SetInteger("dir", dirValue);
    }
}