public abstract class FishState
{
    protected FishReproductionManager fishManager;

    public void Initialize(FishReproductionManager fishManager)
    {
        this.fishManager = fishManager;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}