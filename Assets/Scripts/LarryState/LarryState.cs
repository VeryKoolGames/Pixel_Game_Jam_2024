public abstract class LarryState
{
    protected LarryManager larryManager;

    public void Initialize(LarryManager larryManager)
    {
        this.larryManager = larryManager;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}