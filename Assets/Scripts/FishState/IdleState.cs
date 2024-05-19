public class IdleState : FishState
{
    public override void Enter() { }
    
    public override void Update()
    {
        if (!fishManager) return;
        fishManager.FishLifeHandler();
        fishManager.FishFeedHandler();
        fishManager.FishSexHandler();
        fishManager.CheckForFishion();
    }
    
    public override void Exit() { }
}