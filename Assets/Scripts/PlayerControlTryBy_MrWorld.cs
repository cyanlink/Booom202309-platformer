using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlTryBy_MrWorld : MonoBehaviour
{
    Rigidbody2D rb;

    public float playerSpeed = 5.0f;//移动速度

    public float jumpSpeed = 5.0f;//跳跃速度

    public bool isGround;//是否在地面上

    public Transform groundCheck;//角色地面检查点

    public LayerMask ground;//地面图层

    public float fallAddition = 3.0f;//下落重力加成
    public float jumpAddition = 1.5f;//跳跃重力加成

    private float moveX;

    private bool facingRight = true;//面向右侧

    private bool moveJump;//跳跃输入

    private bool jumpHold;//长按跳跃保持
    


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();//刚体组件调用
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveJump = Input.GetButtonDown("Jump");//空格跳跃
        jumpHold = Input.GetButton("Jump");//跳跃保持

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

    private void Move()//移动
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

    private void Flip()//角色翻转
    {
        facingRight = !facingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }

    private void Jump()//跳跃重力修正
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
