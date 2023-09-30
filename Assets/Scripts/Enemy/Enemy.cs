﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public PhysicsCheck physicsCheck;

    Rigidbody2D rb;

    [HideInInspector] public Animator anim;

    [Header("基本参数")]

    public float normalSpeed;//常规速度

    public float chaseSpeed;//追击速度

    [HideInInspector] public float currentSpeed;//当前速度

    public Vector3 faceDir;//面朝方向

    public float hurtForce;//受到攻击朝向


    public Transform attacker;

    [Header("检测")]
    public Vector2 centerOffset;

    public Vector2 checkSize;//检测尺寸

    public float checkDistance;//检测距离

    public LayerMask attackLayer;

    [Header("计时器")]
    public float waitTime;

    public float waitTimeCounter;

    public bool wait;

    public float lostTime;

    public float lostTimeCounter;


    [Header("状态")]
    public bool isHurt;

    public bool isDead;

    private BaseState currentState;
    protected BaseState patrolState;
    protected BaseState chaseState;
    

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();

        currentSpeed = normalSpeed;
        waitTimeCounter = waitTime;
    }

    private void OnEnable()
    {
        currentState = patrolState;
        currentState.OnEnter(this);
    }

    public void Update()
    {
        faceDir = new Vector3(-transform.localScale.x, 0, 0);

        currentState.LogicUpdate();
        TimeCounter();
    }

    public void FixedUpdate()
    {
        if (!isHurt && !isDead && !wait)
        {
            Move();
        }
        currentState.PhysicUpdate();
    }

    private void OnDisable()
    {
        currentState.OnExit();
    }

    public virtual void Move()
    {
        rb.velocity = new Vector2(currentSpeed*faceDir.x,rb.velocity.y);  
    }

    /// <summary>
    /// 计时器
    /// </summary>
    public void TimeCounter()
    {
        if (wait)
        {
            waitTimeCounter -= Time.deltaTime;
            if (waitTimeCounter <= 0)
            {
                Debug.Log("执行");
                wait = false;
                waitTimeCounter = waitTime;
                transform.localScale = new Vector3(faceDir.x,1,1);
            }
        }

        if (!FoundPlayer()&&lostTimeCounter > 0)
        {
            lostTimeCounter -= Time.deltaTime;
        }
        //else
        //{
        //  lostTimeCounter = lostTime;
        //}
    }

    public bool FoundPlayer()
    {
        return Physics2D.BoxCast(transform.position+(Vector3)centerOffset,checkSize,0,faceDir,checkDistance,attackLayer);
        
    }

    public void SwitchState(NPCState state)
    {
        var newState = state switch
        {
            NPCState.Patrol => patrolState,
            NPCState.Chase => chaseState,
            _ => null
        };

        currentState.OnExit();//结束上一状态
        currentState = newState;//切换新的状态
        currentState.OnEnter(this);//执行新的状态
    }

    #region 事件执行方法
    public void OnTakeDamage(Transform attackTrans)
    {
        attacker = attackTrans;
        //转身
        if (attackTrans.position.x - transform.position.x > 0)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
        if (attackTrans.position.x - transform.position.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        //受伤被击退
        isHurt = true;
        anim.SetTrigger("hurt");
        Vector2 dir = new Vector2(transform.position.x - attackTrans.position.x, 0).normalized;
        rb.velocity = new Vector2(0, rb.velocity.y);
        StartCoroutine(OnHurt(dir));
    }

    private IEnumerator OnHurt(Vector2 dir)
    {
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.45f);
        isHurt = false;
    }

    public void OnDie()
    {
        gameObject.layer = 2;
        anim.SetBool("die", true);
        isDead = true;
    }

    public void OnDestroyAfterAnimation()
    {
        Destroy(this.gameObject);
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + (Vector3)centerOffset+new Vector3(checkDistance*(-transform.localScale.x),0), 0.2F);
    }
} 
