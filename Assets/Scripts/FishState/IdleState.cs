public class IdleState : FishState
{
    public override void Enter() { }
    
    public override void Update()
    {
        fishManager.FishLifeHandler();
        fishManager.FishFeedHandler();
        fishManager.FishSexHandler();
        fishManager.UpdateCheckSexTimer();
    }
    
    public override void Exit() { }
}