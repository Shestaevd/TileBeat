using TileBeat.scripts.FSM;
using TileBeat.scripts.GameObjects.Player;
using TileBeat.scripts.GameUtils;

namespace TileBeat.scripts.GameObjects.Enemies.SimpleEnemy.Modifiers
{
    public class CheckPlayerModifier : StateModifier<SimpleShootingEnemyEntity>
    {
        public override void EnterModify(SimpleShootingEnemyEntity entity, double delta)
        {
            
        }

        public override void ExitModify(SimpleShootingEnemyEntity entity, double delta)
        {
            
        }

        public override void UpdateModify(SimpleShootingEnemyEntity entity, double delta)
        {
            if (entity.Player == null)
                Utils.FindOverlappingPlayer(entity.AreaAgro).ForEach(pe => entity.Player = pe);
        }
    }
}
