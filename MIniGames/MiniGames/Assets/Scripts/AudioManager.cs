using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager _instance = null;

    private AudioSource audioSource;

    public static Audios audios;

    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<AudioManager>();
                if (_instance == null)
                {
                    GameObject g = new GameObject("AudioManager");
                    _instance = g.AddComponent<AudioManager>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        audioSource = GetComponent<AudioSource>();
        audios = Resources.Load<Audios>("Audios01");
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlaySFX(AudioClip _audioClip)
    {
        if (audioSource != null && _audioClip != null)
            audioSource.PlayOneShot(_audioClip);
    }
}
