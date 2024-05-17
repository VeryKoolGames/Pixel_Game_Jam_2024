using DefaultNamespace;

public class EarlyGameState : LarryState
{
    public override void Enter()
    {
        larryManager.SetDialogueType(DialogueTypes.EARLYGAME);
    }

    public override void Update()
    {
        larryManager.Speak();
    }

    public override void Exit()
    {
    }
}
