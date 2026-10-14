using UnityEngine;

namespace SuikaClone.Factories
{
    public class EffectsFactory : MonoBehaviour, IFactory<EffectPlayer>
    {
        [SerializeField] private EffectPlayer _effectPlayer;
        [SerializeField] private Transform _parent;

        private Factory<EffectPlayer> _factory;

        public void Initialize()
        {
            _factory = new Factory<EffectPlayer>(_effectPlayer, _parent);
        }

        public EffectPlayer Create()
        {
            var obj = _factory.Create();
            return obj;
        }
    }
}
