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
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W)) // Fleche haut
        {
            dir = Vector2.up;
            dirValue = 3;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) // Fleche haut
        {
            dir = Vector2.right;
            dirValue = 2;
            spriteRenderer.flipX = true;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A)) // Fleche haut
        {
            dir = Vector2.left;
            dirValue = 2;
            spriteRenderer.flipX = false;

        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) // Fleche haut
        {
            dir = Vector2.down;
            dirValue = 1;
        }
        else
        {
            dir = Vector2.zero;
            dirValue = 0;
        }  
    }

    public void HandleMove()
    {
        rb.MovePosition(rb.position + dir * moveSpeed * Time.fixedDeltaTime);
        animator.SetInteger("dir", dirValue);
    }
}