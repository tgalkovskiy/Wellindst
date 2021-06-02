
using System;

namespace ForestValley.Behaviours.Health
{
    public interface IHealth
    {
        int HP { get; set; }
        void Die();
    }
}
