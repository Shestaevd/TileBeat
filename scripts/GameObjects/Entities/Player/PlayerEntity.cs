using Godot;
using TileBeat.scripts.FSM;
using TileBeat.scripts.GameObjects.Player.States;
using TileBeat.scripts.Loaders;
using TileBeat.scripts.Managers.Beat;

namespace TileBeat.scripts.GameObjects.Player
{
    public partial class PlayerEntity : CharacterBody2D, FsmEntity<PlayerEntity>
    {
        [Export] public float Speed = 200f;

        [Export] public float DashSpeed = 500f;
        [Export] public float DashTime = 0.2f;
        [Export] public float DashCooldown = 0.3f;

        [Export] public float BeatPrecision = 0.15f;

        public float DashCooldownRest = 0f;

        public BeatManager BeatManger;
        public AnimatedSprite2D Animator;
        public AudioStreamPlayer2D AudioStreamPlayer;
        public AudioListener2D AudioListener;

        private AbstractState<PlayerEntity> state = PlayerStateMap.Idle;

        public AudioStream DashSound = AudioStreamLoader.LoadDashSound();

        public AbstractState<PlayerEntity> State { get => state; set => state = value; }

        public override void _Ready ()
        {
            Animator = GetNode<AnimatedSprite2D>("PlayerAnimatedSprite2D");
            AudioStreamPlayer = GetNode<AudioStreamPlayer2D>("PlayerAudio2D");
            AudioListener = GetNode<AudioListener2D>("AudioListener2D");
            BeatManger = GetNode<BeatManager>("../BeatManager");
        }

    }
}
