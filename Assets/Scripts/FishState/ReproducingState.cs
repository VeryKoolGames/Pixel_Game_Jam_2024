using UnityEngine;

public class ReproducingState : FishState
{
    private FishReproductionManager otherFish;

    public ReproducingState(FishReproductionManager fishManager, FishReproductionManager otherFish) : base(fishManager)
    {
        this.otherFish = otherFish;
    }

    public override void Enter()
    {
        fishManager.StartCoroutine(fishManager.WaitForSex(otherFish));
    }

    public override void Update() { }

    public override void Exit() { }
}