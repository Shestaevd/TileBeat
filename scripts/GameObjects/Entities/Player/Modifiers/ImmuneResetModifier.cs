using TileBeat.scripts.FSM;
using TileBeat.scripts.GameObjects.Player;

namespace TileBeat.scripts.GameObjects.Entities.Player.Modifiers
{
    public class ImmuneResetModifier : StateModifier<PlayerEntity>
    {
        public override void EnterModify(PlayerEntity entity, double delta)
        {
           
        }

        public override void ExitModify(PlayerEntity entity, double delta)
        {
            
        }

        public override void UpdateModify(PlayerEntity entity, double delta)
        {
            if (entity.LocalImmunityTime > 0f) entity.LocalImmunityTime = entity.LocalImmunityTime - (float) delta;
        }
    }
}
