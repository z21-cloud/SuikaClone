using SuikaClone.Factories;
using SuikaClone.Fruits;
using UnityEngine;

namespace SuikaClone.Pools
{
    public class BubbleFruitPool : MonoBehaviour, IPool<Fruit>
    {
        [SerializeField] private int initialCount = 10;

        private ObjectPooling<Fruit> _pool;

        public void Initialize(BubbleFruitFactory bubbleFruitFactory)
        {
            // Initialize the pool with the specified initial count
            _pool = new ObjectPooling<Fruit>(bubbleFruitFactory, initialCount);
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