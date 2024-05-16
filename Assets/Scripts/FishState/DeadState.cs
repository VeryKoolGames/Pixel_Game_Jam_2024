public class DeadState : FishState
{
    public DeadState(FishReproductionManager fishManager) : base(fishManager) { }

    public override void Enter()
    {
        fishManager.onFishDeath.Raise(fishManager.gameObject.GetComponent<FlockAgent>());
    }
    
    public override void Update() { }
    
    public override void Exit() { }
}