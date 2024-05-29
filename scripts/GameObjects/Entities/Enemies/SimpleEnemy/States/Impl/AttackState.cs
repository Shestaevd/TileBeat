using TileBeat.scripts.FSM;
using TileBeat.scripts.GameObjects.Entities.Enemies.SimpleEnemy.Modifiers;
using TileBeat.scripts.GameObjects.Utilites.Projectiles;
using TileBeat.scripts.GameUtils;

namespace TileBeat.scripts.GameObjects.Enemies.SimpleEnemy.States.Impl
{
    public class AttackState : AbstractState<SimpleShootingEnemyEntity>
    {
        private uint _localBeatIndex = 0;

        public AttackState(string name, ulong priority) : base(name, priority) 
        {
            AddModifier(new FrictionModifier());
        }

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
                    _localBeatIndex = entity.BeatManager.CurrentBeatIndex();
                    BasicProjectile.Spawn(
                        entity.BasicAttackDamage, 
                        entity.MaxProjectileSpeed, 
                        entity.ProjectileAcceleration, 
                        entity.ProjectileTTL, 
                        entity.Position.DirectionTo(entity.Player.Position), 
                        entity
                    );
                }
            }
        }

        public override bool EnterCondition(SimpleShootingEnemyEntity entity, double delta)
        {
            return Utils.IsPlayerOverlapping(entity.AreaAgro) && entity.Player != null;
        }
    }
}
