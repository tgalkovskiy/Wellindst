using System;
using ForestValley.Behaviours.Health;
using ForestValley.Behaviours.Movement;
using ForestValley.Characters.Enemies;
using ForestValley.Managers;
using UnityEngine;

namespace ForestValley.Characters
{
    [RequireComponent(typeof(CharacterMover))]
    public class PlayerFacade : MonoBehaviour
    {
        public CharacterMover Mover { get; private set; }
        public IHealth Health { get; set; }
        
        private static readonly int Died = Animator.StringToHash("died");
        public static event Action PlayerDied;


        private void Awake()
        {
            Mover = GetComponent<CharacterMover>();
            Health = GetComponent<IHealth>();
        }

        private void OnEnable()
        {
            InputManager.JumpPressed += Mover.Jump;
        }
        
        private void OnDisable()
        {
            InputManager.JumpPressed -= Mover.Jump;
        }


        public void Die()
        {
            Health.Die();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Enemy>() != null)
            {
                GetComponent<Animator>()?.SetTrigger(Died);
                Mover.Freeze = true;
                OnPlayerDied();
                
                Die();
            }
        }

        void FixedUpdate()
        {
            Mover.Move();    
        }


        private static void OnPlayerDied()
        {
            PlayerDied?.Invoke();
        }
    }
}
