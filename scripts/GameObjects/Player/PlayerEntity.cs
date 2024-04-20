using Godot;
using TileBeat.scripts.FSM;
using TileBeat.scripts.GameObjects.Player.States;

namespace TileBeat.scripts.GameObjects.Player
{
    public partial class PlayerEntity : CharacterBody2D, FsmEntity<PlayerEntity>
    {
        [Export]
        public float Speed = 100f;
        [Export]
        public float Friction = 50f;

        public AbstractState<PlayerEntity> state = PlayerStateMap.Idle;

        public AbstractState<PlayerEntity> GetState()
        {
            return state;
        }

        public void SetState(AbstractState<PlayerEntity> state)
        {
            this.state = state;
        }
    }
}
