using Godot;
using TileBeat.scripts.FSM;
using TileBeat.scripts.GameObjects.Entities.Player.Modifiers;
using TileBeat.scripts.GameObjects.Entities.Player.States.Impl;
using TileBeat.scripts.GameObjects.Player.Modifiers;
using TileBeat.scripts.GameObjects.Player.States.Impl;

namespace TileBeat.scripts.GameObjects.Player.States
{
    public static class PlayerStateMap
    {

        public static readonly MoveModifier mm = new MoveModifier();
        public static readonly SpriteDirectionModifier sdm = new SpriteDirectionModifier();
        public static readonly DashCooldownResetModifier dcrm = new DashCooldownResetModifier();
        public static readonly ImmuneResetModifier irm = new ImmuneResetModifier();

        public static AbstractState<PlayerEntity> Idle = new State<PlayerEntity>("Idle", (ulong)PlayerStatePriority.Idle)
            .SetOnStateEnter((entity, delta) =>
            {
                entity.Velocity = Vector2.Zero;
                entity.Animator.Play(Idle.Name);
            })
            .SetStateLogic((entity, delta) => entity.MoveAndSlide())
            .AddModifier(irm)
            .AddModifier(dcrm);

        public static AbstractState<PlayerEntity> Run = new State<PlayerEntity>("Run", (ulong) PlayerStatePriority.Run)
            .SetEnterCondition((entity, delta) => 
                Input.IsActionPressed(PlayerControlMapping.InputMapDown) || 
                Input.IsActionPressed(PlayerControlMapping.InputMapUp) ||
                Input.IsActionPressed(PlayerControlMapping.InputMapRight) ||
                Input.IsActionPressed(PlayerControlMapping.InputMapLeft)
            )
            .SetOnStateEnter((entity, delta) => entity.Animator.Play(Run.Name))
            .AddModifier(sdm)
            .AddModifier(dcrm)
            .AddModifier(irm)
            .AddModifier(mm);

        public static StateDash Dash() { return new StateDash("Dash", (ulong) PlayerStatePriority.Dash); }

        public static StateDamaged Damaged() { return new StateDamaged("Damaged", (ulong)PlayerStatePriority.Damaged); }
    }
}