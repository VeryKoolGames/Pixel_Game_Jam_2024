public class ReadyToReproduceState : FishState
{
    public override void Enter()
    {
        if (fishManager)
            fishManager.loveParticle.SetActive(true);
    }
    
    public override void Update()
    {
        fishManager.UpdateCheckSexTimer();
    }
    
    public override void Exit()
    {
        if (fishManager)
            fishManager.loveParticle.SetActive(false);
    }
}