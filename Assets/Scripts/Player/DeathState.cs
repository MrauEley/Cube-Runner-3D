using UnityEngine;

public class DeathState : MonoBehaviour
{
    private RoadMovement _roadMovement;
    private PlayerMovement _playerMovement;
    private UIManager _uiManager;

    private void Awake()
    {
        _roadMovement = FindObjectOfType<RoadMovement>();
        _uiManager = FindObjectOfType<UIManager>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Wall") || collision.transform.CompareTag("Road"))
        {
            _playerMovement.SetCanMove(false);
            _roadMovement.SetCanMove(false);
            _uiManager.SetGameOverUI(true);
        }
    }
}
