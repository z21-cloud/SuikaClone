using UnityEngine;

namespace SuikaClone.Pools
{
    public interface IPool<T> where T : MonoBehaviour
    {
        public T Get();
        public void Return(T obj);
    }
}