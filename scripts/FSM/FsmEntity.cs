namespace TileBeat.scripts.FSM
{
    public interface FsmEntity<T>
    {
        public AbstractState<T> GetState();
        public void SetState(AbstractState<T> state);
    }
}
