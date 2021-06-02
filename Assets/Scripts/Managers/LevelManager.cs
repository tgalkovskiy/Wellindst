using ForestValley.Assistants;
using ForestValley.Characters;
using ForestValley.Environment;
using ForestValley.Environment.Platforms;
using UnityEngine;

namespace ForestValley.Managers
{
    public class LevelManager : Singleton<LevelManager>, IManager
    {

        [SerializeField] private PlatformsGenerator levelGeneratorPrefab;
        [SerializeField] private PlayerFacade playerFacadePrefab;

        public PlatformsGenerator PlatformsGeneratorInstance { get; private set; }
        public PlayerFacade PlayerFacadeInstance { get; private set; }

        public void Init()
        {
            PlatformsGeneratorInstance = Instantiate(levelGeneratorPrefab, transform);
            PlayerFacadeInstance = Instantiate(playerFacadePrefab);
            
            
        }
    }
}
