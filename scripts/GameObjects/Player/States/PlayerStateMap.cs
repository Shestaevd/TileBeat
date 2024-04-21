using Godot;
using TileBeat.scripts.FSM;
using TileBeat.scripts.GameObjects.Player.Modifiers;
using TileBeat.scripts.GameObjects.Player.States.Impl;

namespace TileBeat.scripts.GameObjects.Player.States
{
    public static class PlayerStateMap
    {

        private static readonly MoveModifier mm = new MoveModifier();

        public static State<PlayerEntity> Idle = new State<PlayerEntity>("Idle", (ulong)PlayerStatePriority.Idle)
            .SetOnStateEnter((entity, delta) => entity.Velocity = Vector2.Zero)
            .SetStateLogic((entity, delta) => entity.MoveAndSlide());

        public static State<PlayerEntity> Move = new State<PlayerEntity>("Move", (ulong)PlayerStatePriority.Move)
            .SetEnterCondition((entity, delta) => 
                Input.IsActionPressed(PlayerControlMapping.InputMapDown) || 
                Input.IsActionPressed(PlayerControlMapping.InputMapUp) ||
                Input.IsActionPressed(PlayerControlMapping.InputMapRight) ||
                Input.IsActionPressed(PlayerControlMapping.InputMapLeft)
            )
            .AddModifier(mm);

        public static StateDash Dash = new StateDash("Dash", (ulong) PlayerStatePriority.Dash);

    }
}