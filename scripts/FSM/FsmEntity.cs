using System.Collections.Generic;

namespace TileBeat.scripts.FSM
{
    public interface FsmEntity<T>
    {
        AbstractState<T> State { get; set; }
        List<AbstractState<T>> AllStates { get; }
    }
}
