namespace Player.State
{
    public abstract class PlayerState
    {
        protected PlayerController player;

        public abstract void EnterState();
        
        public abstract void Update(ControllerInput input);
        
        public abstract void ExitState();
    }
}