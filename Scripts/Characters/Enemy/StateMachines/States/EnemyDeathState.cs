using UnityEngine;

public class EnemyDeathState : EnemyBaseState
{
    private BossEnemy boss;
    Vector3 moveVec = new Vector3(0, 0, 0);
    public EnemyDeathState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
        if (stateMachine.Enemy.Data.Type == EnemyType.Boss)
            boss = stateMachine.Enemy as BossEnemy;
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.Enemy.Animator.SetTrigger("Die");
        Vector3 tmp = 3 * (stateMachine.Enemy.transform.position - stateMachine.Target.transform.position).normalized;
        moveVec = new Vector3(tmp.x, 10, tmp.z);
        if (stateMachine.Enemy.Data.Type != EnemyType.Boss)
        {
            stateMachine.Enemy.Agent.isStopped = true;
        }
        else
        {
            Stop();
        }
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.Enemy.Data.Type != EnemyType.Boss)
        {
            stateMachine.Enemy.transform.position += moveVec * Time.deltaTime;
        }
    }

    public override void Exit()
    {
        base.Exit();
        if (stateMachine.Enemy.Data.Type != EnemyType.Boss)
        {
            stateMachine.Enemy.Agent.isStopped = false;
        }
    }

    private void Stop() 
    {
        boss.bodyController.Stop();
    }
}
