namespace TileBeat.scripts.FSM
{
    public class StateMachine<T> where T : FsmEntity<T>
    {
        public void Run(T entity, double delta)
        {
            AbstractState<T> previous = entity.State;
            AbstractState<T> currentState = entity.State;
            if (GetNewState(entity, delta, ref currentState))
            {
                previous.OnExit(entity, delta);
                currentState.OnEnter(entity, delta);
                currentState.OnUpdate(entity, delta);
            }
            else
            {
                currentState.OnUpdate(entity, delta);
            }
            entity.State = currentState;
        }
        private bool GetNewState(T entity, double delta, ref AbstractState<T> current)
        {
            if (!current.Lock)
            {
                ulong priority = 0;
                AbstractState<T> previous = current;
                switch (current.WhiteList.Count != 0 ? 1 : current.BlackList.Count != 0 ? -1 : 0)
                {
                    case 1:
                        foreach (AbstractState<T> state in entity.AllStates)
                            if (previous.WhiteList.Exists(s => s.Name == state.Name)
                                && (state.TransitFrom.Count == 0 || state.TransitFrom.Contains(previous))
                                && state.EnterCondition(entity, delta)
                                && priority < state.Priority)
                            {
                                priority = state.Priority;
                                current = state;
                            }
                        break;
                    case -1:
                        foreach (AbstractState<T> state in entity.AllStates)
                        {
                            if (previous.BlackList.TrueForAll(s => s.Name != state.Name)
                                && (state.TransitFrom.Count == 0 || state.TransitFrom.Contains(previous))
                                && state.EnterCondition(entity, delta)
                                && priority < state.Priority)
                            {
                                priority = state.Priority;
                                current = state;
                            }
                        }
                        break;
                    case 0:
                        foreach (AbstractState<T> state in entity.AllStates)
                        {
                            if (state.EnterCondition(entity, delta)
                                && (state.TransitFrom.Count == 0 || state.TransitFrom.Contains(previous))
                                && priority < state.Priority)
                            {
                                priority = state.Priority;
                                current = state;
                            }
                        }
                        break;
                }
                return previous.Name != current.Name;
            }
            else
            {
                return false;
            }
        }
    }
}
