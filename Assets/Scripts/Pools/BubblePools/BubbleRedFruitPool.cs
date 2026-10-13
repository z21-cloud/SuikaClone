using SuikaClone.Factories;
using SuikaClone.Fruits;
using UnityEngine;

namespace SuikaClone.Pools
{
    public class BubbleRedFruitPool : MonoBehaviour, IPool<Fruit>
    {
        [SerializeField] private int initialCount = 10;

        private ObjectPooling<Fruit> _pool;

        public void Initialize(IFactory<Fruit> bubbleRedFruitFactory)
        {
            _pool = new ObjectPooling<Fruit>(bubbleRedFruitFactory, initialCount);
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
