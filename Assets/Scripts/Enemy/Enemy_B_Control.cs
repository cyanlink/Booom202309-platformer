using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_B_Control : Enemy
{

    public override void Move()
    {
        base.Move(); 
        anim.SetBool("walk",true);
    }
}
