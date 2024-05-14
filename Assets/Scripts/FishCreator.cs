using UnityEngine;

namespace DefaultNamespace
{
    public class FishCreator : MonoBehaviour
    {
        public static FishCreator Instance { get; private set; }
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public Fish CreateFish(Fish fishOne, Fish fishTwo)
        {
            Debug.Log("Should create fish");
            return null;
        }
        
    }
}