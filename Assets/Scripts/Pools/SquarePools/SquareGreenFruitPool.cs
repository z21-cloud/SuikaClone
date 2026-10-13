using SuikaClone.Factories;
using SuikaClone.Fruits;
using UnityEngine;

namespace SuikaClone.Pools
{
    public class SquareGreenFruitPool : MonoBehaviour, IPool<Fruit>
    {
        [SerializeField] private int initialCount = 10;

        private ObjectPooling<Fruit> _pool;

        public void Initialize(IFactory<Fruit> squareGreenFruitFactory)
        {
            _pool = new ObjectPooling<Fruit>(squareGreenFruitFactory, initialCount);
        }

        public Fruit Get()
        {
            return _pool.Get();
        }

        public void Return(Fruit fruit)
        {
            _pool.Return(fruit);
        }
    }
}
