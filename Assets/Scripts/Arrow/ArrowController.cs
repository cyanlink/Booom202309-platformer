using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    Rigidbody2D rbody;

    public AudioClip hitTheTarget;//����Ŀ����Ч

    public AudioClip hitTheWall;//��ʸ��ײ���ǵ���Ŀ��,ײǽ
    
    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 2f);
    }

    
    void Update()
    {

    }
    
    //��ʸ�ƶ�
    public void Move(Vector2 moveDirection, float moveForce) 
    {
        rbody.AddForce(moveDirection * moveForce);
    }

    //��ײ���
    void OnCollisionEnter2D(Collision2D other)
    {
       
    }
}
