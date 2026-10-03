using UnityEngine;
using SuikaClone.Fruits;
using SuikaClone.Factories;

namespace SuikaClone.Pools
{
    public class BaseFruitPool : MonoBehaviour, IPool<BaseFruit>
    {
        [SerializeField] private int initialCount = 10;
        [SerializeField] private BaseFruitFactory _fruitFactory;

        private ObjectPooling<BaseFruit> _pool;

        public void Initialize()
        {
            // Initialize the pool with the specified initial count
        }

        private void Awake()
        {
            _pool = new ObjectPooling<BaseFruit>(_fruitFactory, initialCount);
        }

        public BaseFruit Get()
        {
            // return _pool.GetObject();
            return _pool.Get();
        }

        public void Return(BaseFruit fruit)
        {
            _pool.Return(fruit);
        }
    }
}