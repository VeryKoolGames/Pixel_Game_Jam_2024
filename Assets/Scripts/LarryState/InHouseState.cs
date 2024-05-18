using DefaultNamespace;

public class InHouseState : LarryState
{
    public override void Enter()
    {
        larryManager.SetDialogueType(DialogueTypes.INHOUSE);
    }

    public override void Update()
    {
        larryManager.Speak();
    }

    public override void Exit()
    {
    }
}
