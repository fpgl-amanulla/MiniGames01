using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ButtonClickSound : MonoBehaviour
{
    private Button btn;
    private AudioSource audioSource;

    [Header("Audio Cilp")]
    [SerializeField] AudioClip audioClip;

    private void Awake()
    {
        btn = GetComponent<Button>();
        audioSource = GetComponent<AudioSource>();

        if (audioClip == null) audioClip = Resources.Load<AudioClip>("btn-click");

        btn.onClick.AddListener(() => ClickCallBack());
    }

    private void ClickCallBack()
    {
        audioSource.PlayOneShot(audioClip);
    }
}
