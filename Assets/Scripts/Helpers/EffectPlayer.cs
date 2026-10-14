using UnityEngine;

public class EffectPlayer : MonoBehaviour
{
    [SerializeField] private ParticleSystem effectsSystem;

    public void PlayEffect()
    {
        effectsSystem.Play();
    }
}
