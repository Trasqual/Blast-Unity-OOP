namespace BlastGame.StateMachineSystem
{
    public class StateMachine
    {
        private IState _currentState;

        public bool SetState(IState state)
        {
            if (_currentState == null || state.GetType() != _currentState.GetType())
            {
                _currentState = state;
                return true;
            }
            return false;
        }

        public IState GetState()
        {
            return _currentState;
        }
    }
}