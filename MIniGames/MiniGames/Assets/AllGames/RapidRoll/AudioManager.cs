using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RapidRoll
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        public AudioSource _audio;

        private void Start()
        {
            Instance = this;
        }
        public void PlayAudio()
        {
            _audio.Play();
        }
        public void StopAudio()
        {
            _audio.Stop();
        }

    }
}