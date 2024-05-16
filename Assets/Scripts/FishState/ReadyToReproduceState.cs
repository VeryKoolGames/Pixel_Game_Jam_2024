public class ReadyToReproduceState : FishState
{
    public ReadyToReproduceState(FishReproductionManager fishManager) : base(fishManager) { }

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