using TileBeat.scripts.FSM;
using TileBeat.scripts.GameUtils;

namespace TileBeat.scripts.GameObjects.Player.Modifiers
{
    public class SpriteDirectionModifier : StateModifier<PlayerEntity>
    {
        private bool _isFliped = false;
        public override void EnterModify(PlayerEntity entity, double delta)
        {
            
        }

        public override void ExitModify(PlayerEntity entity, double delta)
        {
            
        }

        public override void UpdateModify(PlayerEntity entity, double delta)
        {
            float X = Utils.GetDirection().X;
            _isFliped = X < 0 ? true : X > 0 ? false : _isFliped;
            entity.Animator.FlipH = _isFliped;
        }
    }
}
