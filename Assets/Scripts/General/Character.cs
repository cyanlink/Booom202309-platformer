using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("��ɫ��������")]
    //Ѫ������
    public float maxHealth;
    public float currentHealth;

    [Header("�����޵�ʱ��")]
    public float invulnerableDuration;
    private float invulnerableCounter;
    public bool invulnerable;

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

    public void TakeDamage(Attack attacker)//��ǰѪ����ȥ�����˺�
    {
        if (invulnerable)
        {
            return;
        }
        if (currentHealth - attacker.damage >0)
        {
            currentHealth -= attacker.damage;
            TriggerInvulnerable();
        }
        else
        {
            currentHealth = 0;
            //��������,������������
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
