using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Eagle : Enemy
{
    private Rigidbody2D rb;
    public Collider2D Coll;
    public float Speed;
    private float Topy, Bottomy;
    public Transform TopPoint, BottomPoint;
    private bool isUp;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        transform.DetachChildren();
        Coll = GetComponent<Collider2D>();

        Topy = TopPoint.position.y;
        Bottomy = BottomPoint.position.y;
        Destroy(TopPoint.gameObject);
        Destroy(BottomPoint.gameObject);
    }


    void Update()
    {
        Movement();
    }
    void Movement()
    {
        if (isUp)
        {
            rb.velocity = new Vector2(rb.velocity.x, Speed);
            if (transform.position.y > Topy)
            {
                isUp= false;
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -Speed);
            if (transform.position.y < Bottomy)
            {
                isUp = true;
            }
        }
    }
}
