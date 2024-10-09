using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBodyParts : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private MoveUp _moveUp;
    [SerializeField] private HumanPart part;
    public MoveUp MoveUp { get { return _moveUp; } }
    public Animator Animator { get { return _animator; } }
    public HumanPart Part { get { return part; } }
}
