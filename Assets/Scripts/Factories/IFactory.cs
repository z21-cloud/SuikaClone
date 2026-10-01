using UnityEngine;

namespace SuikaClone.Factories
{
    public interface IFactory<T> where T : MonoBehaviour
    {
        T Create();
    }
}
