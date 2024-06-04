using Godot;
using TileBeat.scripts.GameObjects.Player;
using TileBeat.scripts.GameObjects.Player.States;
using System.Linq;

namespace TileBeat.scripts.GameUtils
{
    public static class Utils
    {
        public static Vector2 GetDirection()
        {
            return new Vector2(
                Input.GetActionStrength(PlayerControlMapping.InputMapRight) - Input.GetActionStrength(PlayerControlMapping.InputMapLeft),
                Input.GetActionStrength(PlayerControlMapping.InputMapDown) - Input.GetActionStrength(PlayerControlMapping.InputMapUp)
            ).Normalized();
        }

        public static Vector2 GetAbsoluteDirection() 
        {
            return new Vector2(
                (Input.IsActionPressed(PlayerControlMapping.InputMapRight) ? 1 : 0) - (Input.IsActionPressed(PlayerControlMapping.InputMapLeft) ? 1 : 0),
                (Input.IsActionPressed(PlayerControlMapping.InputMapDown) ? 1 : 0) - (Input.IsActionPressed(PlayerControlMapping.InputMapUp) ? 1 : 0)
            ).Normalized();
        }

        public static Maybe<T> FindOverlappingBody<T>(Area2D area) where T : Node2D
        {
            return Maybe<Node2D>.Of(area.GetOverlappingBodies().ToList().Find(collider => collider is T)).Map(node => (T) node);
        }

        public static Maybe<PlayerEntity> FindOverlappingPlayer(Area2D area)
        {
            return FindOverlappingBody<PlayerEntity>(area);
        }

        public static bool IsPlayerOverlapping(Area2D area)
        {
            return FindOverlappingPlayer(area).IsPresent();
        }
    }
}
