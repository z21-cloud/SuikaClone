using SuikaClone.Factories;
using SuikaClone.Pools;
using UnityEngine;

namespace SuikaClone.Core
{
    public class GameBootstrapper : MonoBehaviour
    {
        [Header("Factories")]
        [SerializeField] private BaseFruitFactory baseFruitFactory;
        
        [Header("Pools")]
        [SerializeField] private BaseFruitPool baseFruitPool;

        private void Awake()
        {
            // Factory initialization
            baseFruitFactory.Initialize();

            // Pool initialization
            baseFruitPool.Initialize(baseFruitFactory);
        }
    }
}
