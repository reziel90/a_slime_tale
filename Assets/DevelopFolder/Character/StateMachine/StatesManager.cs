using UnityEngine;

public class StatesManager
{
    public State actual_state;
    public State previous_state;
    public StatesManager()
    {
    }

    public void SetNextState(State nextState)
    {
        actual_state?.OnExitState();
        previous_state = actual_state;
        actual_state = nextState;
        actual_state.OnEnterState();
    }

    public void Update()
    {
        actual_state.StateUpdate();
    }

    public void FixedUpdate()
    {
        actual_state.StateFixedUpdate();
    }

}