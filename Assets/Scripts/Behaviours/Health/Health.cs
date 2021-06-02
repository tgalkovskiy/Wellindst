using System;
using UnityEngine;

namespace ForestValley.Behaviours.Health
{
    public class Health: MonoBehaviour, IHealth
    {
        
        private static readonly int Died = Animator.StringToHash("died");
        private int hp = 1;
        
        public int HP
        {
            get { return hp;}
            set
            {
                hp = value;
                if (hp <= 0)
                {
                    Die();
                }
            }
        }


        public void Die()
        {
            
        }
        
    }
}