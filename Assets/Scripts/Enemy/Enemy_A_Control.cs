using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_A_Control : MonoBehaviour
{
    public float speed = 1.0f;//�ƶ��ٶ�

    private Rigidbody2D rbody;//���������ȡ

    private Vector2 moveDirection;//�����ƶ�����

    public float changeDirectionTime = 3.0f;//ת�䷽��ʱ��

    private float changeTimer;//ת�䷽���ʱ��



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
        }//ת���ʱ
        Vector2 position = rbody.position;
        position.x += moveDirection.x * speed * Time.deltaTime;
        rbody.MovePosition(position);
    }
}
