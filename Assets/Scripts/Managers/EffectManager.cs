using System.Collections;
using SuikaClone.Factories;
using SuikaClone.Pools;
using UnityEngine;

namespace SuikaClone.Managers
{
    public class EffectManager : MonoBehaviour
    {
        private EffectsFactory _effectsFactory;
        private EffectsPool _effectsPool;
        public void Initialize(EffectsFactory effectsFactory, EffectsPool effectsPool)
        {
            _effectsFactory = effectsFactory;
            _effectsPool = effectsPool;

            _effectsFactory.Initialize();
            _effectsPool.Initialize(_effectsFactory);
        }

        public void PlayEffect(Vector3 position)
        {
            var effect = _effectsPool.Get();

            if(effect == null)
            {
                Debug.LogWarning($"[EffectManager] Effect pool is empty! Effect wouldn't display");
                return;
            }

            effect.transform.position = position;
            StartCoroutine(EffectCoroutine(effect));
        }

        private IEnumerator EffectCoroutine(EffectPlayer effect)
        {
            effect.gameObject.SetActive(true);
            effect.PlayEffect();
            yield return new WaitForSeconds(2f);
            _effectsPool.Return(effect);
        }

        public void Retun(EffectPlayer effectPlayer)
        {
            _effectsPool.Return(effectPlayer);
        }
    }
}
