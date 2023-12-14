public class EnemyStateMachine : StateMachine
{
    public Enemy Enemy { get; }

    public EnemyChasingState ChasingState { get; }
    public EnemyAttackState AttackState { get; }
    public EnemyWanderingState WanderingState { get; }
    public EnemyDeathState DeathState { get; }
    public PlayerConditions Target { get; private set; }


    public EnemyStateMachine(Enemy enemy)
    {
        Enemy = enemy;

        ChasingState = new EnemyChasingState(this);
        AttackState = new EnemyAttackState(this);
        WanderingState = new EnemyWanderingState(this);
        DeathState = new EnemyDeathState(this);
    }

    public void Targeting()
    {
        Target = GamePlaySceneManager.Instance.Player.GetComponentInChildren<PlayerConditions>(); // 나중에 게임매니저에서 찾아서 받는 걸로 수정하기
    }
}
