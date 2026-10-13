using SuikaClone.Factories;
using SuikaClone.Fruits;
using UnityEngine;

namespace SuikaClone.Pools
{
    public class SquareFruitPool : MonoBehaviour, IPool<Fruit>
    {
        [SerializeField] private int initialCount = 10;

        private ObjectPooling<Fruit> _pool;

        public void Initialize(IFactory<Fruit> squareFruitFactory)
        {
            // Initialize the pool with the specified initial count
            _pool = new ObjectPooling<Fruit>(squareFruitFactory, initialCount);
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
