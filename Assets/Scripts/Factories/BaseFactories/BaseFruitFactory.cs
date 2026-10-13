using SuikaClone.Fruits;
using SuikaClone.Managers;
using UnityEngine;

namespace SuikaClone.Factories
{
    public class BaseFruitFactory : MonoBehaviour, IFactory<Fruit>
    {
        [SerializeField] private Fruit _fruitPrefab;
        [SerializeField] private Transform _parent;

        private Factory<Fruit> _factory;
        private IMergeHandler mergeHandler;
        
        public void Initialize(IMergeHandler mergeHandler)
        {
            _factory = new Factory<Fruit>(_fruitPrefab, _parent);
            this.mergeHandler = mergeHandler;
        }

        public Fruit Create()
        {
            var obj = _factory.Create();
            obj.Initialize(mergeHandler);
            return obj;
        }
    }
}
