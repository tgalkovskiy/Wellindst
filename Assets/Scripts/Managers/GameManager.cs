using System;
using System.Collections.Generic;
using ForestValley.Assistants;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ForestValley.Managers
{
    public class GameManager : Singleton<GameManager>, IManager
    {
        #region Vars

        [SerializeField] private List<GameObject> controllers;

        private readonly List<GameObject> loadedControllers = new List<GameObject>();

        public List<GameObject> LoadedControllers
        {
            get { return loadedControllers; }
        }

        public static GameManager gm { get; private set; }
        public static event Action GameBegan;

        private bool gameStarted = false;
        public bool GameStarted
        {
            get => gameStarted;
            set
            {
                gameStarted = value;
                if (gameStarted)
                {
                    StartGame();
                    OnGameBegan();
                }
            }
        } 

        #endregion

        #region ULC

        private void Awake()
        {
            Init();
        }

        #endregion

        #region Public Methods


        public void Init()
        {
            Time.timeScale = 0;
            gm = this;
            

            foreach (var controller in controllers)
            {
                var instance = Instantiate(controller);

                if (instance != null)
                {

                    var iController = instance.GetComponent<IManager>();
                    iController?.Init();

                    loadedControllers.Add(instance);
                }
            }
        }


        public T FindController<T>()
            where T : class
        {

            foreach (var loadedController in loadedControllers)
            {
                if (loadedController.GetComponent<T>() != null)
                {
                    return loadedController.GetComponent<T>();
                }
            }

            return null;
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        #endregion

        private void StartGame()
        {
            Time.timeScale = 1;
        }
        
        
        
        private static void OnGameBegan()
        {
            GameBegan?.Invoke();
        }
    }

}