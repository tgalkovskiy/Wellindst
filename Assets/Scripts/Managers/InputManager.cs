using System;
using UnityEngine;

namespace ForestValley.Managers
{
    public class InputManager : MonoBehaviour
    {

        
        public static event Action JumpPressed;
        
        void Update()
        {
            if (Input.GetMouseButtonDown(0) && GameManager.gm.GameStarted == false)
            {
                GameManager.gm.GameStarted = true;
            }
            if (Input.GetMouseButtonDown(0))
            {
                OnJumpPressed();
            }
        }

        private static void OnJumpPressed()
        {
            JumpPressed?.Invoke();
        }
    }
}
