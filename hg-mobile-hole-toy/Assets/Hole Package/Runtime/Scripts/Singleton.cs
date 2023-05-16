using UnityEngine;

namespace Toy.Hole
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance
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
