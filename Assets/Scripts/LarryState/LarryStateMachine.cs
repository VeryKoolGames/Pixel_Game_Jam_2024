public class LarryStateMachine
{
    private LarryState currentState;

    public LarryState CurrentState => currentState;

    public void ChangeState(LarryState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        
        currentState = newState;
        
        if (currentState != null)
        {
            currentState.Enter();
        }
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        }
    }
}