using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //�����˺�
    public int damage;

    //������Χ
    public float attackRange;

    //����Ƶ��
    public float attackRate;


    private void OnTriggerStay2D(Collider2D other)
    {
        other.GetComponent<Character>()?.TakeDamage(this);
    }
}
