using Godot;
using TileBeat.scripts.FSM;
using TileBeat.scripts.GameObjects.Player.Modifiers;

namespace TileBeat.scripts.GameObjects.Player.States
{
    public static class PlayerStateMap
    {
        public static readonly string InputMapDown = "move_down";
        public static readonly string InputMapUp = "move_up";
        public static readonly string InputMapRight = "move_right";
        public static readonly string InputMapLeft = "move_left";

        private static readonly MoveModifier mm = new MoveModifier();

        public static State<PlayerEntity> Idle = new State<PlayerEntity>("Idle", (ulong)PlayerStatePriority.Idle)
            .SetOnStateEnter(entity => entity.Velocity = Vector2.Zero)
            .SetStateLogic(entity => entity.MoveAndSlide());

        public static State<PlayerEntity> Move = new State<PlayerEntity>("Move", (ulong)PlayerStatePriority.Move)
            .SetEnterCondition(entity => Input.IsActionPressed(InputMapDown) || Input.IsActionPressed(InputMapUp) || Input.IsActionPressed(InputMapRight) || Input.IsActionPressed(InputMapLeft))
            .AddModifier(mm);

    }
}