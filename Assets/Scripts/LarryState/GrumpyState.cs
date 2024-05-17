using DefaultNamespace;

public class GrumpyState : LarryState
{
    public override void Enter()
    {
        larryManager.SetDialogueType(DialogueTypes.GRUMPY);
    }

    public override void Update()
    {
        larryManager.Speak();
    }

    public override void Exit()
    {
    }
}
