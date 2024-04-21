using Godot;
using TileBeat.scripts.FSM;
using System;
using TileBeat.scripts.GameUtils;

namespace TileBeat.scripts.GameObjects.Player.States.Impl
{
    public class StateDash : AbstractState<PlayerEntity>
    {
        private Vector2 _dashPosition = Vector2.Zero;
        private float _timer = 0f;

        public StateDash(string name, ulong priority) : base(name, priority) { }

        protected override void OnEnterLogic(PlayerEntity entity, double delta)
        {
            _dashPosition = entity.Position + Vector2.One * entity.DashRange;
            _timer = entity.DashTime;
            entity.Velocity = Vector2.Zero;
            Lock = true;
        }

        protected override void OnExitLogic(PlayerEntity entity, double delta)
        {
            entity.DashCooldownRest = entity.DashCooldown;
        }

        protected override void OnUpdateLogic(PlayerEntity entity, double delta)
        {
            
            if (_timer > 0f)
            {
                _timer -= (float)delta;
                float weight = (float) Math.Round((double) (100 - (_timer / (entity.DashTime / 100f))) / 100, 2);
                GD.Print("--------------------");
                GD.Print("timer " + _timer);
                GD.Print("weight " + weight);
                GD.Print("delta " + delta);
                GD.Print("--------------------");
                entity.Position = entity.Position.Lerp(_dashPosition, weight);
            }
            else
            {
                Lock = false;
            }
        }

        public override bool EnterCondition(PlayerEntity entity, double delta)
        {
            return Input.IsActionJustPressed(PlayerControlMapping.InputMapDash) && entity.DashCooldownRest <= 0d;
        }
    }
}
