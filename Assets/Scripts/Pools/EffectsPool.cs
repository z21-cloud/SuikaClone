using SuikaClone.Factories;
using UnityEngine;

namespace SuikaClone.Pools
{
    public class EffectsPool : MonoBehaviour, IPool<EffectPlayer>
    {
        [SerializeField] private int initialCount = 10;

        private ObjectPooling<EffectPlayer> _pool;

        public void Initialize(IFactory<EffectPlayer> effectsFactory)
        {
            _pool = new ObjectPooling<EffectPlayer>(effectsFactory, initialCount);
        }

        public EffectPlayer Get()
        {
            return _pool.Get();
        }

        public void Return(EffectPlayer fruit)
        {
            _pool.Return(fruit);
        }
    }
}
