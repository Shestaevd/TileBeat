namespace TileBeat.scripts.FSM
{
    public abstract class StateModifier<T>
    {
        abstract public void UpdateModify(T entity, double delta);
        abstract public void EnterModify(T entity, double delta);
        abstract public void ExitModify(T entity, double delta);
    }
}
