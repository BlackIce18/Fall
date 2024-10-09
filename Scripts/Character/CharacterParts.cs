using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterParts : MonoBehaviour
{
    [SerializeField] private Collider _collider;
    [SerializeField] private GameObject _rootBone;
    [SerializeField] private GameObject _visualPart;
    [SerializeField] private HumanPart part;
    [SerializeField] private ParticleSystem _particleSystem;
    public Collider Collider { get => _collider; }
    public GameObject RootBone { get => _rootBone; }
    public GameObject VisualPart { get => _visualPart; }
    public HumanPart Part { get { return part; } }
    public ParticleSystem ParticleSystem { get { return _particleSystem; } }
    private void Start()
    {
        if(_collider == null)
        {
            Debug.Log("�� ���������� collider ��� ����� ���� ���������!");
        }
        if (_rootBone == null)
        {
            Debug.Log("�� ���������� rootBone ��� ����� ���� ���������!");
        }
        if (_visualPart == null)
        {
            Debug.Log("�� ���������� visualPart ��� ����� ���� ���������!");
        }
    }
}
