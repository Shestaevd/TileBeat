using Godot;
using TileBeat.scripts.FSM;
using TileBeat.scripts.GameObjects.Player.Modifiers;
using TileBeat.scripts.GameObjects.Player.States.Impl;

namespace TileBeat.scripts.GameObjects.Player.States
{
    public static class PlayerStateMap
    {

        public static readonly MoveModifier mm = new MoveModifier();
        public static readonly SpriteDirectionModifier sdm = new SpriteDirectionModifier();
        public static readonly DashCooldownResetModifier dcrm = new DashCooldownResetModifier();

        public static State<PlayerEntity> Idle = new State<PlayerEntity>("Idle", (ulong)PlayerStatePriority.Idle)
            .SetOnStateEnter((entity, delta) => 
            {
                entity.Velocity = Vector2.Zero;
                entity.Animator.Play(Idle.Name);
            })
            .SetStateLogic((entity, delta) => entity.MoveAndSlide())
            .AddModifier(dcrm);

        public static State<PlayerEntity> Run = new State<PlayerEntity>("Run", (ulong)PlayerStatePriority.Run)
            .SetEnterCondition((entity, delta) => 
                Input.IsActionPressed(PlayerControlMapping.InputMapDown) || 
                Input.IsActionPressed(PlayerControlMapping.InputMapUp) ||
                Input.IsActionPressed(PlayerControlMapping.InputMapRight) ||
                Input.IsActionPressed(PlayerControlMapping.InputMapLeft)
            )
            .SetOnStateEnter((entity, delta) => entity.Animator.Play(Run.Name))
            .AddModifier(sdm)
            .AddModifier(dcrm)
            .AddModifier(mm);

        public static AbstractState<PlayerEntity> Dash = new StateDash("Dash", (ulong)PlayerStatePriority.Dash);

    }
}