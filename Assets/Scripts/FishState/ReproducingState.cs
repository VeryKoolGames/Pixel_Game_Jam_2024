using UnityEngine;

public class ReproducingState : FishState
{
    private FishReproductionManager otherFish;
    private Vector2 position;

    public ReproducingState(FishReproductionManager fishManager, FishReproductionManager otherFish, Vector2 position) : base(fishManager)
    {
        this.otherFish = otherFish;
        this.position = position;
    }

    public override void Enter()
    {
        fishManager._flockAgent.sexSpot = position;
        otherFish._flockAgent.sexSpot = position;
        fishManager._flockAgent.isHavingSex = true;
        otherFish._flockAgent.isHavingSex = true;
        fishManager.StartCoroutine(fishManager.WaitForSex(otherFish));
    }

    public override void Update() { }

    public override void Exit()
    {
        fishManager.currentReproductionRate = 0;
        otherFish.currentReproductionRate = 0;
        fishManager._flockAgent.isHavingSex = false;
        otherFish._flockAgent.isHavingSex = false;
    }
}