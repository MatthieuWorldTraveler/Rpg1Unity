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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Hero")
        {
            otherObj = other;
            speed = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Hero")
        {
            otherObj = null;
            speed = 4;
        }
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
}
