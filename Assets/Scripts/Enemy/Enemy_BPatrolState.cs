using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Ѳ��
/// </summary>
public class Enemy_BPatrolState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;

    }

    public override void LogicUpdate()
    {
        //��������л�Ϊ׷��״̬��chase��
        if (currentEnemy.FoundPlayer())
        {
            currentEnemy.SwitchState(NPCState.Chase);
        }

        if (!currentEnemy.physicsCheck.isGround||(currentEnemy.physicsCheck.touchLeftWall && currentEnemy.faceDir.x < 0) || (currentEnemy.physicsCheck.touchRightWall && currentEnemy.faceDir.x > 0))
        {
            currentEnemy.wait = true;
            //δ�󶨶��������д��벻ִ��,���̿�ס
            currentEnemy.anim.SetBool("walk", false);

        }
        else
        {
            currentEnemy.anim.SetBool("walk", true);
        }
    }

    public override void PhysicUpdate()
    {
        
    }

    public override void OnExit()
    {
        currentEnemy.anim.SetBool("walk", false);
    }
}