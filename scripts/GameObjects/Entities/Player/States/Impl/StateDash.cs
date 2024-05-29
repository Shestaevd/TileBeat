using Godot;
using TileBeat.scripts.FSM;
using System;
using TileBeat.scripts.GameUtils;

namespace TileBeat.scripts.GameObjects.Player.States.Impl
{
    public class StateDash : AbstractState<PlayerEntity>
    {
        float _timer = 0f;
        public StateDash(string name, ulong priority) : base(name, priority) 
        {

        }

        protected override void OnEnterLogic(PlayerEntity entity, double delta)
        {
            entity.Velocity = Utils.GetAbsoluteDirection() * entity.DashSpeed;
            entity.Animator.Play(Name);
            entity.AudioStreamPlayer.Stream = entity.DashSound;
            entity.AudioStreamPlayer.Play(0);
            _timer = entity.DashTime;
            Lock = true;
        }

        protected override void OnExitLogic(PlayerEntity entity, double delta)
        {
            entity.DashCooldownRest = entity.DashCooldown;
        }

        protected override void OnUpdateLogic(PlayerEntity entity, double delta)
        {
            if (_timer > 0)
            {
                _timer = _timer - (float) delta;
                entity.MoveAndSlide();
            }
            else
            {
                Lock = false;
            }
        }

        public override bool EnterCondition(PlayerEntity entity, double delta)
        {
            return Input.IsActionJustPressed(PlayerControlMapping.InputMapDash) 
                && entity.BeatManger.InBeatRangePrecision(entity.BeatPrecision) > 0;
        }
    }
}
