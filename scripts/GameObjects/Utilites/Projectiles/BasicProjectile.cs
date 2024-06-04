using Godot;
using System.Linq;
using TileBeat.scripts.GameObjects.Entities.Abstract;
using TileBeat.scripts.GameUtils;

namespace TileBeat.scripts.GameObjects.Utilites.Projectiles
{
    public partial class BasicProjectile : Area2D
    {
        private float _ttl;
        private float _damage;
        private float _maxSpeed;
        private float _acceleration;
        private Vector2 _direction = Vector2.Zero;

        private float _accumulativeSpeed = 0f;
        public static BasicProjectile Make(float damage, float maxSpeed, float acceleration, float ttl, Vector2 direction)
        {
            BasicProjectile projectile = GD.Load<PackedScene>(Config.Paths.Scenes.BasicProjectile).Instantiate<BasicProjectile>();
            projectile._damage = damage;
            projectile._maxSpeed = maxSpeed;
            projectile._direction = direction;
            projectile._acceleration = acceleration;
            projectile._ttl = ttl;
            return projectile;
        }

        public static BasicProjectile Spawn(float damage, float speed, float acceleration, float ttl, Vector2 direction, Node2D parent) 
        {
            BasicProjectile projectile = Make(damage, speed, acceleration, ttl, direction);
            parent.AddChild(projectile);
            return projectile;
        }

        public override void _PhysicsProcess(double delta)
        {
            _accumulativeSpeed = _accumulativeSpeed < _maxSpeed ? _accumulativeSpeed + _acceleration : _maxSpeed;
            Vector2 vector = new Vector2((float) (_direction.X * _accumulativeSpeed * delta * 100), (float) (_direction.Y * _accumulativeSpeed * delta * 100));
            Position = Position + vector;

            _ttl = _ttl - (float) delta;

            bool destroy = false;
            GetOverlappingBodies().ToList().ForEach(col => 
            {
                if (col is IDamagable damagable)
                {
                    destroy = true;
                    damagable.DoDamage(_damage, new Some<Vector2>(GlobalPosition));
                } 
                else if (col is StaticBody2D)
                {
                    destroy = true;
                }
            }
            );

            if (destroy || _ttl < 0f) QueueFree();
        }
    }
}
