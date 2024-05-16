public class IdleState : FishState
{
    public IdleState(FishReproductionManager fishManager) : base(fishManager) { }

    public override void Enter() { }
    
    public override void Update()
    {
        fishManager.FishLifeHandler();
        fishManager.FishFeedHandler();
        fishManager.FishSexHandler();
        fishManager.CheckNearbyFishForSex();
    }
    
    public override void Exit() { }
}