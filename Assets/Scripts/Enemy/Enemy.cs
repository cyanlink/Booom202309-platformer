using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;

    protected Animator anim;

    [Header("基本参数")]

    public float normalSpeed;//常规速度

    public float chaseSpeed;//追击速度

    public float currentSpeed;//当前速度

    public Vector3 faceDir;//面朝方向

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentSpeed = normalSpeed;
    }

    public void Update()
    {
        faceDir = new Vector3(-transform.localScale.x, 0, 0);
    }

    public void FixedUpdate()
    {
        Move();
    }

    public virtual void Move()
    {
        rb.velocity = new Vector2(currentSpeed*faceDir.x,rb.velocity.y);  
    }
}
