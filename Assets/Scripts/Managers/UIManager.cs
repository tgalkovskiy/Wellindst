using System;
using System.Collections;
using ForestValley.Assistants;
using ForestValley.Characters;
using UnityEngine;

namespace ForestValley.Managers
{
    public class UIManager : Singleton<UIManager>, IManager
    {
        [SerializeField] private GameObject startMenuCanvas;
        [SerializeField] private GameObject deathMenuCanvas;

        private void OnEnable()
        {
            GameManager.GameBegan += DisableStartMenuCanvas;
            PlayerFacade.PlayerDied += EnableDeathCanvas;
        }

        private void OnDisable()
        {
            GameManager.GameBegan -= DisableStartMenuCanvas;
            PlayerFacade.PlayerDied -= EnableDeathCanvas;

        }

        public void Init()
        {
        }

        public void DisableStartMenuCanvas()
        {
            startMenuCanvas.SetActive(false);
        }

        public void EnableDeathCanvas()
        {
            StartCoroutine(EnablingDeathCanvasProcess());
        }

        private IEnumerator EnablingDeathCanvasProcess()
        {
            yield return new WaitForSeconds(2);
            Time.timeScale = 0;
            deathMenuCanvas.SetActive(true);

        }
    }
}
