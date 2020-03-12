using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Frog : Enemy
{
    private Rigidbody2D rb;
    public Collider2D Coll;
    private bool Faceleft = true;
    public float Speed,JumpForce;
    public Transform leftPoint, rightPoint;
    private float leftx, rightx;
 
    public LayerMask Ground;

    //动画

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        transform.DetachChildren();
        Coll = GetComponent<Collider2D>();

        leftx = leftPoint.position.x;
        rightx = rightPoint.position.x;
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
    }

    void Update()
    {
        SwitchAnim();
    }

    void Movement()
    {
        if (Faceleft)
        {
            if (transform.position.x < leftx)
            {
                rb.velocity = new Vector2(0, 0);
                transform.localScale = new Vector3(-1, 1, 1);
                Faceleft = false;
            }
            if (Coll.IsTouchingLayers(Ground))
            {
                Anim.SetBool("jumping", true);
                rb.velocity = new Vector2(-Speed, JumpForce);
            }
           
            
        }
        else
        {
            if (transform.position.x > rightx)
            {
                rb.velocity = new Vector2(0, 0);
                transform.localScale = new Vector3(1, 1, 1);
                Faceleft = true;
            }
            if (Coll.IsTouchingLayers(Ground))
            {
                Anim.SetBool("jumping", true);
                rb.velocity = new Vector2(Speed, JumpForce);
            }
            
        }
    }
    void SwitchAnim()
    {
        if(Anim.GetBool("jumping"))
        {
            if(rb.velocity.y < 0.1)
            {
                Anim.SetBool("jumping",false);
                Anim.SetBool("falling",true);
            }
        }
        if(Coll.IsTouchingLayers(Ground) && Anim.GetBool("falling"))
        {
            Anim.SetBool("falling",false);

        }
    }

}
