using System;
using UnityEditor;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterParts _head;
    [SerializeField] private CharacterParts _body;
    [SerializeField] private CharacterParts _leftArm;
    [SerializeField] private CharacterParts _rightArm;
    [SerializeField] private CharacterParts _leftLeg;
    [SerializeField] private CharacterParts _rightLeg;
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterDeadBody _characterDeathBody;
    [SerializeField] private CharacterSoundManager _characterSoundManager;
    [SerializeField] private CharacterMove _characterMove;

    public static event Action OnDeath;
    private bool _isDead = false;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Trap") && _isDead == false)
        {
            Trap trap = collision.gameObject.GetComponent<Trap>();
            _characterDeathBody.transform.position = transform.position;

            foreach (ContactPoint contact in collision.contacts)
            {
                Collider contactedObject = contact.thisCollider;

                if ((contactedObject == _head.Collider) || (contactedObject == _body.Collider))
                {
                    OnDeath?.Invoke();
                    _animator.SetTrigger("IsDeath");
                    _isDead = true;
                    DisableParticleSystems();
                    _characterSoundManager.PlayDeathSound(trap.Type);
                    break;
                }
                else if (contactedObject == _leftArm.Collider)
                {
                    PartCollideWithTrap(trap, _characterDeathBody, _leftArm);
                    _characterMove.DownHorizontalSpeed();
                }
                else if(contactedObject == _rightArm.Collider)
                {
                    PartCollideWithTrap(trap, _characterDeathBody, _rightArm);
                    _characterMove.DownHorizontalSpeed();
                }
                else if(contactedObject == _leftLeg.Collider)
                {
                    PartCollideWithTrap(trap, _characterDeathBody, _leftLeg);
                    _characterMove.DownVerticalSpeed();
                }
                else if(contactedObject == _rightLeg.Collider)
                {
                    PartCollideWithTrap(trap, _characterDeathBody, _rightLeg);
                    _characterMove.DownVerticalSpeed();
                }
                contactedObject.enabled = false;
            }
        }
    }

    private void PartCollideWithTrap(Trap trap, CharacterDeadBody characterDeathBody, CharacterParts characterPart)
    {
        _characterSoundManager.PlayLooseBodyPart(trap.Type);
        characterDeathBody.ShowDeadPart(characterPart.Part);
        characterPart.gameObject.SetActive(false);
        characterPart.ParticleSystem.gameObject.SetActive(true);
    }

    private void Reset()
    {
        transform.position = Vector3.zero;
        _animator.ResetTrigger("IsDeath");
        _isDead = false;

        ShowVisualParts();
        DisableParticleSystems();
        ResetColliders();
    }
    private void ResetColliders()
    {
        _leftArm.Collider.enabled = true;
        _rightArm.Collider.enabled = true;
        _rightLeg.Collider.enabled = true;
        _leftLeg.Collider.enabled = true;
        _head.Collider.enabled = true;
        _body.Collider.enabled = true;
    }
    private void ShowVisualParts()
    {
        _leftArm.VisualPart.SetActive(true);
        _rightArm.VisualPart.SetActive(true);
        _rightLeg.VisualPart.SetActive(true);
        _leftLeg.VisualPart.SetActive(true);
        _head.VisualPart.SetActive(true);
        _body.VisualPart.SetActive(true);
    }
    private void DisableParticleSystems()
    {
        _leftArm.ParticleSystem.gameObject.SetActive(false);
        _rightArm.ParticleSystem.gameObject.SetActive(false);
        _rightLeg.ParticleSystem.gameObject.SetActive(false);
        _leftLeg.ParticleSystem.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        GameManager.OnGameReset += Reset;
    }

    private void OnDisable()
    {
        GameManager.OnGameReset -= Reset;
    }
}
