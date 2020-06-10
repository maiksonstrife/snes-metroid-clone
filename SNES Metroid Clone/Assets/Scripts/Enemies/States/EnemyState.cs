using Control;

namespace Enemies.States
{
    public abstract class EnemyState
    {
        protected CrawlingBehavior _root;
        protected CharacterController2D _CC2D;
        
        public abstract void EnterState();

        public abstract void Update();

        public abstract void ExitState();
        
    }
}