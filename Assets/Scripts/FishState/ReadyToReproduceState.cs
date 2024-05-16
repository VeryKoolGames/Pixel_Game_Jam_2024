public class ReadyToReproduceState : FishState
{
    public override void Enter()
    {
        fishManager.loveParticle.SetActive(true);
    }
    
    public override void Update()
    {
        fishManager.FishSexHandler();
    }
    
    public override void Exit()
    {
        fishManager.loveParticle.SetActive(false);
    }
}