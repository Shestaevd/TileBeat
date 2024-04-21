using Godot;
using TileBeat.scripts.FSM;
using TileBeat.scripts.GameObjects.Player.States;

namespace TileBeat.scripts.GameObjects.Player
{
    public partial class PlayerEntity : CharacterBody2D, FsmEntity<PlayerEntity>
    {
        [Export] public float Speed = 100f;

        [Export] public float DashRange = 100f;
        [Export] public float DashTime = 0.05f;
        [Export] public double DashCooldown = 0.3f;
        public double DashCooldownRest = 0f;

        

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
