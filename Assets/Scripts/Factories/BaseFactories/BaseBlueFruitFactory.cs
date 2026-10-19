using SuikaClone.Fruits;
using SuikaClone.Managers;
using UnityEngine;

namespace SuikaClone.Factories
{
    public class BaseBlueFruitFactory : MonoBehaviour, IFactory<Fruit>
    {
        [SerializeField] private Fruit _fruitPrefab;
        [SerializeField] private Transform _parent;

        private Factory<Fruit> _factory;
        private IMergeHandler _mergeHandler;
        
        public void Initialize(IMergeHandler mergeHandler)
        {
            _factory = new Factory<Fruit>(_fruitPrefab, _parent);
            _mergeHandler = mergeHandler;
        }

        public Fruit Create()
        {
            var obj = _factory.Create();
            obj.Initialize(_mergeHandler);
            return obj;
        }
    }
}
