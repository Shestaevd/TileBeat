using System;

namespace TileBeat.scripts.FSM
{
    public class State<T> : AbstractState<T>
    {
        private Action<T, double> _onEnter;
        private Action<T, double> _inState;
        private Action<T, double> _onExit;
        private Func<T, double, bool> EnterConditionD = (_, _) => true;

        public State(string name, ulong priority) : base(name, priority)
        { 
           
        }
        public State(string name, ulong priority, Action<T, double> stateLogic) : base(name, priority)
        {
            _inState = stateLogic;
        }
        public State(string name, ulong priority, Action<T, double> stateLogic, Func<T, double, bool> enterCondition) : base(name, priority)
        {
            _inState = stateLogic;
            EnterConditionD = enterCondition;
        }
        public State(string name, ulong priority, Action<T, double> stateLogic, Action<T, double> onStateEnter, Action<T, double> onStateExit) : base(name, priority)
        {
            _inState = stateLogic;
            _onEnter = onStateEnter;
            _onExit = onStateExit;
        }
        public State(string name, ulong priority, Action<T, double> stateLogic, Action<T, double> onStateEnter, Action<T, double> onStateExit, Func<T, double, bool> enterCondition) : base(name, priority)
        {
            _inState = stateLogic;
            _onEnter = onStateEnter;
            _onExit = onStateExit;
            EnterConditionD = enterCondition;
        }
        override protected void OnEnterLogic(T entity, double delta)
        {
            _onEnter?.Invoke(entity, delta);
        }
        override protected void OnUpdateLogic(T entity, double delta)
        {
            _inState?.Invoke(entity, delta);
        }
        override protected void OnExitLogic(T entity, double delta)
        {
            _onExit?.Invoke(entity, delta);
        }
        override public bool EnterCondition(T entity, double delta)
        {
            if (EnterConditionD == null) return true;
            return EnterConditionD.Invoke(entity, delta);
        }
        public State<T> SetName(string name)
        {
            Name = name;
            return this;
        }
        public State<T> SetOnStateEnter(Action<T, double> enterLogic)
        {
            _onEnter = enterLogic;
            return this;
        }
        public State<T> SetStateLogic(Action<T, double> stateLogic)
        {
            _inState = stateLogic;
            return this;
        }
        public State<T> SetOnStateExit(Action<T, double> onStateExit)
        {
            _onExit = onStateExit;
            return this;
        }
        public new State<T> AddModifier(StateModifier<T> modifier)
        {
            base.AddModifier(modifier);
            return this;
        }
        public new State<T> ToBlack(AbstractState<T> state)
        {
            base.ToBlack(state);
            return this;
        }
        public new State<T> ToWhite(AbstractState<T> state)
        {
            base.ToWhite(state);
            return this;
        }
        public new State<T> ToTransitFrom(AbstractState<T> state)
        {
            base.ToTransitFrom(state);
            return this;
        }
        public State<T> SetEnterCondition(Func<T, double, bool> con)
        {
            EnterConditionD = con;
            return this;
        }
    }
}
