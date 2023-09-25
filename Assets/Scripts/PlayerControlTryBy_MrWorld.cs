using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private float moveX;

    private bool facingRight = true;//�����Ҳ�

    private bool moveJump;//��Ծ����

    private bool jumpHold;//������Ծ����
    


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();//�����������
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveJump = Input.GetButtonDown("Jump");//�ո���Ծ
        jumpHold = Input.GetButton("Jump");//��Ծ����

        if(moveJump && isGround)
        {
            rb.velocity = Vector2.up * jumpSpeed;
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
        if (rb.velocity.y<0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y*(fallAddition - 1)*Time.fixedDeltaTime;
        }
        else if(rb.velocity.y>0&&jumpHold)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (jumpAddition - 1) * Time.fixedDeltaTime;
        }
    }
}
