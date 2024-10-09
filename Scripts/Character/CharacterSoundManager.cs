using UnityEngine;

public class CharacterSoundManager : MonoBehaviour
{
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _ambientSource;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _fly;
    [SerializeField] private AudioClip _ambientSound;
    [SerializeField] private AudioClip _boneCrack;
    [SerializeField] private AudioClip _fallSound;
    [SerializeField] private AudioClip _spiderDeathSound;

    private void Start()
    {
        PlayFlySound();
    }
    private void PlayFlySound()
    {
        _soundManager.PlayLooped(_musicSource, _fly);
    }
    private void StopPlayFly()
    {
        _musicSource.Stop();
    }
    private void PlayAmbient()
    {
        _soundManager.PlayLooped(_ambientSource, _ambientSound);
    }
    private void StopAmbient()
    {
        _ambientSource.Stop();
    }
    public void PlayLooseBodyPart(TrapType trapType)
    {
        switch(trapType)
        {
            case TrapType.block: 
            {
                _soundManager.PlayOnce(_audioSource, _boneCrack);
                break;        
            }
            case TrapType.spider:
            {
                 _soundManager.PlayOnce(_audioSource, _spiderDeathSound);
                break;
            }
        }
    }
    public void PlayDeathSound(TrapType trapType)
    {
        switch(trapType) {
            case TrapType.block:
            {
                _soundManager.PlayOnce(_audioSource, _fallSound);
                break;
            }
            case TrapType.spider:
            {
                _soundManager.PlayOnce(_audioSource, _fallSound);
                break;
            }
        }
    }


    private void OnEnable()
    {
        GameManager.OnGameReset += PlayFlySound;
        GameManager.OnGameReset += PlayAmbient;
        Character.OnDeath += StopPlayFly;
        Character.OnDeath += StopAmbient;
    }

    private void OnDisable()
    {
        GameManager.OnGameReset -= PlayFlySound;
        GameManager.OnGameReset -= PlayAmbient;
        Character.OnDeath -= StopPlayFly;
        Character.OnDeath -= StopAmbient;
    }
}
