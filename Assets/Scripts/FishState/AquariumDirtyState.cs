public class AquariumDirtyState : FishState
{
    public AquariumDirtyState(FishReproductionManager fishManager) : base(fishManager) { }

    public override void Enter()
    {
        
    }
    
    public override void Update()
    {
        fishManager.FishLifeHandler();
        fishManager.FishFeedHandler();
        fishManager.FishSexHandler();
    }
    
    public override void Exit() { }
}