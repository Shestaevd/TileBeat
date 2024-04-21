using Godot;
using TileBeat.scripts.FSM;
using TileBeat.scripts.GameObjects.Player;
using TileBeat.scripts.GameObjects.Player.States;

namespace TileBeat.scripts.TestLogic
{
    internal partial class CharTest : Node2D
    {
        private PlayerEntity _player;
        private StateMachine<PlayerEntity> playerFsm = new StateMachine<PlayerEntity>(PlayerStateMap.Idle, PlayerStateMap.Move, PlayerStateMap.Dash);
        public override void _Ready()
        {
            _player = GD.Load<PackedScene>("res://scenes/Player.tscn").Instantiate<PlayerEntity>();
            AddChild(_player);
        }
        public override void _Process(double delta)
        {
            playerFsm.Run(_player, delta);
            GD.Print(_player.GetState().Name);
        }
    }
}
