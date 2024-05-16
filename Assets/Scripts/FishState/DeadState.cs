public class DeadState : FishState
{
    public override void Enter()
    {
        fishManager.onFishDeath.Raise(fishManager.gameObject.GetComponent<FlockAgent>());
    }
    
    public override void Update() { }
    
    public override void Exit() { }
}