using SuikaClone.Fruits;
using UnityEngine;

namespace SuikaClone.Factories
{
    public class BaseFruitFactory : MonoBehaviour, IFactory<BaseFruit>
    {
        [SerializeField] private BaseFruit _fruitPrefab;
        [SerializeField] private Transform _parent;

        private Factory<BaseFruit> _factory;

        private void Awake()
        {
            _factory = new Factory<BaseFruit>(_fruitPrefab, _parent);
        }

        public BaseFruit Create()
        {
            return _factory.Create();
        }
    }
}
