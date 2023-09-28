using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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


    public UnityEvent<Transform> OnTakeDamage; //�����¼�
    public UnityEvent OnDie;//�����¼�

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


            //ִ������
            OnTakeDamage?.Invoke(attacker.transform);

        }
        else
        {
            currentHealth = 0;
            //��������,������������
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
