using Godot;
using TileBeat.scripts.GameObjects.Player.States;

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
    }
}
