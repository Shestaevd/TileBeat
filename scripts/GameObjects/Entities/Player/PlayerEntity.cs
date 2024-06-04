using Godot;
using System.Collections.Generic;
using TileBeat.scripts.FSM;
using TileBeat.scripts.GameObjects.Entities.Abstract;
using TileBeat.scripts.GameObjects.Player.States;
using TileBeat.scripts.GameUtils;
using TileBeat.scripts.Loaders;
using TileBeat.scripts.Managers.Beat;

namespace TileBeat.scripts.GameObjects.Player
{
    public partial class PlayerEntity : CharacterBody2D, FsmEntity<PlayerEntity>, IDamagable
    {
        [ExportCategory("Move state")]
        [Export] public float Speed = 200f;

        [ExportCategory("Dash state")]
        [Export] public float DashSpeed = 500f;
        [Export] public float DashTime = 0.2f;
        [Export] public float DashCooldown = 0.3f;

        [ExportCategory("Damaged state")]
        [Export] public float StunTime = 0.2f;
        [Export] public float ImmuneTime = 1f;
        [Export] public float KnockBackPower = 100f;
        [Export] public float KnockBackFriction = 0.5f;

        [ExportCategory("Properties")]
        [Export] public float BeatPrecision = 0.15f;
        [Export] public float Health = 100f;

        // nodes
        public BeatManager BeatManger;
        public AnimatedSprite2D Animator;
        public AudioStreamPlayer2D AudioStreamPlayer;
        public AudioListener2D AudioListener;

        // sounds
        public AudioStream DashSound = AudioStreamLoader.LoadDashSound();

        // fsmEntity impl
        public AbstractState<PlayerEntity> State { get => _state; set => _state = value; }
        public List<AbstractState<PlayerEntity>> AllStates => _states;
        private AbstractState<PlayerEntity> _state = PlayerStateMap.Idle;
        private List<AbstractState<PlayerEntity>> _states = new(new AbstractState<PlayerEntity>[]{ PlayerStateMap.Idle, PlayerStateMap.Dash(), PlayerStateMap.Run, PlayerStateMap.Damaged() });

        // public mutable properties
        // state damaged
        public bool Damaged = false;
        public float LastDamage = 0f;
        public Maybe<Vector2> LastSource = new None<Vector2>();
        public float LocalImmunityTime = 0f;

        public override void _Ready ()
        {
            Animator = GetNode<AnimatedSprite2D>("PlayerAnimatedSprite2D");
            AudioStreamPlayer = GetNode<AudioStreamPlayer2D>("PlayerAudio2D");
            AudioListener = GetNode<AudioListener2D>("AudioListener2D");
            BeatManger = GetNode<BeatManager>("../BeatManager");
        }

        public void DoDamage(float damage, Maybe<Vector2> source)
        {
            if (LocalImmunityTime <= 0)
            {
                LastDamage = damage;
                Damaged = true;
                LastSource = source;
            }
        }
    }
}
