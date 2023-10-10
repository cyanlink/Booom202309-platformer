using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    Rigidbody2D rbody;

    public AudioClip hitTheTarget;//命中目标音效

    public AudioClip hitTheWall;//箭矢碰撞到非敌人目标,撞墙
    
    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 2f);
    }

    
    void Update()
    {

    }
    
    //箭矢移动
    public void Move(Vector2 moveDirection, float moveForce) 
    {
        rbody.AddForce(moveDirection * moveForce);
    }

    //碰撞检测
    void OnCollisionEnter2D(Collision2D other)
    {
       
    }
}
