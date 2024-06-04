using Godot;
using System.Collections.Generic;
using TileBeat.scripts.FSM;
using TileBeat.scripts.GameObjects.Enemies.SimpleEnemy.States;
using TileBeat.scripts.GameObjects.Player;
using TileBeat.scripts.Managers.Beat;

namespace TileBeat.scripts.GameObjects.Enemies.SimpleEnemy
{
    public partial class SimpleShootingEnemyEntity : CharacterBody2D, FsmEntity<SimpleShootingEnemyEntity>
    {
        [ExportCategory("Move state")]
        [Export] public float DashSpeed = 100f;
        [Export] public float DashTime = 0.2f;
        [ExportSubgroup("not implemented")]
        [Export] public uint ConsistentDashes = 3; // not implemented
        [Export] public uint SkipBeatsToRest = 2;  // not implemented

        [ExportCategory("Attack state")]
        [Export] public float BasicAttackDamage = 10f;
        [Export] public float MaxProjectileSpeed = 10f;
        [Export] public float ProjectileAcceleration = 0.2f;
        [Export] public float ProjectileTTL = 2f;

        [ExportCategory("Friction state modifier")]
        [Export] public float FrictionPower = 1f;
        [Export] public float FrictionTime = 1f;

        [ExportCategory("Properties")]
        [Export] public float Health = 100f;
        [Export] public float OnTouchDamage = 10f;

        public PlayerEntity Player;
        public BeatManager BeatManager;
        public AnimatedSprite2D Animator;
        public AudioStreamPlayer2D AudioStreamPlayer;
        public CollisionShape2D CollisionShapeDamage;
        public Area2D AreaAgro;
        public Area2D AreaDamageOnTouch;
        public NavigationAgent2D NavigationAgent;

        private AbstractState<SimpleShootingEnemyEntity> state = SimpleEnemyStateMap.Passive;
        private List<AbstractState<SimpleShootingEnemyEntity>> _states = new(new AbstractState<SimpleShootingEnemyEntity>[] { SimpleEnemyStateMap.Passive, SimpleEnemyStateMap.MoveToPlayer(), SimpleEnemyStateMap.Attack() });
        public AbstractState<SimpleShootingEnemyEntity> State { get => state; set => state = value; }
        public List<AbstractState<SimpleShootingEnemyEntity>> AllStates => _states;



        public override void _Ready()
        {
            Animator = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
            AudioStreamPlayer = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
            CollisionShapeDamage = GetNode<CollisionShape2D>("CollisionShapeDamage");
            AreaAgro = GetNode<Area2D>("AreaAgro");
            AreaDamageOnTouch = GetNode<Area2D>("AreaDamageOnTouch");
            NavigationAgent = GetNode<NavigationAgent2D>("NavigationAgent2D");
            BeatManager = GetNode<BeatManager>("../BeatManager");

        }
    }
}
