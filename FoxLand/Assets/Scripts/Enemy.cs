using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator Anim;
    protected AudioSource deathAudio;
    protected virtual void Start()
    {
        Anim = GetComponent<Animator>();
        deathAudio = GetComponent<AudioSource>();
    }

    public void Death()
    {
        GetComponent<Collider2D>().enabled = false;//取消二次碰撞的bug
        Destroy(gameObject);
    }
    public void JumpOn()
    {
        //播放死亡音效
        deathAudio.Play();
        Anim.SetTrigger("death");
    }
}