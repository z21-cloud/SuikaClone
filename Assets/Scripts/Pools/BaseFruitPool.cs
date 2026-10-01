using UnityEngine;

namespace SuikaClone.Pools
{
    public class BaseFruitPool : MonoBehaviour
    {
        [SerializeField] private int initialCount = 10;
        [SerializeField] private Transform parent;

        // private ObjectPooling<Fruit> _pool;

        public void Initialize()
        {
            // _pool = new ObjectPooling<Fruit>(initialCount, parent);
        }

        private void Awake()
        {
            Initialize();
        }

        /*public Fruit GetFruit()
        {
            // return _pool.GetObject();
            return null;
        }*/

        /*public void ReturnFruit(Fruit fruit)
        {
            // _pool.ReturnObject(fruit);
        }*/
    }
}