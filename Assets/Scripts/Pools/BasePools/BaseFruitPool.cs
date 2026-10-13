using UnityEngine;
using SuikaClone.Fruits;
using SuikaClone.Factories;

namespace SuikaClone.Pools
{
    public class BaseFruitPool : MonoBehaviour, IPool<Fruit>
    {
        [SerializeField] private int initialCount = 10;

        private ObjectPooling<Fruit> _pool;

        public void Initialize(IFactory<Fruit> baseFruitFactory)
        {
            // Initialize the pool with the specified initial count
            _pool = new ObjectPooling<Fruit>(baseFruitFactory, initialCount);
        }

        public Fruit Get()
        {
            // return _pool.GetObject();
            return _pool.Get();
        }

        public void Return(Fruit fruit)
        {
            _pool.Return(fruit);
        }
    }
}