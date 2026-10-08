using SuikaClone.Fruits;
using SuikaClone.Pools;
using UnityEngine;

namespace SuikaClone.Managers
{
    public class MergeManager : MonoBehaviour, IMergeHandler
    {
        // Singleton instance for quick access to the MergeManager from other scripts ; Temp decision, can be changed later if needed
        private FruitSpawnManager fruitSpawnManager;
        private ScoreManager scoreManager;

        public void Initialize(FruitSpawnManager fruitSpawnManager, ScoreManager scoreManager)
        {
            // Initialization logic if needed
            this.fruitSpawnManager = fruitSpawnManager;
            this.scoreManager = scoreManager;
        }

        public void MergeFruits(Fruit fruit1, Fruit fruit2)
        {
            int newFruitScore = fruit1.FruitData.FruitScore + fruit2.FruitData.FruitScore;

            scoreManager.AddScore(newFruitScore);

            SelectFruitType(fruit1, fruit2);
        }

        public void SelectFruitType(Fruit fruit1, Fruit fruit2)
        {
            // Return the original fruits to their respective pools
            fruitSpawnManager.ReturnFruit(fruit1);
            fruitSpawnManager.ReturnFruit(fruit2);

            // Handle fruit selection logic here
            Debug.Log($"[MergeManager] Merging Fruit: {fruit1.name}");

            int newFruitLevel = fruit1.FruitData.FruitLevel + 1;
            Vector3 mergePosition = (fruit1.transform.position + fruit2.transform.position) / 2;

            fruitSpawnManager.SpawnFruit(newFruitLevel, mergePosition);
        }
    }
}
