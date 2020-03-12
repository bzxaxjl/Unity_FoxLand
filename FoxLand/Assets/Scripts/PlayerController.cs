using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    public Animator anim;
    public LayerMask ground;
    public Collider2D coll;
    public int Cherry = 0;
    public Text CherryNum;
    public int Gem = 0;
    public Text GemNum;
    private bool isHurt;
    public AudioSource jumpAudio,hurtAudio,cherryAudio,deathAudio;
    private int extraJump;

    public Transform GroundCheck;
    private bool isGround;

    public GameObject enterDialog;
    public GameObject needMoreDialog;
    //public GameObject signDialog;


    void Start()
    {
        
    }

    void FixedUpdate()//使用Fixed限制不同设备因配置导致的帧数不同问题，强制60帧。
    {
        if(!isHurt)
        {
            Movement();
        }
        SwitchAnim();
        isGround = Physics2D.OverlapCircle(GroundCheck.position, 0.2f, ground);
    }
    private void Update()
    {
        //Jump();
        newJump();
        CherryNum.text = Cherry.ToString();
        GemNum.text = Gem.ToString();
    }
    //移动
    void Movement()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float facedircetion = Input.GetAxisRaw("Horizontal");

        //控制玩家移动
        if (horizontalMove != 0)
        {
            rb.velocity = new Vector2(horizontalMove* speed * Time.fixedDeltaTime ,rb.velocity.y);
            anim.SetFloat("running",Mathf.Abs(facedircetion));
        }
        //控制面向方向
        if (facedircetion !=0)
        {
            transform.localScale = new Vector3(facedircetion,1,1);
        }

    }
    //void Jump()
    //{
    //    //控制角色跳跃
    //    if (Input.GetButton("Jump") && coll.IsTouchingLayers(ground))
    //    {
    //        rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
    //        jumpAudio.Play();
    //        anim.SetBool("jumping", true);
    //    }
    //}
    //控制角色跳跃,更新二段跳
    void newJump()
    {
        if (isGround)
        {
            extraJump = 1;
        }
        if(Input.GetButtonDown("Jump") && extraJump > 0)
        {
            jumpAudio.Play();
            rb.velocity = Vector2.up * jumpForce;
            extraJump--;
            anim.SetBool("jumping",true);
            
        }
        if(Input.GetButtonDown("Jump") && extraJump == 0 && isGround)
        {
            rb.velocity = Vector2.up * jumpForce;
            jumpAudio.Play();
            anim.SetBool("jumping", true);
        }
    }
    void SwitchAnim()
    {
        anim.SetBool("idle", false);

        //自由下落时也可以对敌人造成伤害
        if (rb.velocity.y < 0.1f && !coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", true);
        }
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }else if(isHurt)
        {
            anim.SetBool("hurt", true);
            anim.SetFloat("running",0);
            if (Mathf.Abs(rb.velocity.x)<0.1f)
            {
                anim.SetBool("hurt", false);
                anim.SetBool("idle", true);
                isHurt = false;
            }
        }
        else if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling",false);
            anim.SetBool("idle",true);
        }
    }
    //碰撞触发器
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //收集草莓
        if(collision.tag == "Collection")
        {
            cherryAudio.Play();
            //Destroy(collision.gameObject);
            //Cherry += 1;
            collision.GetComponent<Animator>().Play("isGot");
            //CherryNum.text = Cherry.ToString();
        }
        //收集钻石
        if (collision.tag == "Gem")
        {
            cherryAudio.Play();
            //Destroy(collision.gameObject);
            //Gem += 1;
            collision.GetComponent<Animator>().Play("GotGem");
            //GemNum.text = Gem.ToString();
        }
        if (collision.tag == "DeadLine")
        {
            deathAudio.Play();
            GetComponent<AudioSource>().enabled = false;
            
            Invoke("Restart",0.5f);
        }

        if (collision.tag == "Door"  )
        {
            if (Gem == 3 && Cherry == 3)
            {
                enterDialog.SetActive(true);
                needMoreDialog.SetActive(false);
            }
            else
            {
                needMoreDialog.SetActive(true);
                enterDialog.SetActive(false);
            }
        }
        //if (collision.tag == "Sign")
        //{
        //    signDialog.SetActive(true);
        //}

    }
    //消灭敌人
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if(anim.GetBool("falling"))
            {
                enemy.JumpOn();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
                anim.SetBool("jumping", true);
            }
            else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-10, rb.velocity.y);
                hurtAudio.Play();
                isHurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(10, rb.velocity.y);
                hurtAudio.Play();
                isHurt = true;

            }
        }
    }
    private void Restart()
    {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void CherryCount()
    {
        Cherry += 1;
    }
    public void GemCount()
    {
        Gem += 1;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{

    //    if (collision.tag == "Player")//&& player.Gem == 3 && player.Cherry ==3)
    //    {
    //        enterDialog.SetActive(true);
    //    }
    //}
    private void OnTriggerExit2D(Collider2D collision)
    {
        //PlayerController player = new PlayerController();
        if (collision.tag == "Door")
        {
            enterDialog.SetActive(false);
            needMoreDialog.SetActive(false);
        }
        //if (collision.tag == "Sign")
        //{
        //    signDialog.SetActive(false);
        //}
    }
}
