using UnityEngine;

namespace ForestValley.Assistants
{
    public class Singleton<T> : MonoBehaviour
        where T : MonoBehaviour
    {
        #region Vars

        private T instance;


        public T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();

                    if (instance == null)
                    {
                        var newInstance = new GameObject("Unnamed singleton", typeof(T));
                    }
                }

                return instance;
            }
        }

        #endregion
    }
}
