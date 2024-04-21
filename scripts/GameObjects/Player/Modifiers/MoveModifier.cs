using Godot;
using TileBeat.scripts.FSM;
using TileBeat.scripts.GameUtils;

namespace TileBeat.scripts.GameObjects.Player.Modifiers
{
    public class MoveModifier : StateModifier<PlayerEntity>
    {
        public override void EnterModify(PlayerEntity entity, double delta) { }

        public override void ExitModify(PlayerEntity entity, double delta) { }    
        
        public override void UpdateModify(PlayerEntity entity, double delta)
        {
            Vector2 direction = Utils.GetDirection();

            entity.Velocity = direction * entity.Speed;
           
            entity.MoveAndSlide();
        }
    }
}
