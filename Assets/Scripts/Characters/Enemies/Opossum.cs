using ForestValley.Behaviours.Movement;

namespace ForestValley.Characters.Enemies
{
    public class Opossum : Enemy
    {
        public CharacterMover Mover { get; private set; }
        
        private void Awake()
        {
            Mover = GetComponent<CharacterMover>();
        }
        
        void FixedUpdate()
        {
            Mover.Move();    
        }
    }
}
