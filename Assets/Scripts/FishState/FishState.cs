public abstract class FishState
{
    protected FishReproductionManager fishManager;

    public FishState(FishReproductionManager fishManager)
    {
        this.fishManager = fishManager;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}