using System;
using ForestValley.Assistants;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ForestValley.Managers
{
    public class PoolManager<T>: MonoBehaviour, IManager
        where T : IPool
    {
        [SerializeField] private int poolSize = 10;
        [SerializeField] private GameObject[] poolObjectPrefab;
        
        protected GameObject[] pool;
        private GameObject firstAvailable;


        private void Update()
        {
            ReturningProcess();
        }

        public virtual void Init()
        {
            GameObject poolFolder = new GameObject(typeof(T).Name + " Pool");
            
            pool = new GameObject[poolSize];
            for (int i = 0; i < poolSize; i++)
            {
                int objectIndex = i % poolObjectPrefab.Length;
                pool[i] = Instantiate(poolObjectPrefab[objectIndex]);
                pool[i].SetActive(false);
                
                pool[i].transform.SetParent(poolFolder.transform);
            }

            firstAvailable = pool[0];

            for (int i = 0; i < poolSize-1; i++)
            {
                pool[i].GetComponent<T>().Next = pool[i + 1];
            }

            pool[poolSize - 1].GetComponent<T>().Next = null;
        }

        public GameObject Create()
        {
            if (firstAvailable == null)
            {
                Debug.LogError("Pool is full");
            }

            GameObject newGameObject = firstAvailable;
            
            newGameObject.GetComponent<T>().InitPoolItem();
            newGameObject.SetActive(true);
            
            firstAvailable = newGameObject.GetComponent<T>().Next;
            

            return newGameObject;
        }

        private void ReturningProcess()
        {
            for (int i = 0; i < poolSize; i++)
            {
                if (!pool[i].activeSelf)
                {
                    pool[i].GetComponent<T>().Next = firstAvailable;
                    firstAvailable = pool[i];

                }
                
            }
        }
        
    }
}
