using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_A_Control : MonoBehaviour
{
    public float speed = 1.0f;//移动速度

    private Rigidbody2D rbody;//刚体组件获取

    private Vector2 moveDirection;//控制移动方向

    public float changeDirectionTime = 3.0f;//转变方向时间

    private float changeTimer;//转变方向计时器



    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        changeTimer = changeDirectionTime;
        moveDirection = Vector2.left;
    }

    
    void Update()
    {
        changeTimer -= Time.deltaTime;
        if (changeTimer < 0)
        {
            moveDirection *= -1;
            changeTimer = changeDirectionTime;
        }//转向计时
        Vector2 position = rbody.position;
        position.x += moveDirection.x * speed * Time.deltaTime;
        rbody.MovePosition(position);
    }
}
