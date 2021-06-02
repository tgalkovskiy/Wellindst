using System;
using ForestValley.Assistants;
using ForestValley.Characters;
using ForestValley.Characters.Enemies;
using ForestValley.Managers;
using UnityEngine;

namespace ForestValley.Environment.Platforms
{
    public class Platform : MonoBehaviour, IPool
    {
        public GameObject Next { get; set; }
        public bool Beaten { get; set; } = false;
        public static event Action PlatformBeaten;
        
        [SerializeField] private Animator animator;
        [SerializeField] private float distanceToReturnToPool = 2;
        
        private Collider2D ourCollider;
        private PlayerFacade playerFacade;
        private static readonly int Grounded = Animator.StringToHash("grounded");
        public static event Action PlatformSetInactive;
        

        private void Awake()
        {
            ourCollider = GetComponent<Collider2D>();
        }

        private void Start()
        {
            var levelManager = GameManager.gm.FindController<LevelManager>();
            playerFacade = levelManager.PlayerFacadeInstance;
            print(playerFacade);
        }
        
        private void Update()
        {
            if (transform.position.y + distanceToReturnToPool < playerFacade.transform.position.y)
            {
                SetInactive();
            }
        }

        
        public void OvercomeCheckProcess(bool grounded, Collision2D collisionInfo)
        {
            
            if (collisionInfo != null && ReferenceEquals(collisionInfo.collider, ourCollider))
            {
                if (!Beaten)
                {
                    OnPlatformBeaten();
                }
                Beaten = true;
                
                animator.SetBool(Grounded, grounded);
            }
            else
            {
                animator.SetBool(Grounded, false);
            }
        }

        public void InitPoolItem()
        {
            Beaten = false;
        }
        
        private void SetInactive()
        {
            
            gameObject.SetActive(false);
            OnPlatformSetInactive();
            
        }
        
        private static void OnPlatformBeaten()
        {
            PlatformBeaten?.Invoke();
        }

        private static void OnPlatformSetInactive()
        {
            PlatformSetInactive?.Invoke();
        }
    }
}
