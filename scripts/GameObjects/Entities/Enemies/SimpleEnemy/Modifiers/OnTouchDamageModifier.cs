using Godot;
using TileBeat.scripts.FSM;
using TileBeat.scripts.GameObjects.Enemies.SimpleEnemy;
using TileBeat.scripts.GameUtils;

namespace TileBeat.scripts.GameObjects.Entities.Enemies.SimpleEnemy.Modifiers
{
    public class OnTouchDamageModifier : StateModifier<SimpleShootingEnemyEntity>
    {
        public override void EnterModify(SimpleShootingEnemyEntity entity, double delta)
        {
            
        }

        public override void ExitModify(SimpleShootingEnemyEntity entity, double delta)
        {
            
        }

        public override void UpdateModify(SimpleShootingEnemyEntity entity, double delta)
        {
            Utils.FindOverlappingPlayer(entity.AreaDamageOnTouch).ForEach(pe => pe.DoDamage(entity.OnTouchDamage, new Some<Vector2>(entity.Position)));
        }
    }
}
