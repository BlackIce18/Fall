using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum HumanPart
{
    head,
    body,
    leftArm,
    rightArm,
    leftLeg,
    rightLeg
}
public class CharacterDeadBody : MonoBehaviour
{
    [SerializeField] private DeadBodyParts _head;
    [SerializeField] private DeadBodyParts _body;
    [SerializeField] private DeadBodyParts _leftArm;
    [SerializeField] private DeadBodyParts _rightArm;
    [SerializeField] private DeadBodyParts _leftLeg;
    [SerializeField] private DeadBodyParts _rightLeg;
    [Tooltip("For synchronize speed of body part and level. (body part stay at same position)")]
    [SerializeField] private MoveUp _levelSpeed;

    private void Start()
    {
        Reset();
    }

    public void ShowDeadPart(HumanPart part)
    {
        DeadBodyParts deadBodyPart = GetDeathBodyPart(part);

        if(deadBodyPart == null) return;

        deadBodyPart.gameObject.SetActive(true);
        ChangeBodyPartSpeed(deadBodyPart, _levelSpeed.speed);
        deadBodyPart.Animator.speed = 0;
    }

    private DeadBodyParts GetDeathBodyPart(HumanPart part)
    {
        switch (part)
        {
            case HumanPart.head:
                return _head;
            case HumanPart.body:
                return _body;
            case HumanPart.leftArm:
                return _leftArm;
            case HumanPart.rightArm:
                return _rightArm;
            case HumanPart.leftLeg:
                return _leftLeg;
            case HumanPart.rightLeg:
                return _rightLeg;
            default: break;
        }
        return null;
    }

    private void ChangeBodyPartSpeed(DeadBodyParts deadBodyPart, float speed)
    {
        deadBodyPart.MoveUp.speed = speed;
    }

    private void Reset()
    {
        ResetPart(_head);
        ResetPart(_body);
        ResetPart(_leftArm);
        ResetPart(_rightArm);
        ResetPart(_leftLeg);
        ResetPart(_rightLeg);
    }

    private void ResetPart(DeadBodyParts deadBodyPart)
    {
        deadBodyPart.gameObject.SetActive(false);
        deadBodyPart.transform.position = Vector3.zero;
        ChangeBodyPartSpeed(deadBodyPart, 0);
        deadBodyPart.Animator.speed = 1;
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
