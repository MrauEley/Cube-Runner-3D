using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private GameObject _gameStartUI;

    private void Start()
    {
        _gameStartUI.SetActive(true);
        _gameOverUI.SetActive(false);
    }

    public void SetGameOverUI(bool state)
    {
        _gameOverUI.SetActive(state);
    }

    public void SetGameStartUI(bool state)
    {
        _gameStartUI.SetActive(state);
    }
}
