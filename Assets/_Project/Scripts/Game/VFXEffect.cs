using UnityEngine;

public class VFXEffect : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem[] _particleSystems;

    public void PlayEffect()
    {
        foreach (ParticleSystem ps in _particleSystems)
        {
            ps.Play();
        }
    }
}
