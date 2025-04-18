using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fake_Knight_JumpAnticipateState : Fake_Knight_GroundState
{
    public Fake_Knight_JumpAnticipateState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _stateName, Fake_Knight _boss) : base(_enemyBase, _stateMachine, _stateName, _boss)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(triggeredCalled)
        {
            boss.stateTrigger = true;
        }
    }

}
