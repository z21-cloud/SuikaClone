using UnityEngine;

namespace SuikaClone.Factories
{
    public class Factory<T>: IFactory<T> where T : MonoBehaviour
    {
        private readonly T _prefab;
        private readonly Transform _parent;

        public Factory(T prefab, Transform parent = null)
        {
            _prefab = prefab;
            _parent = parent;
        }

        public T Create()
        {
            var obj = Object.Instantiate(_prefab, _parent);
            return obj;
        }
    }
}