using Godot;
using TileBeat.scripts.FSM;

namespace TileBeat.scripts.GameObjects.Enemies.SimpleEnemy.States.Impl
{
    public class MoveToPlayerState : AbstractState<SimpleShootingEnemyEntity>
    {
        private uint _localBeatIndex = 0;
        private float _timeToMove = 0;
        private Vector2 _direction = Vector2.Zero;

        public MoveToPlayerState(string name, ulong priority) : base(name, priority) { }

        protected override void OnEnterLogic(SimpleShootingEnemyEntity entity, double delta)
        {
            _localBeatIndex = entity.BeatManager.CurrentBeatIndex();
        }

        protected override void OnExitLogic(SimpleShootingEnemyEntity entity, double delta)
        {
            
        }

        protected override void OnUpdateLogic(SimpleShootingEnemyEntity entity, double delta)
        {
            if (entity.Player != null)
            {
                entity.NavigationAgent.TargetPosition = entity.Player.Position;

                if (_localBeatIndex != entity.BeatManager.CurrentBeatIndex())
                {
                    _direction = entity.Position.DirectionTo(entity.NavigationAgent.GetNextPathPosition());
                    _timeToMove = entity.DashTime;
                    _localBeatIndex = entity.BeatManager.CurrentBeatIndex();
                }

                if (_timeToMove > 0f)
                {
                    _timeToMove -= (float)delta;
                    entity.Velocity = _direction * entity.DashSpeed;
                }
                else
                {
                    entity.Velocity = Vector2.Zero;
                }
                entity.MoveAndSlide();
            }
        }

        public override bool EnterCondition(SimpleShootingEnemyEntity entity, double delta)
        {
            return entity.Player != null;
        }
    }
}
