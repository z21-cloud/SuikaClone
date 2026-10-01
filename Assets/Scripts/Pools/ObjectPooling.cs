using System.Collections.Generic;
using UnityEngine;

namespace SuikaClone.Pools
{
    public class ObjectPooling<T> where T : MonoBehaviour
    {
        private readonly T _prefab;
        private readonly Transform _parent;
        private readonly List<T> _objects;

        public ObjectPooling(T prefab, int initialCount = 10, Transform parent = null)
        {
            _prefab = prefab;

            _objects = new List<T>(initialCount);

            for (int i = 0; i < initialCount; i++)
            {
                CreateObject();
            }
        }

        private void CreateObject()
        {
            var obj = GameObject.Instantiate(_prefab, _parent);
            obj.gameObject.SetActive(false);
            _objects.Add(obj);
        }

        public T Get()
        {
            foreach (T obj in _objects)
            {
                if (!obj.gameObject.activeInHierarchy)
                {
                    obj.gameObject.SetActive(true);
                    return obj;
                }
            }

            Debug.LogError("[ObjectPooling] Pool is empty!");
            return null;
        }

        public void Return(T value)
        {
            value.gameObject.SetActive(false);
        }
    }
}
