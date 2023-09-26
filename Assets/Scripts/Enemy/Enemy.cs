using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;

    protected Animator anim;

    [Header("��������")]

    public float normalSpeed;//�����ٶ�

    public float chaseSpeed;//׷���ٶ�

    public float currentSpeed;//��ǰ�ٶ�

    public Vector3 faceDir;//�泯����

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
