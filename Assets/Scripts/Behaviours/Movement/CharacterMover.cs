using System;
using UnityEngine;

namespace ForestValley.Behaviours.Movement
{

    public enum MovementType
    {
        SideToSide,
        StraightMovement
    }

    
    public class CharacterMover : MonoBehaviour, IMove
    {
        [SerializeField] private MovementType movementType;
        
        [Header("Basic movement")]
        [SerializeField] private float speed = 4;
        [SerializeField] private float jumpForce = 100;
        [SerializeField] private int extraJumpCount = 1;
        
        [Header("Side To Side Movement Props")]
        [SerializeField] private float leftSideBound = -5;
        [SerializeField] private float rightSideBound = 5;
        
        private IMoveStrategy moveStrategy;
        private Rigidbody2D rb2d;
        private int currentExtraJumps;
        private bool isGrounded;
        private Collision2D collisionInfo;

        
        public float LeftSideBound { get => leftSideBound; set => leftSideBound = value; }
        public float RightSideBound { get => rightSideBound; set => rightSideBound = value; }
        public MovementType MovementType { get => movementType; set => movementType = value; }
        public IMoveStrategy MoveStrategy { get => moveStrategy; set => moveStrategy = value; }
        public bool Freeze { get; set; } = false;

        public event Action<bool, Collision2D> Landed;


        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            HandleMovementType();

            currentExtraJumps = extraJumpCount;
        }

        private void Update()
        {
 
            GroundCheckProcess();
            
        }

        private void GroundCheckProcess()
        {
            if (rb2d == null)
            {
                return;
            }
            
            isGrounded = Physics2D.OverlapCircle(transform.position, 0.2f, 1 << 3) && 
                         (rb2d.velocity.y < Mathf.Epsilon);
            if (isGrounded)
            {
                currentExtraJumps = extraJumpCount;
                OnLanded(isGrounded, collisionInfo);
            }
            else
            {
                collisionInfo = null;
            }
        }
        
        private void OnCollisionStay2D(Collision2D other)
        {
            collisionInfo = other;
        }

        public void Move()
        {
            if (!Freeze)
                moveStrategy.Move(transform);
        }

        public void Jump()
        {
            if (rb2d == null)
                return;
            
            if (currentExtraJumps > 0)
            {
                rb2d.velocity = new Vector2(0,jumpForce);
                currentExtraJumps--;
            }
            else if (currentExtraJumps == 0 && isGrounded)
            {
                rb2d.velocity = new Vector2(0,jumpForce);
            }
            
        }
        

        
        
        private void HandleMovementType()
        {
            Component c = gameObject.GetComponent<IMoveStrategy>() as Component;
            if(c != null)
            {
                Destroy(c);
            }

            switch (movementType)
            {
                case MovementType.SideToSide:
                    moveStrategy = new SidesMoveStrategy(speed, leftSideBound, rightSideBound);
                    break;
                case MovementType.StraightMovement:
                    moveStrategy = new StraightMoveStrategy(speed);
                    break;
                    
                
            }

        }

        protected virtual void OnLanded(bool grounded, Collision2D collisionInfo)
        {
            Landed?.Invoke(grounded, collisionInfo);
        }
    }
}
