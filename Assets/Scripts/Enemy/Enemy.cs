using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public PhysicsCheck physicsCheck;

    Rigidbody2D rb;

    [HideInInspector] public Animator anim;

    [Header("��������")]

    public float normalSpeed;//�����ٶ�

    public float chaseSpeed;//׷���ٶ�

    [HideInInspector] public float currentSpeed;//��ǰ�ٶ�

    public Vector3 faceDir;//�泯����

    public float hurtForce;//�ܵ���������


    public Transform attacker;

    [Header("��ʱ��")]
    public float waitTime;

    public float waitTimeCounter;

    public bool wait;

    [Header("״̬")]
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
    /// ��ʱ��
    /// </summary>
    public void TimeCounter()
    {
        if (wait)
        {
            waitTimeCounter -= Time.deltaTime;
            if (waitTimeCounter <= 0)
            {
                Debug.Log("ִ��");
                wait = false;
                waitTimeCounter = waitTime;
                transform.localScale = new Vector3(faceDir.x,1,1);
            }
        }
    }

    public void OnTakeDamage(Transform attackTrans)
    {
        attacker = attackTrans;
        //ת��
        if (attackTrans.position.x - transform.position.x > 0)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
        if (attackTrans.position.x - transform.position.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        //���˱�����
        isHurt = true;
        anim.SetTrigger("hurt");
        Vector2 dir = new Vector2(transform.position.x - attackTrans.position.x, 0).normalized;

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
}
