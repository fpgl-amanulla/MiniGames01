using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confetti_FX : MonoBehaviour
{
    private ParticleSystem confetti;

    private void Start()
    {
        confetti = GetComponent<ParticleSystem>();
        confetti.Stop();
    }

    public void PlayConfetti()
    {
        confetti.Play();
    }
}
