using SuikaClone.Fruits;
using UnityEngine;

namespace SuikaClone.Managers
{
    public class MergeManager : MonoBehaviour, IMergeHandler
    {
        private FruitSpawnManager _fruitSpawnManager;
        private EffectManager _effectManager;
        private ScoreManager _scoreManager;

        public void Initialize(FruitSpawnManager fruitSpawnManager, ScoreManager scoreManager, EffectManager effectManager)
        {
            _fruitSpawnManager = fruitSpawnManager;
            _scoreManager = scoreManager;
            _effectManager = effectManager;
        }

        public void MergeFruits(Fruit fruit1, Fruit fruit2)
        {
            if (fruit1.IsMerging || fruit2.IsMerging)
            {
                return;
            }

            fruit1.IsMerging = true;
            fruit2.IsMerging = true;

            int newFruitScore = fruit1.FruitData.FruitScore + fruit2.FruitData.FruitScore;

            Vector3 mergePosition = (fruit1.transform.position + fruit2.transform.position) / 2;
            _effectManager.PlayEffect(mergePosition);
            _scoreManager.AddScore(newFruitScore);

            SelectFruitType(fruit1, fruit2);
        }

        public void SelectFruitType(Fruit fruit1, Fruit fruit2)
        {
            // Compute the new fruit data before returning the originals to pools
            int newFruitLevel = fruit1.FruitData.FruitLevel + 1;
            Vector3 mergePosition = (fruit1.transform.position + fruit2.transform.position) / 2;

            // Return the original fruits to their respective pools
            _fruitSpawnManager.ReturnFruit(fruit1);
            // Debug.Log($"[MergeManager] Returning Fruit: {fruit1.name}");
                        
            _fruitSpawnManager.ReturnFruit(fruit2);
            // Debug.Log($"[MergeManager] Returning Fruit: {fruit2.name}");

            // Handle fruit selection logic here
            Debug.Log($"[MergeManager] Merging Fruit: {fruit1.name} + {fruit2.name}");

            Fruit fruit = _fruitSpawnManager.SpawnFruit(newFruitLevel, mergePosition);

            if(fruit == null || !fruit.TryGetComponent<Rigidbody2D>(out Rigidbody2D fruitRb))
            {
                Debug.LogError("[MergeManager] Spawned fruit is null or missing Rigidbody2D! Check the FruitSpawnManager and ensure that the fruit pools are properly initialized.");
                return;
            }

            Debug.Log($"[MergeManager] Succesfully spawned new fruit: {fruit.name} at position {mergePosition}. Adding force for visual effect.");

            Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized; // Random direction
            fruitRb.AddForce(randomDirection * 7.5f, ForceMode2D.Impulse); // Add force for visual effect
        }
    }
}
