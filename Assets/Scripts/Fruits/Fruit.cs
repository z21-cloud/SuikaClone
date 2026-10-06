using UnityEngine;
using SuikaClone.Managers;

namespace SuikaClone.Fruits
{
    public class Fruit : MonoBehaviour
    {
        [SerializeField] private FruitData fruitData;

        public FruitData FruitData => fruitData;

        public void Initialize()
        {
            // Add FruitMergeManager component to the fruit GameObject
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.TryGetComponent<Fruit>(out var otherFruit))
            {
                if (otherFruit.FruitData.FruitLevel == FruitData.FruitLevel)
                {
                    if(otherFruit.GetInstanceID() > this.GetInstanceID())
                    {
                        MergeManager.Instance.MergeFruits(this, otherFruit);
                    }
                }
            }
        }
    }
}
