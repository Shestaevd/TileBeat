using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBeat.scripts.FSM
{
    public abstract class StateModifier<T>
    {
        abstract public void UpdateModify(T entity);
        abstract public void EnterModify(T entity);
        abstract public void ExitModify(T entity);
    }
}
