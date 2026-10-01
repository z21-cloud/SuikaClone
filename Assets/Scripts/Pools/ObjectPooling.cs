using System.Collections.Generic;
using UnityEngine;
using SuikaClone.Factories;

namespace SuikaClone.Pools
{
    public class ObjectPooling<T> where T : MonoBehaviour
    {
        private readonly List<T> _objects;
        private readonly IFactory<T> _factory;

        public ObjectPooling(IFactory<T> factory, int initialCount = 10)
        {
            _objects = new List<T>(initialCount);
            
            _factory = factory;

            for (int i = 0; i < initialCount; i++)
            {
                CreateObject();
            }
        }

        private void CreateObject()
        {
            var obj = _factory.Create();
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
