using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //¹¥»÷ÉËº¦
    public int damage;

    //¹¥»÷·¶Î§
    public float attackRange;

    //¹¥»÷ÆµÂÊ
    public float attackRate;


    private void OnTriggerStay2D(Collider2D other)
    {
        other.GetComponent<Character>()?.TakeDamage(this);
    }
}
