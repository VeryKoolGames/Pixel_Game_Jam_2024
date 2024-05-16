public class StateMachine
{
    private FishState currentState;

    public FishState CurrentState => currentState;

    public void ChangeState(FishState newState)
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