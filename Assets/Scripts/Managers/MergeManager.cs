using SuikaClone.Fruits;
using SuikaClone.Pools;
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
            int newFruitScore = fruit1.FruitData.FruitScore + fruit2.FruitData.FruitScore;

            SelectFruitType(fruit1, fruit2);
        }

        public void SelectFruitType(Fruit fruit1, Fruit fruit2)
        {
            // Handle fruit selection logic here
            Debug.Log($"[MergeManager] Merging Fruit: {fruit1.name}");

            int newFruitLevel = fruit1.FruitData.FruitLevel + 1;
            Vector3 mergePosition = (fruit1.transform.position + fruit2.transform.position) / 2;

            FruitSpawnManager.Instance.SpawnFruit(newFruitLevel, mergePosition);

            // Return the original fruits to their respective pools
            FruitSpawnManager.Instance.ReturnFruit(fruit1);
            FruitSpawnManager.Instance.ReturnFruit(fruit2);
        }
    }
}
