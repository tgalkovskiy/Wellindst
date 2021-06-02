
using UnityEngine;

namespace ForestValley.Behaviours.Movement
{
    public interface IMoveStrategy
    {
        public float Speed { get; set; }
        
        void Move(Transform transform);
    }
}
