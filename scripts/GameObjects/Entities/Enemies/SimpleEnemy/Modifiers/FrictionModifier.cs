using Godot;
using TileBeat.scripts.FSM;
using TileBeat.scripts.GameObjects.Enemies.SimpleEnemy;

namespace TileBeat.scripts.GameObjects.Entities.Enemies.SimpleEnemy.Modifiers
{
    public class FrictionModifier : StateModifier<SimpleShootingEnemyEntity>
    {
        
        private float _localFrictionTime;
        
        public override void EnterModify(SimpleShootingEnemyEntity entity, double delta)
        {
            _localFrictionTime = entity.FrictionTime;
        }

        public override void ExitModify(SimpleShootingEnemyEntity entity, double delta)
        {
            
        }

        public override void UpdateModify(SimpleShootingEnemyEntity entity, double delta)
        {
            if (entity.Velocity.IsZeroApprox()) _localFrictionTime = 0f;

            if (_localFrictionTime > 0f)
            {
                _localFrictionTime = _localFrictionTime - (float) delta;
                entity.Velocity = Vector2.Zero.Lerp(entity.Velocity, _localFrictionTime / (entity.FrictionTime / 100f) / 100f);
            }
            else
            {
                entity.Velocity = Vector2.Zero;
            }

            entity.MoveAndSlide();
        }
    }
}
