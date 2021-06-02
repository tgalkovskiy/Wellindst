using UnityEngine;

namespace ForestValley.Behaviours.Movement
{
    
    public class SidesMoveStrategy: IMoveStrategy
    {
        private enum Direction
        {
            Left = -1,
            Right = 1
        }
        
        
        private float leftBound = -5;
        private float rightBound = 5;
        private Direction currentDirection = Direction.Right;
        
        public float Speed { get ; set; }


        public SidesMoveStrategy(float speed, float leftBound, float rightBound)
        {
            this.Speed = speed;
            this.leftBound = leftBound;
            this.rightBound = rightBound;
        }        
        
        
        public void Move(Transform transform)
        {
            if (transform.position.x > rightBound)
            {
                currentDirection = Direction.Left;
            }
            else if (transform.position.x < leftBound)
            {
                currentDirection = Direction.Right;
            }
            
            transform.position += Vector3.right * Speed * (int)currentDirection *Time.deltaTime ;
            
        }
    }
}