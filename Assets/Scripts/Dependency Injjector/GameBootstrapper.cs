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
        [SerializeField] private PlayerController playerController;

        private void Awake()
        {
            fruitSpawnManager.Initialize(baseFruitFactory, bubbleFruitFactory, squareFruitFactory, mergeManager);
            playerController.Initialize(fruitSpawnManager);

            mergeManager.Initialize(fruitSpawnManager, scoreManager);
        }
    }
}
