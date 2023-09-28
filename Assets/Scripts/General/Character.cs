using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("角色基本属性")]
    //血量属性
    public float maxHealth;
    public float currentHealth;

    [Header("受伤无敌时间")]
    public float invulnerableDuration;
    private float invulnerableCounter;
    public bool invulnerable;


    public UnityEvent<Transform> OnTakeDamage; //受伤事件
    public UnityEvent OnDie;//死亡事件

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (invulnerable)
        {
            invulnerableCounter -= Time.deltaTime;
            if (invulnerableCounter <=0)
            {
                invulnerable = false;
            }
        }
    }

    public void TakeDamage(Attack attacker)//当前血量减去所受伤害
    {
        if (invulnerable)
        {
            return;
        }
        if (currentHealth - attacker.damage >0)
        {
            currentHealth -= attacker.damage;
            TriggerInvulnerable();


            //执行受伤
            OnTakeDamage?.Invoke(attacker.transform);

        }
        else
        {
            currentHealth = 0;
            //触发死亡,播放死亡动画
            OnDie?.Invoke();
        }

    }

    private void TriggerInvulnerable()
    {
        if (invulnerable)
        {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
    }

}
