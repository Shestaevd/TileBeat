using Godot;
using TileBeat.scripts.FSM;
using TileBeat.scripts.GameObjects.Player;

namespace TileBeat.scripts.GameObjects.Entities.Player.States.Impl
{
    public class StateDamaged : AbstractState<PlayerEntity>
    {
        private float _localStunTime = 0f;
        private Vector2 _knockbackInitPower = Vector2.Zero;
        public StateDamaged(string name, ulong priority) : base(name, priority) { }

        protected override void OnEnterLogic(PlayerEntity entity, double delta)
        {
            _localStunTime = entity.StunTime;
            entity.Damaged = false;
            entity.Health = entity.Health - entity.LastDamage;
            _knockbackInitPower = entity.LastSource.Fold(sourcePosition => sourcePosition.DirectionTo(entity.Position)  * entity.KnockBackPower, Vector2.Zero);
            entity.LocalImmunityTime = entity.ImmuneTime;
            entity.Animator.Play(Name);
            Lock = true;
        }

        protected override void OnExitLogic(PlayerEntity entity, double delta)
        {
            
        }

        protected override void OnUpdateLogic(PlayerEntity entity, double delta)
        {
            if (_localStunTime > 0f) 
            {
                _localStunTime = _localStunTime - (float) delta;
                float weight = _localStunTime / (entity.StunTime * 0.01f) / 100f;
                entity.Velocity = Vector2.Zero.Lerp(_knockbackInitPower, weight);
                entity.MoveAndSlide();
            }
            else
            {
                
                Lock = false;
            }
        }

        public override bool EnterCondition(PlayerEntity entity, double delta)
        {
            return entity.Damaged && entity.LocalImmunityTime <= 0f;
        }
    }
}
