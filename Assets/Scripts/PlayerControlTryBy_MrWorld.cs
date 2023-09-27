using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ��Ϊչʾ�����Բο�
/// </summary>
public class PlayerControlTryBy_MrWorld : MonoBehaviour
{
    Rigidbody2D rb;

    public float playerSpeed = 5.0f;//�ƶ��ٶ�

    public float jumpSpeed = 5.0f;//��Ծ�ٶ�

    public bool isGround;//�Ƿ��ڵ�����

    public Transform groundCheck;//��ɫ�������

    public LayerMask ground;//����ͼ��

    public float fallAddition = 3.0f;//���������ӳ�
    public float jumpAddition = 1.5f;//��Ծ�����ӳ�

    public int jumpCount;//��ǰ��Ծ����

    public int jumpMax=2;

    private float moveX;

    private bool facingRight = true;//�����Ҳ�

    private bool moveJump;//��Ծ����

    private bool jumpHold;//������Ծ

    private bool isJump;//�䴫�����ã���ʾ��Ծ

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();//�����������
        jumpCount = jumpMax;
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveJump = Input.GetButtonDown("Jump");//�ո���Ծ
        jumpHold = Input.GetButton("Jump");//��Ծ����

        if(moveJump && jumpCount >0)
        {
            isJump = true;
        }
    }

    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);
        Move();
        Jump();
    }

    private void Move()//�ƶ�
    {
        rb.velocity = new Vector2(moveX * playerSpeed, rb.velocity.y);
        if (facingRight == false && moveX >0)
        {
            Flip();
        }
        else if(facingRight == true && moveX <0)
        {
            Flip();
        }
    }

    private void Flip()//��ɫ��ת
    {
        facingRight = !facingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }

    private void Jump()//��Ծ��������
    {
        if (isGround)
        {
            jumpCount = jumpMax;
        }
        if (isJump)
        { 
            rb.AddForce(Vector2.up * jumpSpeed,ForceMode2D.Impulse);
            jumpCount--;
            isJump = false;
        }

        if (rb.velocity.y<0)
        {
            rb.gravityScale = fallAddition;
        }
        else if(rb.velocity.y>0&&jumpHold)
        {
            rb.gravityScale = jumpAddition;
        }
        else
        {
            rb.gravityScale = 1f;
        }
    }
}
