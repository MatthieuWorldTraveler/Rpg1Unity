using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBehaviour : MonoBehaviour
{
    public Transform[] pathPoints;
    Vector2 dir;
    public float speed;
    public SpriteRenderer sr;
    Collider2D otherObj = null;
    public Animator animator;
    int dirValue = 1;
    public GameObject loot;
    public int Gold;
    public int xp;

    private void OnTriggerEnter2D(Collider2D other)
    {
        otherObj = other;
        speed = 0;
        dirValue = 0;
        animator.SetInteger("dir", dirValue);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        otherObj = null;
        speed = 4;
        dirValue = 1;
        animator.SetInteger("dir", dirValue);
    }

    private void Start()
    {
        dir = Vector2.right;
    }

    private void Update()
    {
            transform.Translate(dir * speed * Time.deltaTime);
            if (transform.position.x > pathPoints[1].position.x)
            {
                dir = Vector2.left;
                sr.flipX = true;
            }
            if (transform.position.x < pathPoints[0].position.x)
            {
                dir = Vector2.right;
                sr.flipX = false;
            }
    }

    public void dropLoot()
    {
        Instantiate(loot, transform.position, Quaternion.identity);
    }
}
