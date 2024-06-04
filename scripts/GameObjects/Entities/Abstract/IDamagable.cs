using Godot;
using TileBeat.scripts.GameUtils;

namespace TileBeat.scripts.GameObjects.Entities.Abstract
{
    public interface IDamagable
    {
        public void DoDamage(float damage, Maybe<Vector2> sourcePosition);
    }
}
