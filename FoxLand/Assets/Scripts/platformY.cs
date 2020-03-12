using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformY : MonoBehaviour
{

    private Rigidbody2D rb;
    public Collider2D Coll;
    public float Speed;
    private float Topy, Bottomy;
    public Transform TopPoint, BottomPoint;
    private bool isUp;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.DetachChildren();
        Coll = GetComponent<Collider2D>();

        Topy = TopPoint.position.y;
        Bottomy = BottomPoint.position.y;
        Destroy(TopPoint.gameObject);
        Destroy(BottomPoint.gameObject);
    }

    // Update is called once per frame
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
                isUp = false;
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
