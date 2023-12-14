public interface IState
{
    public void Enter();
    public void Exit();
    public void HandleInput();
    public void Update();
    public void LateUpdate();
    public void PhysicsUpdate();

}