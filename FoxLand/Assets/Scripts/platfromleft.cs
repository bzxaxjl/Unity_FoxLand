using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platfromleft : MonoBehaviour
{
    private Rigidbody2D rb;
    public Collider2D Coll;
    private bool isLeft=true;
    public float Speed;
    public Transform leftPoint, rightPoint;
    private float leftx, rightx;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.DetachChildren();
        Coll = GetComponent<Collider2D>();

        leftx = leftPoint.position.x;
        rightx = rightPoint.position.x;
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    void Movement()
    {
        if (isLeft)
        {
            rb.velocity = new Vector2(-Speed,rb.velocity.y);
            if (transform.position.x < leftx)
            {
                isLeft = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(Speed,rb.velocity.y);
            if (transform.position.x > rightx)
            {
                isLeft = true;
            }
        }
    }
}
