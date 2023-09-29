using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_B_Control : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        patrolState = new Enemy_BPatrolState();
        chaseState = new Enemy_BChaseState();

    }
}
