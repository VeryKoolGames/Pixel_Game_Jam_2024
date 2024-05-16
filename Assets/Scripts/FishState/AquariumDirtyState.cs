public class AquariumDirtyState : FishState
{
    public override void Enter()
    {
        
    }
    
    public override void Update()
    {
        fishManager.FishLifeHandler();
        fishManager.FishFeedHandler();
    }
    
    public override void Exit() { }
}