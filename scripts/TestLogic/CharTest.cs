using Godot;
using TileBeat.scripts.FSM;
using TileBeat.scripts.GameObjects;
using TileBeat.scripts.GameObjects.Enemies.SimpleEnemy;
using TileBeat.scripts.GameObjects.Enemies.SimpleEnemy.States;
using TileBeat.scripts.GameObjects.Player;
using TileBeat.scripts.GameObjects.Player.States;
using TileBeat.scripts.Managers.Beat;
using TileBeat.scripts.UI;

namespace TileBeat.scripts.TestLogic
{
    internal partial class CharTest : Node2D
    {
        private PlayerEntity _player;
        private SimpleShootingEnemyEntity _en;

        private StateMachine<PlayerEntity> playerFsm = 
            new StateMachine<PlayerEntity>(PlayerStateMap.Idle, PlayerStateMap.Run, PlayerStateMap.Dash);
        private StateMachine<SimpleShootingEnemyEntity> enemyFsm = 
            new StateMachine<SimpleShootingEnemyEntity>(SimpleEnemyStateMap.Passive, SimpleEnemyStateMap.MoveToPlayer, SimpleEnemyStateMap.Attack);

        public override void _Ready()
        {
            _player = GD.Load<PackedScene>(Config.Paths.Scenes.Player).Instantiate<PlayerEntity>();
            _en = GD.Load<PackedScene>(Config.Paths.Scenes.BasicEnemy).Instantiate<SimpleShootingEnemyEntity>();

            _player.Position = new Vector2(-160, -200);
            _en.Position = new Vector2(104, -152);

            BeatManager bm = BeatManager.Spawn(TrackBeatMappinig.PixelMurder, this);

            Sprite2D beatBox = GetNode<Sprite2D>("Sprite2D");
            SimpleBeatDrawer sbd = new SimpleBeatDrawer(bm, beatBox, _player.BeatPrecision);
            
            
            AddChild(_player);
            AddChild(_en);
            AddChild(sbd);
            
            bm.VolumeDb += -30;
            bm.Play();

            
        }
        public override void _PhysicsProcess(double delta)
        {

            playerFsm.Run(_player, delta);
            enemyFsm.Run(_en, delta);
        }
    }
}
