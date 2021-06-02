using UnityEngine;

namespace ForestValley.Assistants
{
    public interface IPool
    {
        public GameObject Next { get; set; }
        void InitPoolItem();
    }
}
