using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 巡逻
/// </summary>
public class Enemy_BPatrolState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;

    }

    public override void LogicUpdate()
    {
        //发现玩家切换为追击状态（chase）

        if (!currentEnemy.physicsCheck.isGround||(currentEnemy.physicsCheck.touchLeftWall && currentEnemy.faceDir.x < 0) || (currentEnemy.physicsCheck.touchRightWall && currentEnemy.faceDir.x > 0))
        {
            currentEnemy.wait = true;
            //未绑定动画，下行代码不执行,流程卡住
            currentEnemy.anim.SetBool("walk", false);

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
