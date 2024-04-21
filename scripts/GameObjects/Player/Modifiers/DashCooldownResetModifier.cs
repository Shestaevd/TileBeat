using TileBeat.scripts.FSM;

namespace TileBeat.scripts.GameObjects.Player.Modifiers
{
    internal class DashCooldownResetModifier : StateModifier<PlayerEntity>
    {
        public override void EnterModify(PlayerEntity entity, double delta)
        {
            
        }

        public override void ExitModify(PlayerEntity entity, double delta)
        {
            
        }

        public override void UpdateModify(PlayerEntity entity, double delta)
        {
            if (entity.DashCooldownRest > 0) entity.DashCooldownRest -= delta;
        }
    }
}
