using UnityEngine;

public class VFXEffect : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem[] _particleSystems;

    [SerializeField]
    private bool _playOnAwake;

    private void Awake()
    {
        if (_playOnAwake)
            PlayEffect();
    }

    public void PlayEffect()
    {
        foreach (ParticleSystem ps in _particleSystems)
        {
            ps.Play();
        }
    }

    public void StopEffect()
    {
        foreach (ParticleSystem ps in _particleSystems)
        {
            ps.Stop();
        }
    }
}
