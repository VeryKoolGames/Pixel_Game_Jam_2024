using DefaultNamespace;

public class LarryIdleState : LarryState
{
    public override void Enter()
    {
        larryManager.SetDialogueType(DialogueTypes.IDLE);
    }
    
    public override void Update()
    {
        larryManager.Speak();
    }
    
    public override void Exit() { }
}