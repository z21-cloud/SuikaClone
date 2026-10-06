using SuikaClone.Fruits;
using UnityEngine;

namespace SuikaClone.Managers
{
    public class MergeManager : MonoBehaviour
    {
        // Singleton instance for quick access to the MergeManager from other scripts ; Temp decision, can be changed later if needed
        public static MergeManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void MergeFruits(Fruit fruit1, Fruit fruit2)
        {

        }
    }
}
