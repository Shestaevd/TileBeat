using TileBeat.scripts.FSM;
using TileBeat.scripts.GameObjects.Enemies.SimpleEnemy.Modifiers;
using TileBeat.scripts.GameObjects.Enemies.SimpleEnemy.States.Impl;

namespace TileBeat.scripts.GameObjects.Enemies.SimpleEnemy.States
{
    public class SimpleEnemyStateMap
    {

        public static CheckPlayerModifier cpm = new CheckPlayerModifier();

        public static State<SimpleShootingEnemyEntity> Passive = new State<SimpleShootingEnemyEntity>("Passive", (ulong) SimpleEnemyStatePriority.Passive).AddModifier(cpm);

        public static MoveToPlayerState MoveToPlayer = new MoveToPlayerState("MoveToPlayer", (ulong) SimpleEnemyStatePriority.MoveToPlayer);

        public static AttackState Attack = new AttackState("Attack", (ulong) SimpleEnemyStatePriority.Attack);

    }
}
