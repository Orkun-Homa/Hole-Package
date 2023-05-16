using UnityEngine;

namespace Internal.Core
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T THIS
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                }
                return instance;
            }
            set
            {
                
            }
        }
        private static T instance;
    }
}
