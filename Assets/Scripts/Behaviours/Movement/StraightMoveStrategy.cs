using UnityEngine;

namespace ForestValley.Behaviours.Movement
{
    public class StraightMoveStrategy: IMoveStrategy
    {
        public float Speed { get; set; }

        public StraightMoveStrategy(float speed)
        {
            Speed = speed;
        }
        
        public void Move(Transform transform)
        {
            transform.position += Vector3.right * Speed * Time.deltaTime;
        }
    }
}