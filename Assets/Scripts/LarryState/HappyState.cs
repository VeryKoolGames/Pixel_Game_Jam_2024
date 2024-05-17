using DefaultNamespace;

public class HappyState : LarryState
{
    public override void Enter()
    {
        larryManager.SetDialogueType(DialogueTypes.HAPPY);
    }
    
    public override void Update()
    {
        larryManager.Speak();
    }
    
    public override void Exit() { }
}