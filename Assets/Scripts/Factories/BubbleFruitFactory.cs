using SuikaClone.Fruits;
using UnityEngine;

namespace SuikaClone.Factories
{
    public class BubbleFruitFactory : MonoBehaviour, IFactory<Fruit>
    {
        [SerializeField] private Fruit _fruitPrefab;
        [SerializeField] private Transform _parent;

        private Factory<Fruit> _factory;

        public void Initialize()
        {
            _factory = new Factory<Fruit>(_fruitPrefab, _parent);
        }

        public Fruit Create()
        {
            return _factory.Create();
        }
    }
}
