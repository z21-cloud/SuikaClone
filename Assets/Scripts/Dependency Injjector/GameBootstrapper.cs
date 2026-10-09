using SuikaClone.Factories;
using SuikaClone.Managers;
using SuikaClone.Pools;
using UnityEngine;

namespace SuikaClone.Core
{
    public class GameBootstrapper : MonoBehaviour
    {
        
        [Header("Factories")]
        [SerializeField] private BaseFruitFactory baseFruitFactory;
        [SerializeField] private BubbleFruitFactory bubbleFruitFactory;
        [SerializeField] private SquareFruitFactory squareFruitFactory;

        [Header("Pools")]
        [SerializeField] private BaseFruitPool baseFruitPool;
        [SerializeField] private BubbleFruitPool bubbleFruitPool;
        [SerializeField] private SquareFruitPool squareFruitPool;

        [Header("Managers")]
        [SerializeField] private MergeManager mergeManager;
        [SerializeField] private FruitSpawnManager fruitSpawnManager;
        [SerializeField] private ScoreManager scoreManager;

        private void Awake()
        {
            // Factory initialization
            baseFruitFactory.Initialize(mergeManager);
            bubbleFruitFactory.Initialize(mergeManager);
            squareFruitFactory.Initalize(mergeManager);

            // Pool initialization
            baseFruitPool.Initialize(baseFruitFactory);
            bubbleFruitPool.Initialize(bubbleFruitFactory);
            squareFruitPool.Initialize(squareFruitFactory);

            mergeManager.Initialize(fruitSpawnManager, scoreManager);
        }
    }
}
