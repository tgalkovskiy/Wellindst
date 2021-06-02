using UnityEngine;

namespace ForestValley.Environment.Platforms
{
    public class EnemyPlatform : Platform
    {
        [SerializeField] private GameObject enemy;
        [SerializeField] private float leftSpawnBound = -2.8f;
        [SerializeField] private float rightSpawnBound = 2.8f;

        private void OnEnable()
        {
            
            RandomizeEnemyPos();
        }

        private void RandomizeEnemyPos()
        {
            if (enemy == null)
            {
                return;
            }
            enemy.transform.position = new Vector3(Random.Range(leftSpawnBound, rightSpawnBound), enemy.transform.position.y);
        }
    }
}