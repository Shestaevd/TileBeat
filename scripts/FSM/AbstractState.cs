using System.Collections.Generic;

namespace TileBeat.scripts.FSM
{

    public abstract class AbstractState<T>
    {
        public List<AbstractState<T>> BlackList = new List<AbstractState<T>>();
        public List<AbstractState<T>> WhiteList = new List<AbstractState<T>>();
        public List<AbstractState<T>> TransitFrom = new List<AbstractState<T>>();

        private List<StateModifier<T>> Modifiers = new List<StateModifier<T>>();

        public bool Lock = false;
        public ulong Priority = 0;

        public string Name;

        public AbstractState(string name, ulong priority)
        {
            Name = name;
            Priority = priority;
        }

        public void OnEnterModifier(T entity, double delta)
        {
            Modifiers.ForEach(modifier => modifier.EnterModify(entity, delta));
        }
        public void UpdateModifier(T entity, double delta)
        {
            Modifiers.ForEach(modifier => modifier.UpdateModify(entity, delta));
        }
        public void OnExitModifier(T entity, double delta)
        {
            Modifiers.ForEach(modifier => modifier.ExitModify(entity, delta));
        }

        abstract protected void OnEnterLogic(T entity, double delta);
        abstract protected void OnUpdateLogic(T entity, double delta);
        abstract protected void OnExitLogic(T entity, double delta);
        public void OnEnter(T entity, double delta)
        {
            OnEnterLogic(entity, delta);
            Modifiers.ForEach(m => m.EnterModify(entity, delta));
        }
        public void OnUpdate(T entity, double delta)
        {
            OnUpdateLogic(entity, delta);
            Modifiers.ForEach(m => m.UpdateModify(entity, delta));
        }
        public void OnExit(T entity, double delta)
        {
            OnExitLogic(entity, delta);
            Modifiers.ForEach(m => m.ExitModify(entity, delta));
        }
        public void AddModifier(StateModifier<T> m)
        {
            Modifiers.Add(m);
        }
        virtual public bool EnterCondition(T entity, double delta)
        {
            return true;
        }
        public AbstractState<T> ToBlack(AbstractState<T> state)
        {
            BlackList.Add(state);
            return this;
        }
        public AbstractState<T> ToWhite(AbstractState<T> state)
        {
            WhiteList.Add(state);
            return this;
        }

        public AbstractState<T> ToTransitFrom(AbstractState<T> state)
        {
            TransitFrom.Add(state);
            return this;
        }
    }

}