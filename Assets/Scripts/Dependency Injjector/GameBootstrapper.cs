using SuikaClone.Factories;
using SuikaClone.Pools;
using UnityEngine;

namespace SuikaClone.Core
{
    public class GameBootstrapper : MonoBehaviour
    {
        [Header("Factories")]
        [SerializeField] private BaseFruitFactory baseFruitFactory;
        [SerializeField] private BubbleFruitFactory bubbleFruitFactory;
        
        [Header("Pools")]
        [SerializeField] private BaseFruitPool baseFruitPool;
        [SerializeField] private BubbleFruitPool bubbleFruitPool;

        private void Awake()
        {
            // Factory initialization
            baseFruitFactory.Initialize();
            bubbleFruitFactory.Initialize();

            // Pool initialization
            baseFruitPool.Initialize(baseFruitFactory);
            bubbleFruitPool.Initialize(bubbleFruitFactory);
        }
    }
}
