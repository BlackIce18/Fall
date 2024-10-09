using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [Range(0f, 10f)]
    [SerializeField] private float _speedVertical;
    [Range(0f, 10f)]
    [SerializeField] private float _speedHorizontal;
    [SerializeField] private GameObject _characterModel;
    [SerializeField] private Rigidbody _rigidBody;
    private float _moveHorizontal, _moveVertical;
    private Vector3 minRotation = Vector3.zero;
    private Vector3 maxRotation = Vector3.zero;

    private bool _canMove = true;
    private float _currentVerticalSpeed;
    private float _currentHorizontalSpeed;

    private void Start()
    {
        _currentVerticalSpeed = _speedVertical;
        _currentHorizontalSpeed = _speedHorizontal;
    }
    private void FixedUpdate()
    {
        if (_canMove)
        {
            _moveVertical = Input.GetAxis("Vertical") * _currentVerticalSpeed * Time.fixedDeltaTime;
            _moveHorizontal = Input.GetAxis("Horizontal") * _currentHorizontalSpeed * Time.fixedDeltaTime;
            Vector3 movement = new Vector3(_moveHorizontal, 0, _moveVertical);

            _rigidBody.AddForce(movement * 100);
        }
    }
    private void LimitRotation()
    {
        float x = Mathf.Clamp(transform.eulerAngles.x, minRotation.x, maxRotation.x);
        float y = Mathf.Clamp(transform.eulerAngles.y, minRotation.y, maxRotation.y);
        float z = Mathf.Clamp(transform.eulerAngles.z, minRotation.z, maxRotation.z);

        Camera.main.transform.eulerAngles = new Vector3(x, y, z);
    }

    public void DownVerticalSpeed()
    {
        if(_currentVerticalSpeed > 0)
        {
            _currentVerticalSpeed -= _currentVerticalSpeed / 2;
        }
    }

    public void DownHorizontalSpeed()
    {
        if (_currentHorizontalSpeed > 0)
        {
            _currentHorizontalSpeed -= _currentHorizontalSpeed / 2;
        }
    }

    private void DisableMoving()
    {
        _canMove = false;
        _rigidBody.isKinematic = true;
    }

    private void EnableMoving()
    {
        _canMove = true;
        _rigidBody.isKinematic = false;
    }

    private void Reset()
    {
        EnableMoving();
        _currentVerticalSpeed = _speedVertical;
        _currentHorizontalSpeed = _speedHorizontal;
    }

    private void OnEnable()
    {
        Character.OnDeath += DisableMoving;
        GameManager.OnGameReset += Reset;
    }

    private void OnDisable()
    {
        Character.OnDeath -= DisableMoving;
        GameManager.OnGameReset -= Reset;
    }
}
