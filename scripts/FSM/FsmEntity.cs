namespace TileBeat.scripts.FSM
{
    public interface FsmEntity<T>
    {
        AbstractState<T> State { get; set; }
    }
}
