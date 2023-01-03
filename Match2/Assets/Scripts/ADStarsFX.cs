using UnityEngine;

public class ADStarsFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    public void PlayParticle()
    {
        _particleSystem.Play();
    }
}
