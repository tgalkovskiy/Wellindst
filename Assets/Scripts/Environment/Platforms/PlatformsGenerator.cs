using System.Collections;
using ForestValley.Characters.Enemies;
using ForestValley.Managers;
using UnityEngine;

namespace ForestValley.Environment.Platforms
{
    
    public class PlatformsGenerator : MonoBehaviour
    {
        public static int PlatformsBeatenCount { get; set; } = 0;

        [SerializeField] private int startSpawningCount = 5;
        [SerializeField] private int safePlatformsCount = 3;
        [SerializeField] private float distanceBetweenPlatforms = 0.5f;
        [SerializeField] private Vector2 startSpawnPoint;
        
        private PoolManager<SimplePlatform> simplePlatformPoolManager;
        private PoolManager<EnemyPlatform> enemyPlatformPoolManager;
        private Vector2 nextSpawnPoint;


        private void Start()
        {

            GameManager gameManager = GameManager.gm;
            simplePlatformPoolManager = gameManager.FindController<PoolManager<SimplePlatform>>();
            enemyPlatformPoolManager = gameManager.FindController<PoolManager<EnemyPlatform>>();
            
            SpawnPlatforms();
        }

        private void OnEnable()
        {
            Platform.PlatformBeaten += IncreaseBeatenPlatformsCounter;
            Platform.PlatformSetInactive += SpawnNextPlatform;
        }
        
        private void OnDisable()
        {
            Platform.PlatformBeaten -= IncreaseBeatenPlatformsCounter;
            Platform.PlatformSetInactive -= SpawnNextPlatform;
        }


        public void SpawnNextPlatform()
        {
            int platformTypeRandomValue = Random.Range(0, 2); //0 - simple platform, 1 - enemy platform

            Vector2 spawnPoint = nextSpawnPoint;
            
            if (platformTypeRandomValue == 0)
            {
                SpawnNextSimplePlatform(spawnPoint);
            }
            else
            {
                SpawnNextEnemyPlatform(spawnPoint);
            }
            

        }
        
        public void SpawnNextSimplePlatform(Vector2 spawnPoint)
        {
                        
            GameObject newPlatform = simplePlatformPoolManager.Create();
            newPlatform.transform.position = spawnPoint;
            
            nextSpawnPoint.y += distanceBetweenPlatforms;


        }
        
        public void SpawnNextEnemyPlatform(Vector2 spawnPoint)
        {
            GameObject newPlatform = enemyPlatformPoolManager.Create();
            newPlatform.transform.position = spawnPoint;

            nextSpawnPoint.y += distanceBetweenPlatforms;

        }


        private void IncreaseBeatenPlatformsCounter()
        {
            PlatformsBeatenCount++;
        }

        
        private void SpawnPlatforms()
        {
            Vector2 spawnPoint = startSpawnPoint;

            for (int i = 0; i < safePlatformsCount; i++)
            {
                SpawnNextSimplePlatform(spawnPoint);
                spawnPoint = nextSpawnPoint;
            }

            int restPlatforms = startSpawningCount - safePlatformsCount;
            for (int i = 0; i < restPlatforms; i++)
            {
                SpawnNextPlatform();

            }
            
        }
        
        
    }
}
