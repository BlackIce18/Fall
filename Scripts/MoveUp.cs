using UnityEngine;

public class MoveUp : MonoBehaviour
{
    public float speed;
    private bool _isMoving = true;

    private void Update()
    {
        if(_isMoving)
        {
            transform.position += transform.up * speed * Time.deltaTime;
        }
    }
    private void Reset()
    {
        _isMoving = true;
    }
    private void DisableMoving()
    {
        _isMoving = false;
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
