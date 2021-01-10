using UnityEngine;

public class DustparticleSystem : MonoBehaviour
{
    private ParticleSystem dust;

    private void Start()
    {
        dust = GetComponent<ParticleSystem>();
    }

    public void PlayDust()
    {
        if (dust == null)
            return;
        dust.Play();
    }
}
