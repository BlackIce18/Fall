using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    [SerializeField] private GameObject _target;

    private void Update()
    {
        if(_object && _target)
        {
            _object.transform.LookAt(_target.transform);
        }
    }
}
