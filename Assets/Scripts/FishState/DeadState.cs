public class DeadState : FishState
{
    public override void Enter()
    {
        if (fishManager)
            fishManager.onFishDeath.Raise(fishManager.gameObject.GetComponent<FlockAgent>());
    }
    
    public override void Update() { }
    
    public override void Exit() { }
}