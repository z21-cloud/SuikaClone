using UnityEngine;
using SuikaClone.Managers;

namespace SuikaClone.Fruits
{
    public class Fruit : MonoBehaviour
    {
        [SerializeField] private FruitData fruitData;

        private IMergeHandler mergeHandler;
        private bool isMerging = false;

        public bool IsMerging
        {
            get => isMerging;
            set => isMerging = value;
        }
        
        public FruitData FruitData => fruitData;

        public void Initialize(IMergeHandler mergeHandler)
        {
            this.mergeHandler = mergeHandler;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.TryGetComponent<Fruit>(out var otherFruit))
            {
                if (otherFruit.FruitData.FruitLevel == FruitData.FruitLevel)
                {
                    if(otherFruit.GetInstanceID() > this.GetInstanceID())
                    {
                        mergeHandler.MergeFruits(this, otherFruit);
                    }
                }
            }
        }
    }
}
