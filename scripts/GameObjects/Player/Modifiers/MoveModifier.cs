using Godot;
using TileBeat.scripts.FSM;
using TileBeat.scripts.GameObjects.Player.States;

namespace TileBeat.scripts.GameObjects.Player.Modifiers
{
    public class MoveModifier : StateModifier<PlayerEntity>
    {
        public override void EnterModify(PlayerEntity entity) { }

        public override void ExitModify(PlayerEntity entity) { }    
        
        public override void UpdateModify(PlayerEntity entity)
        {
            Vector2 direction = new Vector2(
                Input.GetActionStrength(PlayerStateMap.InputMapRight) - Input.GetActionStrength(PlayerStateMap.InputMapLeft),
                Input.GetActionStrength(PlayerStateMap.InputMapDown) - Input.GetActionStrength(PlayerStateMap.InputMapUp)     
            );

            entity.Velocity = direction.Normalized() * entity.Speed;
           
            entity.MoveAndSlide();
        }
    }
}
