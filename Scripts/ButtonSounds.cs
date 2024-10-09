using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    [SerializeField] private AudioClip _clickSound; 
    [SerializeField] private AudioClip _hoverSound;
    [SerializeField] private AudioSource _audioSource;

    public void PlayClickSound()
    {
        _audioSource.PlayOneShot(_clickSound);
    }

    public void PlayHoverSound() 
    {
        _audioSource.PlayOneShot(_hoverSound);
    }
}
