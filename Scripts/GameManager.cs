using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject _gameOverWindow;
    public static event Action OnGameReset;
    [SerializeField] private Character _character;
    public Character Character { private set { GameManager.instance._character = value; } get { return GameManager.instance._character; } }
    private void Start()
    {
        instance = this;
    }

    public void ResetGame()
    {
        OnGameReset?.Invoke();
        _gameOverWindow.gameObject.SetActive(false);
    }
    private void PlayerDeath()
    {
        _gameOverWindow.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        Character.OnDeath += PlayerDeath;
    }

    private void OnDisable()
    {
        Character.OnDeath -= PlayerDeath;
    }
}
