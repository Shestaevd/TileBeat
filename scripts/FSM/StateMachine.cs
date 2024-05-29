using System;
using System.Collections.Generic;
using Godot;

namespace TileBeat.scripts.FSM
{
    public class StateMachine<T> where T : FsmEntity<T>
    {
        private List<AbstractState<T>> States = new List<AbstractState<T>>();

        public StateMachine(params AbstractState<T>[] states)
        {
            new List<AbstractState<T>>(states).ForEach(s => AddState(s));
        }

        public StateMachine(List<AbstractState<T>> states)
        {
            states.ForEach(s => AddState(s));
        }

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
                        foreach (AbstractState<T> state in States)
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
                        foreach (AbstractState<T> state in States)
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
                        foreach (AbstractState<T> state in States)
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
        public StateMachine<T> AddState(AbstractState<T> newState)
        {
            foreach (AbstractState<T> state in States)
            {
                if (state.Name.Equals(newState.Name))
                {
                    GD.Print("State with name: " + newState.Name + " already exists");
                    return this;
                }
                if (state.Priority == newState.Priority)
                {
                    GD.Print("State with priority: " + newState.Priority + " already exists");
                    return this;
                }
            }
            States.Add(newState);
            return this;
        }

        public State<T> NewStateInstance(string name, ulong priority)
        {
            State<T> newState = new State<T>(name, priority);
            AddState(newState);
            return newState;
        }
        public State<T> NewStateInstance(string name, ulong priority, Action<T, double> stateLogic)
        {
            State<T> newState = new State<T>(name, priority, stateLogic);
            AddState(newState);
            return newState;
        }
        public State<T> NewStateInstance(string name, ulong priority, Action<T, double> stateLogic, Func<T,double, bool> enterCondition)
        {
            State<T> newState = new State<T>(name, priority, stateLogic, enterCondition);
            AddState(newState);
            return newState;
        }
        public State<T> NewStateInstance(string name, ulong priority, Action<T, double> stateLogic, Action<T, double> onStateEnter, Action<T, double> onStateExit)
        {
            State<T> newState = new State<T>(name, priority, stateLogic, onStateEnter, onStateExit);
            AddState(newState);
            return newState;
        }
        public State<T> NewStateInstance(string name, ulong priority, Action<T, double> stateLogic, Action<T, double> onStateEnter, Action<T, double> onStateExit, Func<T, double, bool> enterCondition)
        {
            State<T> newState = new State<T>(name, priority, stateLogic, onStateEnter, onStateExit, enterCondition);
            AddState(newState);
            return newState;
        }
    }
}
