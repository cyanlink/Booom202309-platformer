using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    PhysicsCheck physicsCheck;

    Rigidbody2D rb;

    protected Animator anim;

    [Header("��������")]

    public float normalSpeed;//�����ٶ�

    public float chaseSpeed;//׷���ٶ�

    public float currentSpeed;//��ǰ�ٶ�

    public Vector3 faceDir;//�泯����

    [Header("��ʱ��")]
    public float waitTime;

    public float waitTimeCounter;

    public bool wait;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();

        currentSpeed = normalSpeed;
        waitTimeCounter = waitTime;
    }

    public void Update()
    {
        faceDir = new Vector3(-transform.localScale.x, 0, 0);

        if ((physicsCheck.touchLeftWall&&faceDir.x<0) || (physicsCheck.touchRightWall&&faceDir.x>0))
        {
            wait = true;
            //anim.SetBool("walk", false);

        }
        TimeCounter();
    }

    public void FixedUpdate()
    {
        Move();
    }

    public virtual void Move()
    {
        rb.velocity = new Vector2(currentSpeed*faceDir.x,rb.velocity.y);  
    }
    //��ʱ��
    public void TimeCounter()
    {
        if (wait)
        {
            waitTimeCounter -= Time.deltaTime;
            if (waitTimeCounter <= 0)
            {
                wait = false;
                waitTimeCounter = waitTime;
                transform.localScale = new Vector3(faceDir.x,1,1);
            }
        }
    }
}
